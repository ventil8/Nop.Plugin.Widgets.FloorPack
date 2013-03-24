using Nop.Admin.Controllers;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.FloorPack.Domain;
using Nop.Plugin.Widgets.FloorPack.Services;
using Nop.Services.Catalog;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.FloorPack.Filters
{
    public class FloorPackFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is RedirectToRouteResult)
            {
                RedirectToRouteResult result = filterContext.Result as RedirectToRouteResult;
                IProductService productService = EngineContext.Current.Resolve<IProductService>();
                //ProductVariant productVariant = null;
                int productVariantId = 0;

                if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerType ==
                        typeof(ProductController) &&
                    filterContext.ActionDescriptor.ActionName.Equals("Create",
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    Product product = null;

                    if (result.RouteValues.ContainsKey("Id"))
                    {
                        int productId = Convert.ToInt32(result.RouteValues["Id"]);
                        product = productService.GetProductById(productId);
                    }
                    else
                    {
                        product = productService.GetAllProducts(true)
                            .OrderByDescending(p => p.Id).FirstOrDefault();
                    }

                    if (product != null)
                        productVariantId = product.ProductVariants.FirstOrDefault().Id;
                        //productVariant = product.ProductVariants.FirstOrDefault();
                }
                else if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerType ==
                    typeof(ProductVariantController))
                {
                    if (filterContext.ActionDescriptor.ActionName.Equals("Create",
                            StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (result.RouteValues.ContainsKey("Controller") &&
                            result.RouteValues["Controller"].ToString()
                                .Equals("Product", StringComparison.InvariantCultureIgnoreCase))
                        {
                            int productId = Convert.ToInt32(result.RouteValues["Id"]);
                            Product product = productService.GetProductById(productId);
                            productVariantId = product.ProductVariants.OrderByDescending(pv => pv.Id)
                                .FirstOrDefault().Id;
                            //productVariant = product.ProductVariants.OrderByDescending(pv => pv.Id)
                            //    .FirstOrDefault();
                        }
                        else
                        {
                            productVariantId = Convert.ToInt32(result.RouteValues["Id"]);
                            //productVariant = productService.GetProductVariantById(productVariantId);
                        }
                    }
                    else if (filterContext.ActionDescriptor.ActionName.Equals("Edit",
                            StringComparison.InvariantCultureIgnoreCase))
                    {
                        var requestRouteVales = filterContext.Controller.ControllerContext
                            .RouteData.Values;
                        productVariantId = Convert.ToInt32(requestRouteVales["Id"]);
                        //productVariant = productService.GetProductVariantById(productVariantId);
                    }
                }

                if (productVariantId > 0)
                {
                    IFloorPackService floorPackService = EngineContext.Current
                        .Resolve<IFloorPackService>();

                    FloorPackRecord record = floorPackService.GetByProductVariantId(productVariantId);
                    NameValueCollection form = filterContext.HttpContext.Request.Form;
                    decimal m2PerPack = 0;
                    decimal.TryParse(form["M2 Per Pack:"], out m2PerPack);

                    if (record == null)
                        record = new FloorPackRecord();

                    record.ProductVariantId = productVariantId;
                    record.M2PerPack = m2PerPack;

                    if (record.Id == 0)
                        floorPackService.Insert(record);
                    else
                        floorPackService.Update(record);
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
