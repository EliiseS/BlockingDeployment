using MediatR;
using StructureMap;

namespace WebApplication1.IoC
{
    public class RuntimeRegistry : Registry
    {
        public RuntimeRegistry()
        {
            Scan(x =>
            {
                x.Assembly(GetType().Assembly);
                x.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>)); // Handlers with no response
                x.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>)); // Handlers with a response
                x.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<>)); // Async handlers with no response
                x.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>)); // Async Handlers with a response
                x.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                x.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
                //x.AddAllTypesOf(typeof(IRequestHandler<,>));
                //x.AddAllTypesOf(typeof(IAsyncRequestHandler<,>));
                //x.AddAllTypesOf(typeof(INotificationHandler<>));
                //x.AddAllTypesOf(typeof(IAsyncNotificationHandler<>));
                x.AssembliesAndExecutablesFromApplicationBaseDirectory();
                x.TheCallingAssembly();
                x.LookForRegistries();
                x.WithDefaultConventions();
            });

            //Singleton
            //For(typeof(IRepository)).Singleton().Use(typeof(Repository));
            //For<IExceptionHandler>().Singleton().Use<ExceptionHandler>();
            //For<ILoggerFactory>().Singleton().Use<LoggerFactory>();
            //For(typeof(ILogger<>)).Singleton().Add(typeof(Logger<>));
            //For<ILogManager>().Singleton().Use<LogManager>();

            //Setting up Mediatr
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
            For<IMediator>().Use<Mediator>();
        }
    }
}