using Autofac;
using Minuto.Seguros.Infra.Data.Context;

namespace Minuto.Seguros.Infra.Data
{
    public class DataModule: Module
    {
        public readonly string _connectionString;
        public readonly string _database;

        public DataModule(string connectionString, string database)
        {
            _connectionString = connectionString;
            _database = database;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("Repository") && !t.Name.EndsWith("BaseRepository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();


            builder.Register(c => new MongoContext(_connectionString, _database))
              .As<MongoContext>().SingleInstance();

            base.Load(builder);
        }
    }
}
