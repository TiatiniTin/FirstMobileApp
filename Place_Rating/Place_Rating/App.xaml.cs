using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Place_Rating.Services;
using Place_Rating.Views;

namespace Place_Rating
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
