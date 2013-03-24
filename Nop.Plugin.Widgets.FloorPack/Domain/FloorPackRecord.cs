using Nop.Core;

namespace Nop.Plugin.Widgets.FloorPack.Domain
{
    public class FloorPackRecord : BaseEntity
    {
        public virtual int ProductVariantId { get; set; }

        public virtual decimal? M2PerPack { get; set; }
    }
}
