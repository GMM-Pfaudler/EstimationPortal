using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace EstimationPortal.Installers
{
    using Castle.Facilities.Logging;
    using Castle.Services.Logging.Log4netIntegration;
    using SUP_BAL.IRepository;
    using SUP_BAL.Repository;
    using SUP_DAL.EFContextProvider;
    using CustomeAttribute;
    using Plumbing;
    using System;
    using EstimationPortal.CustomeAttribute;

    //using SUP_BAL.BAL;

    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.
                    FromThisAssembly().
                    BasedOn<IController>().
                    If(c => c.Name.EndsWith("Controller")).
                    LifestyleTransient())
                      .Register(Component.For<DropDownCollection>().LifestyleTransient());
                      //.Register(Component.For<InvoiceBAL>().LifestyleTransient())
                      //.Register(Component.For<PMSCBAL>().LifestyleTransient())
                      //.Register(Component.For<SuppBAL>().LifestyleTransient())
                      //.Register(Component.For<VRFBAL>().LifestyleTransient())
                      //.Register(Component.For<PODetailsBAL>().LifestyleTransient());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}