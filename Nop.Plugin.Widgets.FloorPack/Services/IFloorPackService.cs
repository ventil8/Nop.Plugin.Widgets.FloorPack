using Nop.Plugin.Widgets.FloorPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Widgets.FloorPack.Services
{
    public interface IFloorPackService
    {
        FloorPackRecord GetById(int id);

        FloorPackRecord GetByProductVariantId(int productVariantId);

        void Insert(FloorPackRecord floorPack);

        void Update(FloorPackRecord floorPack);

        void Delete(FloorPackRecord floorpack);
    }
}
