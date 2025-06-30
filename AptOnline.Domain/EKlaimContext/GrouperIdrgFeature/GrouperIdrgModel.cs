using AptOnline.Domain.EKlaimContext.EKlaimFeature;
using AptOnline.Domain.EKlaimContext.IdrgFeature;
using AptOnline.Domain.SepContext.SepFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgModel : IEKlaimKey
{
    private readonly List<GrouperIdrgDiagnosaType> _listDiagnosa;
    private readonly List<GrouperIdrgProsedurType> _listProsedur;

    public GrouperIdrgModel(string eKlaimId, DateTime grouperIdrgDate, 
        SepRefference sep, GrouperIdrgResultType result, 
        GroupingPhaseEnum phase, DateTime finalTimestamp,
        IEnumerable<GrouperIdrgDiagnosaType> listDiagnosa, 
        IEnumerable<GrouperIdrgProsedurType> listProsedur)
    {
        Guard.Against.NullOrWhiteSpace(eKlaimId, nameof(eKlaimId));
        Guard.Against.Null(sep, nameof(sep));
        Guard.Against.Null(result, nameof(result));

        var listDx = listDiagnosa?.ToList() ?? throw new ArgumentNullException(nameof(listDiagnosa));
        var listPr = listProsedur?.ToList() ?? throw new ArgumentNullException(nameof(listProsedur));

        EKlaimId = eKlaimId;
        GrouperIdrgDate = grouperIdrgDate;
        Sep = sep;
        HasilGrouping = result;
        Phase = phase;
        FinalTimestamp = finalTimestamp;
        _listDiagnosa = listDx;
        _listProsedur = listPr;
    }

    public static GrouperIdrgModel Create(string eKlaimId, DateTime grouperIdrgDate, 
        SepRefference sep)
    {
        var result = new GrouperIdrgModel(
            eKlaimId, grouperIdrgDate, sep, GrouperIdrgResultType.Default, 
            GroupingPhaseEnum.BelumGrouping, new DateTime(3000,1,1), 
            new List<GrouperIdrgDiagnosaType>(), 
            new List<GrouperIdrgProsedurType>());
        return result;
    }
    
    public string EKlaimId { get; init; }
    public DateTime GrouperIdrgDate { get; init; }
    public SepRefference Sep { get; init; }
    
    public GroupingPhaseEnum Phase { get; private set; }
    public DateTime FinalTimestamp { get; private set; }
    public GrouperIdrgResultType HasilGrouping { get; private set;} 
    
    public IEnumerable<GrouperIdrgDiagnosaType> ListDiagnosa  => _listDiagnosa;
    public IEnumerable<GrouperIdrgProsedurType> ListProsedur => _listProsedur;
    
    public static GrouperIdrgModel Default 
    => new GrouperIdrgModel(
        "-", new DateTime(3000,1,1), SepType.Default.ToRefference(), 
        GrouperIdrgResultType.Default, 
        GroupingPhaseEnum.BelumGrouping,
        new DateTime(3000,1,1),
        new List<GrouperIdrgDiagnosaType>(), 
        new List<GrouperIdrgProsedurType>());

    #region DIAGNOSA-RELATED METHOD
    public void Add(IdrgDiagnosaType diagnosa)
    {
        if (Phase == GroupingPhaseEnum.Final) 
            throw new InvalidOperationException("Grouper Idrg sudah final");
        
        if (_listDiagnosa.Any(x => x.Idrg == diagnosa.ToRefference())) 
            return;
        
        var noUrut = _listDiagnosa
            .DefaultIfEmpty(new GrouperIdrgDiagnosaType(0, IdrgRefferenceType.Default))
            .Max(x => x.NoUrut) + 1;
        var idrg = diagnosa.ToRefference();
        _listDiagnosa.Add(new GrouperIdrgDiagnosaType(noUrut, idrg));
    }
    public void Remove(IdrgDiagnosaType diagnosa)
    {
        if (Phase == GroupingPhaseEnum.Final) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        _listDiagnosa.RemoveAll(x => x.Idrg == diagnosa.ToRefference());
        ReArrangeNoUrut();
    }
    public void UbahNoUrutDiagnosa(IdrgDiagnosaType idrg, int targetNoUrut)
    {
        if (Phase == GroupingPhaseEnum.Final) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        ReArrangeNoUrut();

        targetNoUrut = Math.Max(1, Math.Min(targetNoUrut, _listDiagnosa.Count));
        var currentItem = _listDiagnosa.First(x => x.Idrg == idrg.ToRefference());
        _listDiagnosa.Remove(currentItem);
        _listDiagnosa.Insert(targetNoUrut - 1, currentItem);

        for (var i = 0; i < _listDiagnosa.Count; i++)
            _listDiagnosa[i].SetNoUrut(i + 1);
    }
    #endregion
    
    #region PROSEDUR-RELATED METHOD
    public void Add(IdrgProsedurType prosedur)
    {
        if (Phase == GroupingPhaseEnum.Final) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        if (_listProsedur.Any(x => x.Idrg == prosedur.ToRefference())) 
            return;
        
        var noUrut = _listProsedur
            .DefaultIfEmpty(new GrouperIdrgProsedurType(0, IdrgRefferenceType.Default,1,1))
            .Max(x => x.NoUrut) + 1;
        var idrg = prosedur.ToRefference();
        _listProsedur.Add(new GrouperIdrgProsedurType(noUrut, idrg, 1,1));
    }
    public void Remove(IdrgProsedurType prosedur)
    {
        if (Phase == GroupingPhaseEnum.Final) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        _listProsedur.RemoveAll(x => x.Idrg == prosedur.ToRefference());
        ReArrangeNoUrut();
    }
    public void UbahNoUrutProsedur(IdrgProsedurType idrg, int targetNoUrut)
    {
        if (Phase == GroupingPhaseEnum.Final) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        //  rapihkan dahulu no urut (jangna ada yang lompat nomor)
        ReArrangeNoUrut();
        //  start shifting item
        targetNoUrut = Math.Max(1, Math.Min(targetNoUrut, _listProsedur.Count));
        var currentItem = _listProsedur.First(x => x.Idrg == idrg.ToRefference());
        _listProsedur.Remove(currentItem);
        _listProsedur.Insert(targetNoUrut - 1, currentItem);
        for (var i = 0; i < _listProsedur.Count; i++)
            _listProsedur[i].SetNoUrut(i + 1);
    }

    public void ChangeMulitiplicity(IdrgProsedurType idrg, int multiplicity)
    {
        if (Phase == GroupingPhaseEnum.Final) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        var currentItem = _listProsedur
            .FirstOrDefault(x => x.Idrg == idrg.ToRefference());
        if (currentItem is null) 
            throw new KeyNotFoundException($"Prosedur {idrg} tidak ditemukan");
        currentItem.ChangeMultiplicity(multiplicity);
    }

    public void ChangeSetting(IdrgProsedurType idrg, int setting)
    {
        if (Phase == GroupingPhaseEnum.Final) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        var currentItem = _listProsedur
            .FirstOrDefault(x => x.Idrg == idrg.ToRefference());
        if (currentItem is null) 
            throw new KeyNotFoundException($"Prosedur {idrg} tidak ditemukan");
        currentItem.ChangeSetting(setting);
    }
    #endregion
    
    #region GROUPING-RELATED METHOD

    public void Grouping(GrouperIdrgResultType hasilGrouping)
    { 
        if (Phase == GroupingPhaseEnum.Final)
            throw new InvalidOperationException("Grouper Idrg sudah final");

        HasilGrouping = hasilGrouping;

        if (hasilGrouping.Mdc == MdcType.Default 
            || hasilGrouping.Drg == DrgType.Default)
        {
            Phase = GroupingPhaseEnum.GroupingTapiGagal;
            return;
        }

        Phase = GroupingPhaseEnum.GroupingDanBerhasil;
    }

    public void Final()
    {
        if (Phase == GroupingPhaseEnum.Final)
            throw new InvalidOperationException("Grouper Idrg sudah final");
        
        if (Phase != GroupingPhaseEnum.GroupingDanBerhasil)
            throw new InvalidOperationException("Grouper Idrg belum grouping dan berhasil");
        
        Phase = GroupingPhaseEnum.Final;
        FinalTimestamp = DateTime.Now;
    }
    #endregion
    
    private void ReArrangeNoUrut()
    {
        var i = 1;
        foreach (var item in _listDiagnosa.OrderBy(x => x.NoUrut))
        {
            item.SetNoUrut(_listDiagnosa.IndexOf(item) + 1);
            i++;
        }

        i = 1;
        foreach(var item in _listProsedur.OrderBy(x => x.NoUrut))
        {
            item.SetNoUrut(_listProsedur.IndexOf(item) + 1);
            i++;
        }
    }

}