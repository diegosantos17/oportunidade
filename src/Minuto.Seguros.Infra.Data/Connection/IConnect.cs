using MongoDB.Driver;
using System;

namespace Minuto.Seguros.Infra.Data.Connection
{
    public interface IConnect : IDisposable
    {
        IMongoCollection<T> Collection<T>(string CollectionName);
    }
}
