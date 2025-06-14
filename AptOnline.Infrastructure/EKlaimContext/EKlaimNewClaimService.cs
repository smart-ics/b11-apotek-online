using AptOnline.Application.EklaimContext;
using AptOnline.Domain.EKlaimContext;
using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Infrastructure.EKlaimContext.Shared;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace AptOnline.Infrastructure.EKlaimContext
{
    public class EKlaimNewClaimService : IEKlaimNewClaimService
    {
        const string MARK_START = "----BEGIN ENCRYPTED DATA----";
        const string MARK_END = "----END ENCRYPTED DATA----";
        private readonly EKlaimOptions _opt;

        public EKlaimNewClaimService(IOptions<EKlaimOptions> options)
        {
            _opt = options.Value;
        }

        public EKlaimNewClaimDto Execute(EKlaimModel req)
        {
            var reqObj = new EKlaimNewClaimRequest
            {
                metadata = new EKlaimNewClaimRequestMeta { method = "new_claim" },
                data = new EKlaimNewClaimRequestData
                {
                    nomor_kartu = req.PesertaBpjs.PesertaBpjsNo,
                    nomor_sep = req.Sep.SepNo,
                    nomor_rm = req.Pasien.PasienId,
                    nama_pasien = req.Pasien.PasienName,
                    tgl_lahir = req.Pasien.BirthDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    gender = req.Pasien.Gender.Value
                }
            };
            var isDebugMode = _opt.Debug.Equals("1");
            var endpoint = isDebugMode ? _opt.BaseApiUrl + "?mode=debug" : _opt.BaseApiUrl;
            var reqBody = JsonConvert.SerializeObject(reqObj);
            var client = new RestClient(endpoint);
            var request = new RestRequest(Method.POST);
            if (!isDebugMode)
                reqBody = EKlaimHelper.Encrypt(reqBody, _opt.ApiKey);
            request.AddJsonBody(reqBody);
            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.ErrorMessage);
            string tmpResult = response.Content;
            if (!isDebugMode)
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

    public class EKlaimNewClaimRequest
    {
        public EKlaimNewClaimRequestMeta metadata { get; set; }
        public EKlaimNewClaimRequestData data { get; set; }
    }

    public class EKlaimNewClaimRequestMeta
    {
        public string method { get; set; }
    }

    public class EKlaimNewClaimRequestData
    {
        public string nomor_kartu { get; set; }
        public string nomor_sep { get; set; }
        public string nomor_rm { get; set; }
        public string nama_pasien { get; set; }
        public string tgl_lahir { get; set; }
        public string gender { get; set; }
    }

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
