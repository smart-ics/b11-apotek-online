using System.Net;
using AptOnline.Application.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Nuna.Lib.PatternHelper;
using RestSharp;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.NewClaimService;

public class EKlaimNewClaimServiceX //: IEKlaimNewClaimService
{
    const string MARK_START = "----BEGIN ENCRYPTED DATA----";
    const string MARK_END = "----END ENCRYPTED DATA----";
    private readonly EKlaimOptions _opt;

    public EKlaimNewClaimServiceX(IOptions<EKlaimOptions> options)
    {
        _opt = options.Value;
    }
    public Result<EKlaimNewClaimDto> Execute(EKlaimModel req)
    {
        var result = BuildRequest(req)
            .Bind(SendRequest)
            .Bind(CleanUpResponse)
            .Bind(ParseResponse);
        
        return result;
    }

    private Result<(RestClient client, RestRequest request)> BuildRequest(EKlaimModel req)
    {
        var isDebugMode = _opt.Debug.Equals("1");
        var endpoint = isDebugMode ? _opt.BaseApiUrl + "?mode=debug" : _opt.BaseApiUrl;
        var reqBody = JsonConvert.SerializeObject(req);
        if (!isDebugMode)
            reqBody = EKlaimHelper.Encrypt(reqBody, _opt.ApiKey);

        var client = new RestClient(endpoint);
        var request = new RestRequest(Method.POST);
        request.AddJsonBody(reqBody);

        return Result<(RestClient, RestRequest)>.Success((client, request));
    }

    private Result<string> SendRequest((RestClient client, RestRequest request) input)
    {
        var (client, request) = input;
        var response = client.Execute(request);

        return response.StatusCode != HttpStatusCode.OK ? 
            Result<string>.Failure(response.ErrorMessage ?? "Unknown HTTP error") : 
            Result<string>.Success(response.Content);
    }

    private Result<string> CleanUpResponse(string responseContent)
    {
        if (_opt.Debug.Equals("1"))
            return Result<string>.Success(responseContent);

        try
        {
            var cleaned = responseContent
                .Replace(MARK_START, "")
                .Replace(MARK_END, "");
            var decrypted = EKlaimHelper.Decrypt(cleaned, _opt.ApiKey);
            return Result<string>.Success(decrypted);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure("Failed to decrypt response: " + ex.Message);
        }
    }

    private Result<EKlaimNewClaimDto> ParseResponse(string json)
    {
        try
        {
            var result = JsonConvert.DeserializeObject<EKlaimNewClaimResponseEnvelope>(json);

            if (result is null)
                return Result<EKlaimNewClaimDto>.Failure("Failed to deserialize response");

            return !result.metadata.code.Equals("200") ? 
                Result<EKlaimNewClaimDto>.Failure(result.metadata.message) : 
                Result<EKlaimNewClaimDto>.Success(new EKlaimNewClaimDto(true, result.response.admission_id));
        }
        catch (Exception ex)
        {
            return Result<EKlaimNewClaimDto>.Failure("Invalid JSON response: " + ex.Message);
        }
    }
}