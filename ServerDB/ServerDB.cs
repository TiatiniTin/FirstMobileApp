using System;
using System.Collections.Generic;
using System.Linq;
using BoxProtocol.DB;
using Nest;

namespace BoxServerCore
{
    public static class ServerDB
    {
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
        public static void Store<T>(T item) where T : class, IHaveId
        {
            var client = Client();
            client.Index<T>(item, idx =>
            {
                idx.Index(typeof(T).Name);
                idx.Id(item.Id);
                return idx;
            });
        }
        public static T Get<T>(string id) where T : class, IHaveId
        {
            var doc = Client().Get<T>(id, idx => idx.Index(typeof(T).Name));
            return doc.Source;
        }

        public static List<T> GetOnLocation<T>(GeoLocation point) where T : class, IHaveCoordinates
        {
            var docs = Client().Search<T>(s => s
                .Query(query => query.GeoDistance(
                        n => n.Distance("1km").Location(point)
                    )
                )
            );
            return docs.Documents.ToList();
        }
        public static void Update<T>(T updated) where T : class, IHaveId
        {
            Client().Update<T>(updated.Id, descriptor => descriptor.Doc(updated).Index(typeof(T).Name));
        }
    }
}