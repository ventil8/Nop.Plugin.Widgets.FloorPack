using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.FloorPack.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public ConfigurationModel()
        {
            AvailableZones = new List<SelectListItem>();

            AvailableZones.Add(new SelectListItem()
            {
                Text = FloorPackPlugin.DEFAULT_ZONE,
                Value = FloorPackPlugin.DEFAULT_ZONE
            });
        }

        [NopResourceDisplayName("Admin.ContentManagement.Widgets.ChooseZone")]
        public string ZoneId { get; set; }
        public IList<SelectListItem> AvailableZones { get; set; }
    }
}