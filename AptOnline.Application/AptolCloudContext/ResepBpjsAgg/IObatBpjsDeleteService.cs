using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using Nuna.Lib.DataAccessHelper;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg
{
    public interface IObatBpjsDeleteService : IRequestResponseService<ObatBpjsDeleteParam, ObatBpjsDeleteResponseDto>
    {
    }
    public class ObatBpjsDeleteResponseDto
    {
        public ObatBpjsDeleteResponseDto(string obatId, string obatName)
        {
            ObatId = obatId;
            ObatName = obatName;
            RespCode = string.Empty;
            RespMessage = string.Empty;
        }

        public string ObatId { get; internal set; }
        public string ObatName { get; internal set; }
        public string RespCode { get; internal set; }
        public string RespMessage { get; internal set; }

        public void SetResp(string code, string msg)
        {
            RespCode = code;
            RespMessage = msg;
        }
    }
    public record ObatBpjsDeleteParam(string NoSep, string NoApotik, string NoResep, ResepMidwareItemModel Obat);
    //{
    //    "nosepapotek" : "1801A00104190000001",
    //    "noresep" : "12345",
    //    "kodeobat" : "25180404057",
    //    "tipeobat" : "N"
    //    }

}