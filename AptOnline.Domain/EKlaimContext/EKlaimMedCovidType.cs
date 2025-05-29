using GuardNet;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimMedCovidType
{
    public EKlaimMedCovidType(Covid19StatusCodeValType covid19StatusCodeVal, EpisodeValType episodesVal, 
        AksesNaatType aksesNaatVal, YesNoIndikatorValType isomanIndikatorVal)
    {
        Guard.NotNull(covid19StatusCodeVal, nameof(covid19StatusCodeVal), "Covid19 Status Tidak boleh kosong");
        Guard.NotNull(episodesVal, nameof(episodesVal), "Episode Tidak boleh kosong");
        Guard.NotNull(aksesNaatVal, nameof(aksesNaatVal), "Akses Naat Tidak boleh kosong");
        Guard.NotNull(isomanIndikatorVal, nameof(isomanIndikatorVal), "Isoman Indikator Tidak boleh kosong");
        
        Covid19StatusCodeVal = covid19StatusCodeVal;
        EpisodesVal = episodesVal;
        AksesNaatVal = aksesNaatVal;
        IsomanIndikatorVal = isomanIndikatorVal;
    }
    
    public static EKlaimMedCovidType Default
        => new EKlaimMedCovidType(Covid19StatusCodeValType.Default,
            EpisodeValType.Default, AksesNaatType.Default, 
            YesNoIndikatorValType.Default);
    
    
    public Covid19StatusCodeValType Covid19StatusCodeVal { get; init; }
    public EpisodeValType EpisodesVal { get; init; }
    public AksesNaatType AksesNaatVal { get; init; }
    public YesNoIndikatorValType IsomanIndikatorVal { get; init; }
}

