using AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;
using AptOnline.Domain.SepContext.SepFeature;

namespace AptOnline.Infrastructure.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgDto
{
    public GrouperIdrgDto(){}

    public GrouperIdrgDto(GrouperIdrgModel model)
    {
        EKlaimId = model.EKlaimId;
        GrouperIdrgDate = model.GrouperIdrgDate;
        SepId = model.Sep.SepId;
        SepDate = model.Sep.SepDate;
        SepNo = model.Sep.SepNo;
        InfoResult = model.HasilGrouping.InfoResult;
        JenisRawat = model.HasilGrouping.JenisRawat;
        MdcId = model.HasilGrouping.Mdc.MdcId;
        MdcName = model.HasilGrouping.Mdc.MdcName;
        DrgId = model.HasilGrouping.Drg.DrgId;
        DrgName = model.HasilGrouping.Drg.DrgName;
        StatusResult = model.HasilGrouping.StatusResult;
        Phase = (int)model.Phase;
        FinalTimestamp = model.FinalTimestamp;
    }
    public string EKlaimId {get;set;}
    public DateTime GrouperIdrgDate {get;set;}
    public string SepId {get;set;}
    public DateTime SepDate {get;set;}
    public string SepNo {get;set;}
    public string InfoResult {get;set;}
    public string JenisRawat {get;set;}
    public string MdcId {get;set;}
    public string MdcName {get;set;}
    public string DrgId {get;set;}
    public string DrgName {get;set;}
    public string StatusResult {get;set;}
    public int Phase {get;set;}
    public DateTime FinalTimestamp {get;set;}
    
    public GrouperIdrgModel ToModel(
        IEnumerable<GrouperIdrgDiagnosaType> listDiagnosa,
        IEnumerable<GrouperIdrgProsedurType> listProsedur)
    {
        var sep = new SepRefference(SepId, SepNo, SepDate);
        var mdc = new MdcType(MdcId, MdcName);
        var drg = new DrgType(DrgId, DrgName);
        var groupingResult = new GrouperIdrgResultType(InfoResult, JenisRawat, mdc, drg, StatusResult);
        var result = new GrouperIdrgModel(EKlaimId, GrouperIdrgDate, 
            sep, groupingResult,  (GroupingPhaseEnum)Phase, FinalTimestamp, 
            listDiagnosa, 
            listProsedur);
        return result;
    }
}