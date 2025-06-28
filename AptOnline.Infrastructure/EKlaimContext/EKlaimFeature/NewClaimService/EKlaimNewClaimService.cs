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
    private readonly IRestClientFactory _restClient;

    public EKlaimNewClaimService(IOptions<EKlaimOptions> options,
        IRestClientFactory restClient)
    {
        _opt = options.Value;
        _isDebugMode = _opt.Debug.Equals("1");
        _restClient = restClient;
    }

    public EKlaimNewClaimDto Execute(EKlaimModel req)
    {
        //  BUILD-REQUEST
        var reqObj = new EKlaimNewClaimRequest(
            new EKlaimNewClaimRequestMeta("new_claim"), 
            new EKlaimNewClaimRequestData(req));
        var reqBody = JsonSerializer.Serialize(reqObj);
        if (!_isDebugMode)
            reqBody = EKlaimHelper.Encrypt(reqBody, _opt.ApiKey);
        var request = new RestRequest(Method.POST);
        request.AddJsonBody(reqBody);
        
        //  EXECUTE
        var endpoint = _isDebugMode 
            ? _opt.BaseApiUrl + "?mode=debug" 
            : _opt.BaseApiUrl;
        var client = _restClient.Create(endpoint);
        var response = client.Execute(request);
            
        //  RESPONSE
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new HttpRequestException(response.ErrorMessage);
        
        var tmpResult = response.Content;
        if (!_isDebugMode)
        {
            tmpResult = tmpResult.Replace(MARK_START, "").Replace(MARK_END, "");
            tmpResult = EKlaimHelper.Decrypt(tmpResult, _opt.ApiKey);
        }
        
        EKlaimNewClaimResponse? result;
        try
        {
            result = JsonSerializer.Deserialize<EKlaimNewClaimResponse>(tmpResult);
        }
        catch (JsonException ex)
        {
            throw new JsonException($"Failed to parse response: {tmpResult}", ex);
        }
        
        result ??= EKlaimNewClaimResponse.Default;
        return result.metadata.code.Equals("200") 
            ? new EKlaimNewClaimDto(true, result.response.admission_id) 
            : new EKlaimNewClaimDto(false, result.metadata.message);
    }
}