using System;
using System.Collections.Generic;
using Place_Rating.ViewModels;
using Place_Rating.Views;
using Xamarin.Forms;

namespace Place_Rating
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
