using AptOnline.Application.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using System.Text.Json;
using RestSharp;

namespace AptOnline.Infrastructure.EKlaimContext.EKlaimFeature.NewClaimService;

public class EKlaimNewClaimService : IEKlaimNewClaimService
{
    private const string MARK_START = "----BEGIN ENCRYPTED DATA----";
    private const string MARK_END = "----END ENCRYPTED DATA----";
    private readonly EKlaimOptions _opt;
    private readonly bool _isDebugMode;
    private readonly bool _isReleaseMode;
    private readonly IRestClientFactory _restClient;

    public EKlaimNewClaimService(IOptions<EKlaimOptions> options,
        IRestClientFactory restClient)
    {
        _opt = options.Value;
        _isDebugMode = _opt.Debug.Equals("1");
        _isReleaseMode = !_isDebugMode;
        _restClient = restClient;
    }

    public EKlaimNewClaimDto Execute(EKlaimModel req)
    {
        //  REQUEST
        var request = new RestRequest(Method.POST);
        request.AddJsonBody(BuildRequest(req));

        var endpoint = _opt.BaseApiUrl;
        if (_isDebugMode) endpoint += "?mode=debug";
        var client = _restClient.Create(endpoint);
        
        //  EXECUTE
        var response = client.Execute(request);
            
        //  RESPONSE
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new HttpRequestException(response.ErrorMessage);
        var responseContent = CleanUpResponse(response.Content);
        
        //  PARSE CONTENT
        var result = responseContent
            .DeserializeOrThrow<EKlaimNewClaimResponse>($"Parsing failed: {responseContent}");
        result ??= EKlaimNewClaimResponse.Default;
        return result.metadata.code.Equals("200") 
            ? new EKlaimNewClaimDto(true, result.response.admission_id) 
            : new EKlaimNewClaimDto(false, result.metadata.message);
    }

    private string BuildRequest(EKlaimModel req)
    {
        var reqObj = new EKlaimNewClaimRequest(
            new EKlaimNewClaimRequestMeta("new_claim"), 
            new EKlaimNewClaimRequestData(req));
        var reqBody = JsonSerializer.Serialize(reqObj);
        
        if (_isReleaseMode)
            reqBody = EKlaimHelper.Encrypt(reqBody, _opt.ApiKey);
        return reqBody;
    }

    private string CleanUpResponse(string responseContent)
    {
        var result = responseContent;
        if (!_isReleaseMode) return result;
        
        result = result.Replace(MARK_START, "").Replace(MARK_END, "");
        result = EKlaimHelper.Decrypt(result, _opt.ApiKey);
        return result;
    }
}