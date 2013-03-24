using Nop.Plugin.Widgets.FloorPack.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Widgets.FloorPack.Data
{
    public class FloorPackRecordMap : EntityTypeConfiguration<FloorPackRecord>
    {
        public FloorPackRecordMap()
        {
            ToTable("FloorPack");
            HasKey(m => m.Id);
            Property(m => m.ProductVariantId);
            Property(m => m.M2PerPack).IsOptional();

        }
    }
}
