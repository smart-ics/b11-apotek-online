using AptOnline.Domain.AptolMidwareContext.ResepMidwareContext;

namespace AptOnline.Infrastructure.AptolMidwareContext.ResepMidwareAgg;

public class ResepMidwareItemDto
{
    public ResepMidwareItemDto()
    {
    }

    public ResepMidwareItemDto(ResepMidwareItemModel model)
    {
        ResepMidwareId = model.ResepMidwareId;
        NoUrut = model.NoUrut;
        IsRacik = model.IsRacik;
        RacikId = model.RacikId;
        BarangId = model.Brg.BrgId;
        BarangName = model.Brg.BrgName;
        DphoId = model.Dpho.DphoId;
        DphoName = model.Dpho.DphoName;
        Signa1 = model.Signa1;
        Signa2 = model.Signa2;
        Permintaan = model.Permintaan;
        Jho = model.Jho;
        Jumlah = model.Jumlah;
        Note = model.Note;
    }
    public string ResepMidwareId {get;set;}
    public int NoUrut {get;set;}
    public bool  IsRacik {get;set;}
    public string RacikId {get;set;}
    public string BarangId {get;set;}
    public string BarangName {get;set;}
    public string DphoId {get;set;}
    public string DphoName {get;set;}
    public int Signa1 {get;set;}
    public decimal Signa2 {get;set;}
    public int Permintaan {get;set;}
    public int Jho {get;set;}
    public int Jumlah {get;set;}
    public string Note {get;set;}
    
    public ResepMidwareItemModel ToModel() 
        => ResepMidwareItemModel.Load(
            ResepMidwareId, NoUrut, IsRacik, RacikId,
            BarangId, BarangName, DphoId, DphoName,
            Signa1, Signa2, Permintaan,
            Jho, Jumlah, Note);
}