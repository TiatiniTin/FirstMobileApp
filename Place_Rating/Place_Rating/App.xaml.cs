using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BoxProtocol.Interfaces;
using Place_Rating.Views;

namespace Place_Rating
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IServerDB>();
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
