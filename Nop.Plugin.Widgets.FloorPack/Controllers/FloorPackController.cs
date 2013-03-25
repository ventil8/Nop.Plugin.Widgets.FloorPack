using System.Web.Mvc;
using Nop.Plugin.Widgets.FloorPack.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Controllers;
using Nop.Services.Catalog;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.FloorPack.Services;
using Nop.Plugin.Widgets.FloorPack.Domain;
using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.Customers;

namespace Nop.Plugin.Widgets.FloorPack.Controllers
{
    public class FloorPackController : Controller
    {
        private readonly FloorPackSettings _floorPackSettings;
        private readonly ISettingService _settingService;
        private readonly IProductService _productService;
        private readonly IFloorPackService _floorPackService;
        private readonly IPriceFormatter _priceFormatter;

        public FloorPackController(FloorPackSettings floorPackSettings,
            ISettingService settingService,
            IProductService productService,
            IFloorPackService floorPackService,
            IPriceFormatter priceFormatter)
        {
            _floorPackSettings = floorPackSettings;
            _settingService = settingService;
            _productService = productService;
            _floorPackService = floorPackService;
            _priceFormatter = priceFormatter;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new ConfigurationModel();

            model.ZoneId = _floorPackSettings.WidgetZone;

            return View("Nop.Plugin.Widgets.FloorPack.Views.Configure", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            //save settings
            _floorPackSettings.WidgetZone = model.ZoneId;
            _settingService.SaveSetting(_floorPackSettings);

            return Configure();
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone)
        {
            PublicInfoModel model = new PublicInfoModel();
            int productId = Convert.ToInt32(Request.RequestContext.RouteData.Values["productId"]);
            Product product = _productService.GetProductById(productId);
            ProductVariant productVariant = product.ProductVariants.FirstOrDefault();
            FloorPackRecord record = _floorPackService.GetByProductVariantId(productVariant.Id);

            if (record == null || !record.M2PerPack.HasValue || record.M2PerPack.Value <= 0)
                return null;

            model.M2PerPack = record.M2PerPack.Value;
            model.ProductVariantId = record.ProductVariantId;

            return View("Nop.Plugin.Widgets.FloorPack.Views.PublicInfo", model);
        }

        /// <summary>
        /// AJAX method
        /// </summary>
        [HttpPost]
        public ActionResult Calculate(int productVariantId, decimal area)
        {
            ProductVariant productVariant = _productService.GetProductVariantById(productVariantId);
            FloorPackRecord floorPack = _floorPackService.GetByProductVariantId(productVariantId);

            int packCount = Convert.ToInt32(Math.Ceiling(area / floorPack.M2PerPack.Value));
            decimal m2Count = packCount * floorPack.M2PerPack.Value;
            decimal unitPriceBase = productVariant.Price;
            string unitPriceFormatted = _priceFormatter.FormatPrice(unitPriceBase);
            decimal totalPriceBase = productVariant.Price * m2Count;
            string totalPriceFormatted = _priceFormatter.FormatPrice(totalPriceBase);

            return Json(new
            {
                successful = true,
                calculationResult = string.Format("You Require {0} Packs ({1}m2) at {2} per m2",
                    packCount, m2Count, unitPriceFormatted),
                totalPrice = string.Format("Total Price: {0}", totalPriceFormatted)
            });
        }

        public ActionResult AdminEditor(int productVariantId)
        {
            FloorPackRecord record = _floorPackService.GetByProductVariantId(productVariantId);

            if (record == null)
                record = new FloorPackRecord()
                {
                    ProductVariantId = productVariantId,
                    M2PerPack = 0
                };

            return View("Nop.Plugin.Widgets.FloorPack.Views.AdminEditor", record);
        }
    }
}
