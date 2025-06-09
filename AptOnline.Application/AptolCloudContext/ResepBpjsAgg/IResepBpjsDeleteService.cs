using AptOnline.Domain.AptolCloudContext.ResepBpjsAgg;
using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using Nuna.Lib.CleanArchHelper;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg
{
    public interface IResepBpjsDeleteService : INunaService<ResepBpjsDeleteResponseDto, ResepBpjsDeleteParam>
    {
    }
    public record ResepBpjsDeleteParam(string NoSep, string NoApotik, string NoResep);
    //{
    //   "nosjp": "1202A00201210000032",
    //   "refasalsjp": "1202R0010121V000325",
    //   "noresep": "0SI44"
    //   }
    public class ResepBpjsDeleteResponseDto
    {
        public ResepBpjsDeleteResponseDto(string resepRsId, string reffId)
        {
            ReffId = reffId;
            ResepRsId = resepRsId;
            RespCode = string.Empty;
            RespMessage = string.Empty;
        }

        public string ResepRsId { get; internal set; }
        public string ReffId { get; internal set; }
        public string RespCode { get; internal set; }
        public string RespMessage { get; internal set; }

        public void SetResp(string code, string msg)
        {
            RespCode = code;
            RespMessage = msg;
        }
    }
}
