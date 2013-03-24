using Nop.Core.Data;
using Nop.Plugin.Widgets.FloorPack.Domain;
using System.Linq;

namespace Nop.Plugin.Widgets.FloorPack.Services
{
    public class FloorPackService : IFloorPackService
    {
        private readonly IRepository<FloorPackRecord> _floorPackRecordRepository;

        public FloorPackService(IRepository<FloorPackRecord> floorPackRecordRepository)
        {
            _floorPackRecordRepository = floorPackRecordRepository;
        }

        public FloorPackRecord GetById(int id)
        {
            return _floorPackRecordRepository.Table.SingleOrDefault(f => f.Id == id);
        }

        public FloorPackRecord GetByProductVariantId(int productVariantId)
        {
            return _floorPackRecordRepository.Table
                .SingleOrDefault(f => f.ProductVariantId == productVariantId);
        }

        public void Insert(FloorPackRecord floorPack)
        {
            _floorPackRecordRepository.Insert(floorPack);
        }

        public void Update(FloorPackRecord floorPack)
        {
            _floorPackRecordRepository.Update(floorPack);
        }

        public void Delete(FloorPackRecord floorpack)
        {
            _floorPackRecordRepository.Delete(floorpack);
        }
    }
}
