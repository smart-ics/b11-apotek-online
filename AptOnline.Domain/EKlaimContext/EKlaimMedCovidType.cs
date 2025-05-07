namespace AptOnline.Domain.EKlaimContext;

public record EKlaimMedCovidType(
    Covid19StatusCodeValType Covid19StatusCodeVal,
    EpisodeValType EpisodesVal,
    AksesNaatValType AksesNaatVal,
    YesNoIndikatorValType IsomanIndikatorVal);
