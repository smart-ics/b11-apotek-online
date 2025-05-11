using AptOnline.Domain.SepContext.ReferensiFeature;

namespace AptOnline.Domain.SepContext;

public record SuplesiInfoType(string Code, string Name, string NomorSuplesi, KecamatanType LokasiLaka);
public record StatusLakaType(string Code, string Name);
    