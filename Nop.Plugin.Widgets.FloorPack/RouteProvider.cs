using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Widgets.FloorPack
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.Widgets.FloorPack.Configure",
                 "Plugins/WidgetsFloorPack/Configure",
                 new { controller = "FloorPack", action = "Configure" },
                 new[] { "Nop.Plugin.Widgets.FloorPack.Controllers" }
            );

            routes.MapRoute("Plugin.Widgets.FloorPack.PublicInfo",
                 "Plugins/WidgetsFloorPack/PublicInfo",
                 new { controller = "FloorPack", action = "PublicInfo" },
                 new[] { "Nop.Plugin.Widgets.FloorPack.Controllers" }
            );

            routes.MapRoute("Plugin.Widgets.FloorPack.AdminEditor",
                 "Plugins/WidgetsFloorPack/AdminEditor/{productVariantId}",
                 new { controller = "FloorPack", action = "AdminEditor" },
                 new[] { "Nop.Plugin.Widgets.FloorPack.Controllers" }
            );

            routes.MapRoute("Plugin.Widgets.FloorPack.Calculate",
                 "Plugins/WidgetsFloorPack/Calculate",
                 new { controller = "FloorPack", action = "Calculate" },
                 new[] { "Nop.Plugin.Widgets.FloorPack.Controllers" }
            );
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
