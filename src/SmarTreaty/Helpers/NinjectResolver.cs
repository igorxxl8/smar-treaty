using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Extensions.ChildKernel;

namespace SmarTreaty.Helpers
{
    public class NinjectResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver() : this(new StandardKernel())
        {
        }

        public NinjectResolver(IKernel ninjectKernel, bool scope = false)
        {
            _kernel = ninjectKernel;
            if (!scope)
            {
                AddBindings(_kernel);
            }
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectResolver(AddRequestBindings(new ChildKernel(_kernel)), true);
        }

        private void AddBindings(IKernel kernel)
        {

        }

        private IKernel AddRequestBindings(IKernel kernel)
        {
            return kernel;
        }
    }
}