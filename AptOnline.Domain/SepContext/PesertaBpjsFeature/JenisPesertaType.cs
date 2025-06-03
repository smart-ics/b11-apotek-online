namespace AptOnline.Domain.SepContext.PesertaBpjsFeature;

public record JenisPesertaType(string Code, string Name)
{
    public static JenisPesertaType Default => new("-", "-");
}