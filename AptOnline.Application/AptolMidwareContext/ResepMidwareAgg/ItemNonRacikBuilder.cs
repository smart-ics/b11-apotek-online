namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg
{
    public interface IItemNonRacikBuilder
    {
        IEnumerable<InsertObatNonRacikReqDto> Build(IEnumerable<Listbarang> listBrg,
            InsertResepBpjsRespDto header, IEnumerable<ResepItem> listBrgResep,
            IEnumerable<RespMapDpho> listDpho);
    }
    public class ItemNonRacikBuilder: IItemNonRacikBuilder
    {
        public IEnumerable<InsertObatNonRacikReqDto> Build(IEnumerable<Listbarang> listBrg,
            InsertResepBpjsRespDto header, IEnumerable<ResepItem> listBrgResep,
            IEnumerable<RespMapDpho> listDpho)
        {
            var listObat = listBrg
                .Where(x => x.isRacik == false)
                .Where(x => x.racikId.Trim().Length == 0)?
                .ToList() ?? new List<Listbarang>();
            List<InsertObatNonRacikReqDto> listDto = new();
            foreach (var item in listObat)
            {
                var dpho = listDpho.Where(x => x.BrgId == item.brgId).FirstOrDefault();
                if (dpho is null) continue;
                var resep = listBrgResep.Where(x => x.brgId == item.brgId).FirstOrDefault();
                var signa1 = item.etiketQty; 
                var signa2 = item.etiketHari;
                if (signa1 < 1 && signa2 < 1)
                {
                    var (Signa1, Signa2) = FarmasiHelper.GenSigna(resep.etiketDescription);
                    signa1 = Convert.ToInt16(Signa1);
                    signa2 = Convert.ToInt16(Signa2);
                }

                int jho = (int)Math.Ceiling((double)(item.qty / (signa1 * signa2)));
                listDto.Add(new InsertObatNonRacikReqDto
                {
                    BarangId = item.brgId,
                    NOSJP = header.response.noApotik,
                    NORESEP = header.response.noResep,
                    KDOBT = dpho.DphoId,
                    NMOBAT = dpho.DphoName,
                    SIGNA1OBT = signa1,
                    SIGNA2OBT = signa2,
                    JMLOBT = item.qty,
                    JHO = jho,
                    CatKhsObt = ""
                });
            }
            return listDto;
        }
    }
}