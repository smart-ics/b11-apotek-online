using AptOnline.Application.EklaimContext;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Infrastructure.EKlaimContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace AptOnline.Infrastructure.EKlaimContext
{
    public class EKlaimNewClaimService : IEklaimNewClaimService
    {
        const string MARK_START = "----BEGIN ENCRYPTED DATA----";
        const string MARK_END = "----END ENCRYPTED DATA----";
        private readonly EKlaimOptions _opt;

        public EKlaimNewClaimService(IOptions<EKlaimOptions> options)
        {
            _opt = options.Value;
        }

        public EKlaimNewClaimDto Execute(EklaimModel req)
        {
            var isDebugMode = _opt.Debug.Equals("1");
            var endpoint = isDebugMode ? _opt.BaseApiUrl + "?mode=debug" : _opt.BaseApiUrl;
            var reqBody = JsonConvert.SerializeObject(req);
            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.POST);
            if (isDebugMode)
                reqBody = EKlaimHelper.Encrypt(reqBody, _opt.ApiKey);
            request.AddJsonBody(reqBody);
            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);
            string tmpResult = response.Content;
            if (isDebugMode)
            {
                tmpResult = tmpResult.Replace(MARK_START, "").Replace(MARK_END, "");
                tmpResult = EKlaimHelper.Decrypt(tmpResult, _opt.ApiKey);
            }
            try
            {
                var result = JsonConvert.DeserializeObject<EKlaimNewClaimResponse>(tmpResult);
                if (!result.metadata.code.Equals("200"))
                    return new EKlaimNewClaimDto(false, result.metadata.message);
                return new EKlaimNewClaimDto(true, result.response.admission_id);
            }
            catch 
            {
                throw new Exception(tmpResult);
            }
        }
    }
    #region Dto
    public class EKlaimNewClaimResponse
    {
        public EKlaimResponseMeta metadata { get; set; }
        public EKlaimNewClaimRespDto response { get; set; }
    }
    public class EKlaimNewClaimRespDto
    {
        public string patient_id { get; set; }
        public string admission_id { get; set; }
        public string hospital_admission_id { get; set; }
    }
    #endregion
}
