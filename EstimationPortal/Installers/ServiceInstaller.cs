using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.Services.Logging.Log4netIntegration;
using SUP_BAL.IRepository;
using SUP_BAL.Repository;
using SUP_DAL.EFContextProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstimationPortal.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(f => f.LogUsing<Log4netFactory>().WithConfig(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            container.Register(Component.For<SupplierDbContext>().DependsOn(Dependency.OnValue<string>("DefaultConnection")).LifestyleTransient());
            container.Register(Component.For(typeof(IGenericRepository<>)).ImplementedBy(typeof(GenericRepository<>)).LifeStyle.Transient);
        }
    }
}