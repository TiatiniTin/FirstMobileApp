using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoxProtocol.Models;
using Xamarin.Essentials;
using MagicOnion;

namespace BoxProtocol.Interfaces
{
    public interface IServerDB : IService<IServerDB>
    {
        void Add<T>(T item) where T : class, IHaveID;
        void Update<T>(T updated) where T : class, IHaveID;
        void Delete<T>(string id) where T : class, IHaveID;
        T Get<T>(string id) where T : class, IHaveID;
        List<T> GetAll<T>() where T : class, IHaveID;
        List<T> GetOnLocation<T>(GeoLocation point) where T : class, IHaveCoordinates;
        //Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
