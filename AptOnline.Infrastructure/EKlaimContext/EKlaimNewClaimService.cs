using System.Net;
using AptOnline.Application.EklaimContext;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Infrastructure.EKlaimContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace AptOnline.Infrastructure.EKlaimContext;

public class EKlaimNewClaimService : IEKlaimNewClaimService
{
    private const string MARK_START = "----BEGIN ENCRYPTED DATA----";
    private const string MARK_END = "----END ENCRYPTED DATA----";
    private readonly EKlaimOptions _opt;
    private readonly IRestClientFactory _restClientFactory;

    public EKlaimNewClaimService(IOptions<EKlaimOptions> options,
        IRestClientFactory restClientFactory)
    {
        _restClientFactory = restClientFactory;
        _opt = options.Value;
    }

    public EKlaimNewClaimDto Execute(EKlaimModel req)
    {
        var clientRequest = BuildRequest(req);
        var response = SendRequest(clientRequest);
        var cleanedResponse = CleanUpResponse(response);
        var result = ParseResponse(cleanedResponse);
        return result;
    }

    #region HELPER-METHODS

    private (RestClient client, RestRequest request) BuildRequest(EKlaimModel req)
    {
        var isDebugMode = _opt.Debug.Equals("1");
        var endpoint = isDebugMode ? _opt.BaseApiUrl + "?mode=debug" : _opt.BaseApiUrl;
        var reqBody = JsonConvert.SerializeObject(req);
        if (!isDebugMode)
            reqBody = EKlaimHelper.Encrypt(reqBody, _opt.ApiKey);

        var client = _restClientFactory.Create(endpoint);
        var request = new RestRequest(Method.POST);
        request.AddJsonBody(reqBody);

        return (client, request);
    }

    private static string SendRequest((RestClient client, RestRequest request) input)
    {
        var (client, request) = input;
        var response = client.Execute(request);

        return response.StatusCode != HttpStatusCode.OK
            ? response.ErrorMessage ?? "Unknown HTTP error"
            : response.Content;
    }

    private string CleanUpResponse(string responseContent)
    {
        if (_opt.Debug.Equals("1"))
            return responseContent;

        try
        {
            var cleaned = responseContent
                .Replace(MARK_START, "")
                .Replace(MARK_END, "");
            var decrypted = EKlaimHelper.Decrypt(cleaned, _opt.ApiKey);
            return decrypted;
        }
        catch (Exception ex)
        {
            return "Failed to decrypt response: " + ex.Message;
        }
    }

    private static EKlaimNewClaimDto ParseResponse(string json)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<EKlaimNewClaimResponseEnvelope>(json);

            if (result is null)
                return new EKlaimNewClaimDto(false, "Failed to deserialize response");

            return !result.metadata.code.Equals("200")
                ? new EKlaimNewClaimDto(false, result.metadata.message)
                : new EKlaimNewClaimDto(true, result.response.admission_id);
        }
        catch (Exception ex)
        {
            return new EKlaimNewClaimDto(false, "Invalid JSON response: " + ex.Message);
        }
    }

    #endregion
}