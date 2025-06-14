using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.TarifRsFeature;

public record TarifRsType
{
    public TarifRsType(BillProcedureType procedure, BillJasaMedisType jasaMedis, 
        BillPenunjangType penunjang, BillAkomodasiType akomodasi, 
        BillObatType obat)
    {
        Guard.Against.Null(procedure, nameof(procedure));
        Guard.Against.Null(jasaMedis, nameof(jasaMedis));
        Guard.Against.Null(penunjang, nameof(penunjang));
        Guard.Against.Null(akomodasi, nameof(akomodasi));
        Guard.Against.Null(obat, nameof(obat));

        Procedure = procedure;
        JasaMedis = jasaMedis;
        Penunjang = penunjang;
        Akomodasi = akomodasi;
        Obat = obat;
    }
    public BillProcedureType Procedure { get; init; }
    public BillJasaMedisType JasaMedis { get; init; }
    public BillPenunjangType Penunjang { get; init; }
    public BillAkomodasiType Akomodasi { get; init; }
    public BillObatType Obat { get; init; }
    
    public static TarifRsType Default => new TarifRsType(BillProcedureType.Default, 
        BillJasaMedisType.Default, BillPenunjangType.Default, 
        BillAkomodasiType.Default, BillObatType.Default);
}