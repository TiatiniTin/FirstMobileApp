using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoxProtocol.Models;
using Xamarin.Essentials;

namespace BoxProtocol.Interfaces
{
    public interface IHaveID
    {
        string Id { get; set; }
    }
    public interface IHaveCoordinates
    {
        Location Place_location { get; set; }
    }
    public interface IDataStore
    {
        //void Add(T item);
        void Add<T>(T item) where T : class, IHaveID;
        //void Update(T item);
        void Update<T>(T updated) where T : class, IHaveID;
        //void Delete(string id);
        void Delete<T>(string id) where T : class, IHaveID;
        T Get<T>(string id) where T : class, IHaveID;
        //Item Get(string id);
        //List<T> GetAll(string id);
        List<T> GetAll<T>() where T : class, IHaveID;
        List<T> GetOnLocation<T>(GeoLocation point) where T : class, IHaveCoordinates;
       // List<T> GetOnLocation(GeoLocation point);

        //Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
