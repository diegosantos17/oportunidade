using Autofac;

namespace Minuto.Seguros.Service
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service") && !t.Name.EndsWith("BaseService"))
                .AsImplementedInterfaces();
            base.Load(builder);
        }
    }
}
