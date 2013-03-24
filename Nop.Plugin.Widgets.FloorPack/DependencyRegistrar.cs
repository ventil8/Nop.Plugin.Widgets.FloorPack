using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Widgets.FloorPack.Data;
using Nop.Plugin.Widgets.FloorPack.Domain;
using Nop.Plugin.Widgets.FloorPack.Filters;
using Nop.Plugin.Widgets.FloorPack.Services;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.FloorPack
{
    /// <summary>
    /// Register for the required dependencies
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_floor_pack";

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            var dataSettingsManager = new DataSettingsManager();
            DataSettings dataSettings = dataSettingsManager.LoadSettings();

            builder.Register<IDbContext>(c => RegisterIDbContext(c, dataSettings))
                .Named<IDbContext>(CONTEXT_NAME).InstancePerHttpRequest();
            builder.Register(c => RegisterIDbContext(c, dataSettings))
                .InstancePerHttpRequest();

            builder.RegisterType<EfRepository<FloorPackRecord>>().As<IRepository<FloorPackRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerHttpRequest();

            builder.RegisterType<FloorPackService>().As<IFloorPackService>()
                .InstancePerHttpRequest();
            builder.RegisterType<FloorPackFilterAttribute>().InstancePerHttpRequest();
            builder.RegisterType<FloorPackFilterProvider>().As<IFilterProvider>()
                .InstancePerHttpRequest();
        }

        public int Order
        {
            get { return 0; }
        }

        private FloorPackRecordObjectContext RegisterIDbContext(
            IComponentContext componentContext, DataSettings dataSettings)
        {
            string dataConnectionStrings;

            if (dataSettings != null && dataSettings.IsValid())
                dataConnectionStrings = dataSettings.DataConnectionString;
            else
                dataConnectionStrings = 
                    componentContext.Resolve<DataSettings>().DataConnectionString;

            return new FloorPackRecordObjectContext(dataConnectionStrings);
        }
    }
}
