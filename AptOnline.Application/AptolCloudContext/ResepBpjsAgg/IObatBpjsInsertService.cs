using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;
using Nuna.Lib.DataAccessHelper;

namespace AptOnline.Application.AptolCloudContext.ResepBpjsAgg
{
    public interface IObatBpjsInsertService : IRequestResponseService<ObatBpjsInsertParam, ObatBpjsInsertResponseDto>
    {
    }
    public class ObatBpjsInsertResponseDto
    {
        public ObatBpjsInsertResponseDto(string obatId, string obatName)
        {
            ObatId = obatId;
            ObatName = obatName;
            RespCode = string.Empty;
            RespMessage = string.Empty;
        }

        public string ObatId { get; internal set; }
        public string ObatName {  get; internal set; }
        public string RespCode { get; internal set; }
        public string RespMessage {  get; internal set; }

        public void SetResp(string code, string msg)
        {
            RespCode = code;
            RespMessage = msg;
        }
    }
    public record ObatBpjsInsertParam(string NoSep, string NoApotik, string NoResep, ResepMidwareItemModel Obat);
}
