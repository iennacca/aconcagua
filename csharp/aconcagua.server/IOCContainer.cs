using infrastructure;
using Ninject;
using Ninject.Extensions.Conventions;

namespace aconcagua.server
{
    public static class IOCContainer
    {
        public static readonly ILogService Logger;

        static IOCContainer()
        {
            var kernel = new StandardKernel();

            kernel.Bind(x => x
                .FromAssembliesMatching("infrastructure*.dll")
                .SelectAllClasses()
                .BindAllInterfaces()
                .Configure(b => b.InSingletonScope()));

            var args = new Ninject.Parameters.ConstructorArgument("classType", typeof(IOCContainer));
            Logger = kernel.Get<ILogService>(args);        
        }
    }
}
