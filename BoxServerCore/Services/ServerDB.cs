using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoxProtocol.Models;
using BoxProtocol.Interfaces;
using Xamarin.Essentials;
using Nest;


namespace BoxServerCore.Services
{
    public class ServerDB : IDataStore
    {
        readonly List<Item> items;

        private static bool Initialized;
        private static ElasticClient _client;
        public static ElasticClient Client()
        {
            if (!Initialized)
            {
                var node = new Uri("http://127.0.0.1:9200");
                var settings = new ConnectionSettings(node);
                _client = new ElasticClient(settings);
            }
            return _client;
        }

        public ServerDB()
        {
            //var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            //Location location = Geolocation.GetLocationAsync(request).Result;
            var location = Geolocation.GetLastKnownLocationAsync().Result;

            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(),
                    Name = "First person",
                    Place_image_path = "isaak.jpg",
                    Place_name = "Исаакиевский собор",
                    Place_rating = "10/10",
                    Place_location = location,

                    Place_description="This is an item description.",
                    Time_created = DateTime.Now
                },
                new Item { Id = Guid.NewGuid().ToString(), 
                    Name = "Second person", 
                    Place_image_path = "Hermitage.jpg", 
                    Place_name = "Эрмитаж", 
                    Place_rating = "10/10", 
                    Place_location = location, 
                   //Place_location = "location",
                    Place_description="This is an item description.",
                    Time_created = DateTime.Now
                }
            };
        }

        public void Add<Item>(Item item) where Item : class, IHaveID
        {
            var client = Client();
            client.Index<Item>(item, idx =>
            {
                idx.Index(typeof(Item).Name);
                idx.Id(item.Id);
                return idx;
            });
        }
        /*public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }*/

        public void Update<Item>(Item updated) where Item : class, IHaveID
        {
            Client().Update<Item>(updated.Id, descriptor => descriptor.Doc(updated).Index(typeof(Item).Name));
        }
        /*public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }*/

        public void Delete<Item>(string id) where Item : class, IHaveID
        {
            Client().Delete<Item>(id, descriptor => descriptor.Index(typeof(Item).Name));
        }
        /*public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }*/

        public Item Get<Item>(string id) where Item : class, IHaveID
        {
            var doc = Client().Get<Item>(id, idx => idx.Index(typeof(Item).Name));
            return doc.Source;
        }
        public List<Item> GetAll<Item>() where Item : class, IHaveID
        {
            var docs = Client().Search<Item>();
            return docs.Documents.ToList();
        }      
        
        /*public async Task<Item> GetAll<Item>() where Item : class, IHaveID
        {
            var docs = await Client().Search<Item>();
            return docs.Documents.ToList();
        }*/

        /*public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }*/


        public List<Item> GetOnLocation<Item>(GeoLocation point) where Item : class, IHaveCoordinates
        {
            var docs = Client().Search<Item>(s => s
                .Query(query => query.GeoDistance(
                        n => n.Distance("1km").Location(point)
                    )
                )
            );
            return docs.Documents.ToList();
        }
        
    }
}


  