using ProyectoApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ProyectoApp.Views
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