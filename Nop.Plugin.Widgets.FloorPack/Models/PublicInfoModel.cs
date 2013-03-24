using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.FloorPack.Models
{
    public class PublicInfoModel : BaseNopModel
    {
        public int ProductVariantId { get; set; }

        public decimal M2PerPack { get; set; }
    }
}