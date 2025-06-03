using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext;

public record EKlaimMedCovidType
{
    public EKlaimMedCovidType(Covid19StatusCodeType covid19StatusCodeVal, EpisodeValType episodesVal, 
        AksesNaatType aksesNaatVal, YesNoIndikatorValType isomanIndikatorVal)
    {
        Guard.Against.Null(covid19StatusCodeVal, nameof(covid19StatusCodeVal), "Covid19 Status Tidak boleh kosong");
        Guard.Against.Null(episodesVal, nameof(episodesVal), "Episode Tidak boleh kosong");
        Guard.Against.Null(aksesNaatVal, nameof(aksesNaatVal), "Akses Naat Tidak boleh kosong");
        Guard.Against.Null(isomanIndikatorVal, nameof(isomanIndikatorVal), "Isoman Indikator Tidak boleh kosong");
        
        Covid19StatusCodeVal = covid19StatusCodeVal;
        EpisodesVal = episodesVal;
        AksesNaatVal = aksesNaatVal;
        IsomanIndikatorVal = isomanIndikatorVal;
    }
    
    public static EKlaimMedCovidType Default
        => new EKlaimMedCovidType(Covid19StatusCodeType.Default,
            EpisodeValType.Default, AksesNaatType.Default, 
            YesNoIndikatorValType.Default);
    
    
    public Covid19StatusCodeType Covid19StatusCodeVal { get; init; }
    public EpisodeValType EpisodesVal { get; init; }
    public AksesNaatType AksesNaatVal { get; init; }
    public YesNoIndikatorValType IsomanIndikatorVal { get; init; }
}

