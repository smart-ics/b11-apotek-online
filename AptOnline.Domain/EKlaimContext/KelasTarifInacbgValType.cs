using AptOnline.Domain.Helpers;

namespace AptOnline.Domain.EKlaimContext;

public record KelasTarifInacbgValType : StringLookupValueObject<KelasTarifInacbgValType>
{
    public KelasTarifInacbgValType(string value) : base(value)
    {
    }

    protected override string[] ValidValues => new[]
    {
        "AP",   //  Tarif RS Kelas A Pemerintah
        "AS",   //  Tarif RS Kelas A Swasta
        "BP",   //  Tarif RS Kelas B Pemerintah
        "BS",   //  Tarif RS Kelas B Swasta
        "CP",   //  Tarif RS Kelas C Pemerintah
        "CS",   //  Tarif RS Kelas C Swasta
        "DS",   //  Tarif RS Kelas D Swasta
        "DP",   //  Tarif RS Kelas D Pemerintah
        "RSCM", //  Tarif RSUPN Cipto Mangunkusumo
        "RSJP", //  Tarif RSUPN Jantung Harapan Kita
        "RSD",  //  Tarif RS Kanker Dharmais
        "RSAB"  //  Tarif RSAB Harapan Kita
    };
}
