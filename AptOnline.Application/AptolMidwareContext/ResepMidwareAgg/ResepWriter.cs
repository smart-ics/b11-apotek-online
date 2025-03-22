namespace AptOnline.Application.AptolMidwareContext.ResepMidwareAgg
{
    public interface IResepWriter
    {
        public ResepModel Save(ResepModel resep);
        public ResepItemModel SaveItem(ResepItemModel obat);
    }
    public class ResepWriter: IResepWriter
    {
        private readonly IResepDal _resepDal;
        private readonly IResepItemDal _resepItemDal;

        public ResepWriter(IResepDal resepDal, IResepItemDal resepItemDal)
        {
            _resepDal = resepDal;
            _resepItemDal = resepItemDal;
        }

        public ResepModel Save(ResepModel model)
        {
            var savedResep = _resepDal.GetData(model.PenjualanId);
            bool success;
            if (savedResep is not null)
                success = _resepDal.Insert(model);
            else
                success = _resepDal.Update(model);
            if (success)
                return model;
            else
                return null;

        }

        public ResepItemModel SaveItem(ResepItemModel item)
        {
            if (_resepItemDal.Delete(item.PenjualanId, item.BarangId))
            {
                _resepItemDal.Insert(item);
                return item;
            }
            return null;
        }
    }
}
