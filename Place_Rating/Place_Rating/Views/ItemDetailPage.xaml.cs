using System.ComponentModel;
using Xamarin.Forms;
using Place_Rating.ViewModels;

namespace Place_Rating.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}