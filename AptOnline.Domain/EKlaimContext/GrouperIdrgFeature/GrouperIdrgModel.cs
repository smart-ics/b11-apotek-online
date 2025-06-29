using AptOnline.Domain.EKlaimContext.IdrgFeature;
using AptOnline.Domain.SepContext.SepFeature;

namespace AptOnline.Domain.EKlaimContext.GrouperIdrgFeature;

public class GrouperIdrgModel
{
    private readonly List<GrouperIdrgDiagnosaType> _listDiagnosa;
    private readonly List<GrouperIdrgProsedurType> _listProsedur;

    public GrouperIdrgModel(string eKlaimId, SepRefference sep)
    {
        EKlaimId = eKlaimId;
        Sep = sep;
        HasilGrouping = GrouperIdrgResultType.Default;
        IsFinal = false;
        
        _listDiagnosa = new List<GrouperIdrgDiagnosaType>();
        _listProsedur = new List<GrouperIdrgProsedurType>();
    }
    public string EKlaimId { get; init; }
    public SepRefference Sep { get; init; }
    public bool IsFinal { get; private set; }
    public DateTime FinalTimestamp { get; private set; }
    public GrouperIdrgResultType HasilGrouping { get; private set;} 
    
    public IEnumerable<GrouperIdrgDiagnosaType> ListDiagnosa  => _listDiagnosa;
    public IEnumerable<GrouperIdrgProsedurType> ListProsedur => _listProsedur;

    #region DIAGNOSA-RELATED METHOD
    public void Add(IdrgDiagnosaType diagnosa)
    {
        if (IsFinal) 
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
        if (IsFinal) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        _listDiagnosa.RemoveAll(x => x.Idrg == diagnosa.ToRefference());
        ReArrangeNoUrut();
    }
    public void UbahNoUrutDiagnosa(IdrgDiagnosaType idrg, int targetNoUrut)
    {
        if (IsFinal) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        //  rapihkan dahulu no urut (jangna ada yang lompat nomor)
        ReArrangeNoUrut();
        //  start shifting item
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
        if (IsFinal) 
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
        if (IsFinal) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        _listProsedur.RemoveAll(x => x.Idrg == prosedur.ToRefference());
        ReArrangeNoUrut();
    }
    public void UbahNoUrutProsedur(IdrgProsedurType idrg, int targetNoUrut)
    {
        if (IsFinal) 
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
        if (IsFinal) 
            throw new InvalidOperationException("Grouper Idrg sudah final");

        var currentItem = _listProsedur
            .FirstOrDefault(x => x.Idrg == idrg.ToRefference());
        if (currentItem is null) 
            throw new KeyNotFoundException($"Prosedur {idrg} tidak ditemukan");
        currentItem.ChangeMultiplicity(multiplicity);
    }

    public void ChangeSetting(IdrgProsedurType idrg, int setting)
    {
        if (IsFinal) 
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
        => HasilGrouping = hasilGrouping;

    public void Final()
    {
        IsFinal = true;
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