using Nuna.Lib.PatternHelper;

namespace AptOnline.Domain.Helpers;

public class ResultMonadUsageSample
{
    public static void Run1() =>
        GetNameById(1)
            .Bind(name => ParseAge("35"))
            .Map(age => $"User age is {age}")
            .OnSuccess(Console.WriteLine)
            .OnFailure(Console.Error.WriteLine);

    #region RUN-1
    private static Result<string> GetNameById(int id)
    {
        return id <= 0 ? 
            Result<string>.Failure("Invalid ID.") : 
            Result<string>.Success("Yudis");
    }

    private static Result<int> ParseAge(string input)
    {
        return !int.TryParse(input, out var age) ? 
            Result<int>.Failure("Invalid age.") : 
            Result<int>.Success(age);
    }
    #endregion
    
    #region RUN-2

    
    public record MhsTestModel(string Nim, string Nama, string Alamat);
    public record RegisTestModel(string RegId, string Nim, string KelasId);
    public record KelasTestModel(string KelasId, string NamaKelas);

    private static MayBe<KelasTestModel> GetKelas(string kelasId)
    {
        var result = kelasId == "111" ? new KelasTestModel("111", "TI") : null;
        return MayBe.From(result);
    }

    private static MayBe<MhsTestModel> GetMhs(string nim)
    {
        var result = nim == "222" ? new MhsTestModel("222", "Yudis", "Jl. Kebon Jeruk") : null;
        return MayBe.From(result);
    }
    
    private static Result<RegisTestModel> RegisterMhs(string nim, string kelasId)
    {
        var result =  GetKelas(kelasId)
            .ToResult("Kelas not found.")
            .Bind(kelas => GetMhs(nim)
                .ToResult("Mahasiswa not found.")
                .Map(mhs => new RegisTestModel(Guid.NewGuid().ToString(), nim, kelas.KelasId)));
        return result;
    }

    public static void Run2()
    {
        var result = RegisterMhs("222", "111");

        if (result.IsSuccess)
            Console.WriteLine(result.Value);
        else
            Console.WriteLine($"Error: {result.Error}");
    }
    #endregion

}