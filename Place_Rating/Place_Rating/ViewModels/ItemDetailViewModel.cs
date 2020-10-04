using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BoxProtocol.Models;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Place_Rating.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string place_name;
        private string place_description;
        private string place_rating;
        private Location place_location;
        //private string place_location;
        private string place_image_path;
        private DateTime time_created;
        private string name;

        public string Id { get; set; }

        public string Place_name
        {
            get => place_name;
            set => SetProperty(ref place_name, value);
        }

        public string Place_description
        {
            get => place_description;
            set => SetProperty(ref place_description, value);
        }
        public string Place_rating
        {
            get => place_rating;
            set => SetProperty(ref place_rating, value);
        }        
        public DateTime Time_created
        {
            get => time_created;
            set => SetProperty(ref time_created, DateTime.Now);
        }
        public Location Place_location
        {
            get => place_location;
            set => SetProperty(ref place_location, get_location());
        }

        public Location get_location()
        {
            //string unkn_loc = "unknown location";
            try
            {
                var location = Geolocation.GetLastKnownLocationAsync().Result;
                //var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                //Location location = Geolocation.GetLocationAsync(request).Result;

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    return location;
                }
            }
            catch (FeatureNotSupportedException )
            {
                // Handle not supported on device exception

            }
            catch (FeatureNotEnabledException )
            {
                // Handle not enabled on device exception

            }
            catch (PermissionException )
            {
                // Handle permission exception

            }
            catch (Exception )
            {
                // Unable to get location

            }
            return null;
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Place_image_path
        {
            get => place_image_path;
            set => SetProperty(ref place_image_path, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public void LoadItemId(string itemId)
        {
            try
            {
                var item =  DataStore.Get<Item>(itemId);
                Id = item.Id;
                Name = item.Name;
                Place_name = item.Place_name;
                Place_location = item.Place_location;
                Place_description = item.Place_description;
                Place_rating = item.Place_rating;
                Place_image_path = item.Place_image_path;
                Time_created = item.Time_created;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
