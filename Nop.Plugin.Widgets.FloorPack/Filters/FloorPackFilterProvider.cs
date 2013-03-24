using Nop.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.FloorPack.Filters
{
    public class FloorPackFilterProvider : IFilterProvider
    {
        FloorPackFilterAttribute _actionFilter;

        public FloorPackFilterProvider(FloorPackFilterAttribute actionFilter)
        {
            _actionFilter = actionFilter;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            if ((actionDescriptor.ControllerDescriptor.ControllerType ==
                    typeof(ProductController) &&
                (actionDescriptor.ActionName.Equals("Create", 
                    StringComparison.InvariantCultureIgnoreCase) ||
                actionDescriptor.ActionName.Equals("Edit", 
                    StringComparison.InvariantCultureIgnoreCase)) &&
                controllerContext.HttpContext.Request.HttpMethod == "POST") ||
                (actionDescriptor.ControllerDescriptor.ControllerType ==
                    typeof(ProductVariantController) &&
                (actionDescriptor.ActionName.Equals("Create",
                    StringComparison.InvariantCultureIgnoreCase) ||
                actionDescriptor.ActionName.Equals("Edit",
                    StringComparison.InvariantCultureIgnoreCase)) &&
                controllerContext.HttpContext.Request.HttpMethod == "POST"))
            {
                return new Filter[] 
                { 
                    new Filter(_actionFilter, FilterScope.Action, null)
                };
            };

            return new Filter[] { };
        }
    }
}