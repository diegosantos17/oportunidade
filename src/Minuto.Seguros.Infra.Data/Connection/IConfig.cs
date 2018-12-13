using System;
using System.Collections.Generic;
using System.Text;

namespace Minuto.Seguros.Infra.Data.Connection
{
    public interface IConfig
    {
        string MongoConnectionString { get; }
        string MongoDatabase { get; }
    }
}
