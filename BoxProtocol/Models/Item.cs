using BoxProtocol.Interfaces;
using System;
using Xamarin.Essentials;


namespace BoxProtocol.Models
{
    public class Item : IHaveID, IHaveCoordinates
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Place_name { get; set; }
        public Location Place_location { get; set; }
        public string Place_rating { get; set; }
        public string Place_description { get; set; }
        public string Place_image_path { get; set; }
        public DateTime Time_created { get; set; }

    }
}