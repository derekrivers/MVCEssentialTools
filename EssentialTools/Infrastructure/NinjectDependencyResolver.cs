using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;
using Ninject.Web.Common;

namespace EssentialTools.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public void AddBindings()
        {
            kernel.Bind<IValueCalculator>()
                .To<LinqValueCalculator>()
                .InRequestScope();

            kernel.Bind<IDiscountHelper>()
                .To<DiscountHelper>()
                .WithConstructorArgument("discountParam", 50M);

            kernel.Bind<IDiscountHelper>()
                .To<FlexibleDiscountHelper>()
                .WhenInjectedInto<LinqValueCalculator>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}