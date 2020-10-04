using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using BoxProtocol.Models;
using BoxProtocol.Interfaces;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Nest;
using System.IO;
using MagicOnion.Client;
using Grpc.Core;

namespace Place_Rating.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string place_name;
        private string place_description;
        private string place_rating;
        private Location place_location;
        private string place_image_path;
        private DateTime time_created;
        private string name;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(place_name)
                && !String.IsNullOrWhiteSpace(place_description)
                && !String.IsNullOrWhiteSpace(place_rating)
                && !String.IsNullOrWhiteSpace(place_image_path)
                && !String.IsNullOrWhiteSpace(name);
        }

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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Place_name = Place_name,
                Place_location = Place_location,
                Place_rating = Place_rating,
                Place_description = Place_description,
                Place_image_path = Place_image_path,
                Time_created = Time_created
            };

            //await DataStore.AddItemAsync(newItem);

            var channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);
            var client = MagicOnionClient.Create<IServerDB>(channel);
            await client.Add(newItem);
   

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
