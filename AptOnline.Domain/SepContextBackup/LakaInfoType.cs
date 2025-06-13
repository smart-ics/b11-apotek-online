using AptOnline.Domain.SepContext.ReferensiFeature;

namespace AptOnline.Domain.SepContext;

public record SuplesiInfoType(bool IsSuplesi, string NomorSepSuplesi, KecamatanType LokasiLaka);
public record StatusLakaType(string Code, string Name);
    