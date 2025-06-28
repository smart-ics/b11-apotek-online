// using AptOnline.Domain.EKlaimContext.IdrgFeature;
// using AptOnline.Domain.SepContext.SepFeature;
//
// namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;
//
// public class GrouperIdrgModel
// {
//     public string EKlaimId { get; init; }
//     public SepRefference Sep { get; init; }
//     
//     public IEnumerable<GrouperIdrgDiagnosaType> ListDiagnosa { get; init; }
//     
// }
//
// public record GrouperIdrgDiagnosaType()
// {
//     public GrouperIdrgDiagnosaType(int noUrut, IdrgRefferenceType idrg)
//     {
//         
//     }
//     public int NoUrut { get; init; }
//     public IdrgRefferenceType Idrg { get; init; } 
//     public bool IsPrimary => NoUrut == 1;
// }
// public record GrouperIdrgProsedurType(IdrgRefferenceType Idrg, int NoUrut, int Multiplicity, int Setting);
