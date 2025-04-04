using System.Security.Cryptography.X509Certificates;
using AptOnline.Application.AptolCloudContext.PpkAgg;
using AptOnline.Domain.AptolCloudContext.PpkAgg;
using AptOnline.Infrastructure.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xunit;

namespace AptOnline.Infrastructure.AptolCloudContext.PpkAgg;

public class PpkGetService : IPpkGetService
{
    private readonly BpjsOptions _opt;
    private readonly string _timestamp;
    private readonly string _signature;
    private readonly string _decryptKey;
    public PpkGetService(IOptions<BpjsOptions> opt)
    {
        _opt = opt.Value;
        _timestamp = BpjsHelper.GetTimeStamp().ToString();
        _signature = BpjsHelper.GenHMAC256(_opt.ConsId + "&" + _timestamp, _opt.SecretKey);
        _decryptKey = _opt.ConsId + _opt.SecretKey + _timestamp;
    }
    public PpkType Execute()
    {
        var endpoint = $"{_opt.BaseApiUrl}/referensi/settingppk/read/{_opt.PpkId}";
        var client = new RestClient(endpoint)
        {
            ClientCertificates = new X509CertificateCollection()
        };
        var request = new RestRequest(Method.GET);
        request.AddHeader("X-cons-id", _opt.ConsId);
        request.AddHeader("X-timestamp", _timestamp);
        request.AddHeader("X-signature", _signature);
        request.AddHeader("user_key", _opt.UserKey);
        var response = client.Execute(request);

        JObject jResult;
        try { jResult = JObject.Parse(response.Content); }
        catch { throw new Exception(response.StatusDescription); }

        if (!jResult["metaData"]["code"].ToString().Equals("200"))
        {
            throw new Exception(response.Content);
        }
        var tempResp = jResult.SelectToken("response");
        try
        {
            string decryptedResp = BpjsHelper.Decrypt(_decryptKey, tempResp.ToString());
            jResult["response"] = JObject.Parse(decryptedResp);
        }
        catch { }
        var tempResult = jResult.ToObject<PpkGetResponse>();
        var content = tempResult.response;
        var result = new PpkGetDto
        {
            Kode = tempResult.response.Kode,
            Nama =_opt.PpkName,
            NamaApoteker = tempResult.response.NamaApoteker,
            NamaKepala = tempResult.response.NamaKepala,
            JabatanKepala = tempResult.response.JabatanKepala,
            NipKepala = tempResult.response.NamaKepala,
            Siup = tempResult.response.Siup,
            Alamat = tempResult.response.Alamat,
            Kota = tempResult.response.Kota,
            NamaVerifikator = tempResult.response.NamaVerifikator,
            NppVerifikator = tempResult.response.NppVerifikator,
            NamaPetugasApotek = tempResult.response.NamaPetugasApotek,
            NipPetugasApotek = tempResult.response.NipPetugasApotek,
            CheckStock = tempResult.response.CheckStock
        };
        return result.ToType();
    }
}

public class PpkGetSeriveTest
{
    private readonly PpkGetService _sut;

    public PpkGetSeriveTest()
    {
        var opt = FakeAppSetting.GetBpjsOptions();
        _sut = new PpkGetService(opt);
    }

    [Fact]
    public void PpkGetService_Execute_Test()
    {
        var expected = new PpkType("0137A047", "IFRS Mekar Sari", 
            "128719", "Jl. Raya Mekar Sari No. 1", "bekasi",
            new KepalaType("kepala", "jabatan", "kepala"), 
            new VerifikatorType("verifikator", "99999"), 
            new ApotekType("petugas", "99999", "apoteker", "False"));
        var actual = _sut.Execute();
        actual.Should().BeEquivalentTo(expected);
    }
}