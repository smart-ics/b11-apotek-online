namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg
{
    public interface IItemRacikBuilder
    {
        IEnumerable<InsertObatRacikReqDto> Build(IEnumerable<Listbarang> listBrg,
            InsertResepBpjsRespDto header, IEnumerable<ResepItem> listBrgResep,
            IEnumerable<RespMapDpho> listDpho);
    }
    public class ItemRacikBuilder: IItemRacikBuilder
    {
        public IEnumerable<InsertObatRacikReqDto> Build(IEnumerable<Listbarang> listBrg,
            InsertResepBpjsRespDto header, IEnumerable<ResepItem> listBrgResep,
            IEnumerable<RespMapDpho> listDpho)
        {
            //ambil header raciknya dulu
            var listRacik = listBrg
                .Where(x => x.isRacik == true)
                .Where(x => x.racikId.Trim().Length == 0)?.ToList();
            if (listRacik is null) return null;

            List<Listbarang> listObat = new();
            List<InsertObatRacikReqDto> listDto = new();
            foreach (var racik in listRacik)
            {
                //ambil item raciknya
                listObat.AddRange(listBrg
                    .Where(x => x.racikId == racik.brgId).ToList());
                //tambahkan ke dto
                foreach (var obat in listObat)
                {
                    var dpho = listDpho.Where(x => x.BrgId == obat.brgId).FirstOrDefault();
                    if (dpho is null) continue;
                    var resep = listBrgResep.Where(x => x.brgId == obat.brgId).FirstOrDefault();
                    int signa1 = racik.etiketQty;
                    int signa2 = racik.etiketHari;
                    if (signa1 < 1 && signa2 < 1)
                    {
                        var (Signa1, Signa2) = FarmasiHelper.GenSigna(resep.etiketDescription);
                        signa1 = Convert.ToInt16(Signa1);
                        signa2 = Convert.ToInt16(Signa2);
                    }
                    int jho = (int)Math.Ceiling((double)(obat.qty / (signa1 * signa2)));
                    listDto.Add(new InsertObatRacikReqDto
                    {
                        BarangId = obat.brgId,
                        NOSJP = header.response.noApotik,
                        NORESEP = header.response.noResep,
                        JNSROBT = obat.racikId,
                        KDOBT = dpho.DphoId,
                        NMOBAT = dpho.DphoName,
                        SIGNA1OBT = signa1,
                        SIGNA2OBT = signa2,
                        PERMINTAAN = resep is not null ? resep.qty : obat.qty,
                        JMLOBT = obat.qty,
                        JHO = jho,
                        CatKhsObt = $"Obat Racikan {racik.brgId}"
                    });
                }
            }
            return listDto;
        }
    }
}