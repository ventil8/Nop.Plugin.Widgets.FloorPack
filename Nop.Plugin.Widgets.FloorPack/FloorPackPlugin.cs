using System.Collections.Generic;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Plugin.Widgets.FloorPack.Data;

namespace Nop.Plugin.Widgets.FloorPack
{
    /// <summary>
    /// Live person provider
    /// </summary>
    public class FloorPackPlugin : BasePlugin, IWidgetPlugin
    {
        public const string DEFAULT_ZONE = "productdetails_add_info";

        private readonly ISettingService _settingService;
        private readonly FloorPackSettings _floorPackSettings;
        private readonly FloorPackRecordObjectContext _objectContext;

        public FloorPackPlugin(ISettingService settingService,
            FloorPackSettings floorPackSettings,
            FloorPackRecordObjectContext objectContext)
        {
            _settingService = settingService;
            _floorPackSettings = floorPackSettings;
            _objectContext = objectContext;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return !string.IsNullOrWhiteSpace(_floorPackSettings.WidgetZone)
                ? new List<string>() { _floorPackSettings.WidgetZone }
                : new List<string>() { DEFAULT_ZONE };
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName,
            out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "FloorPack";
            routeValues = new RouteValueDictionary() 
            { 
                { "Namespaces", "Nop.Plugin.Widgets.FloorPack.Controllers" }, 
                { "area", null } 
            };
        }

        /// <summary>
        /// Gets a route for displaying widget
        /// </summary>
        /// <param name="widgetZone">Widget zone where it's displayed</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName,
            out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "FloorPack";
            routeValues = new RouteValueDictionary()
            {
                {"Namespaces", "Nop.Plugin.Widgets.FloorPack.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //settings
            var settings = new FloorPackSettings()
            {
                WidgetZone = DEFAULT_ZONE
            };
            _settingService.SaveSetting(settings);

            //this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.FloorPack.ButtonCode", "Button code(max 2000)");
            //this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.FloorPack.ButtonCode.Hint", "Enter your button code here.");
            //this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.FloorPack.LiveChat", "Live chat");
            //this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.FloorPack.MonitoringCode", "Monitoring code(max 2000)");
            //this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.FloorPack.MonitoringCode.Hint", "Enter your monitoring code here.");

            _objectContext.Install();
            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<FloorPackSettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.FloorPack.ButtonCode");
            this.DeletePluginLocaleResource("Plugins.Widgets.FloorPack.ButtonCode.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.FloorPack.LiveChat");
            this.DeletePluginLocaleResource("Plugins.Widgets.FloorPack.MonitoringCode");
            this.DeletePluginLocaleResource("Plugins.Widgets.FloorPack.MonitoringCode.Hint");

            base.Uninstall();
        }
    }
}
