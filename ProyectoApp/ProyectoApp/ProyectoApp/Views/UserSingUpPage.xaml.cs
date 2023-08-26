using ProyectoApp.Models;
using ProyectoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSingUpPage : ContentPage
    {
        UserViewModel viewModel;
        public UserSingUpPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UserViewModel();

            LoadUserTypesAsync();
        }

        private async void LoadUserTypesAsync()
        {
            PkrUsuarioTipo.ItemsSource = await viewModel.GetUserTypesAsync();
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert(":)", "User created Ok!", "OK");
            await Navigation.PopModalAsync();
        }
    }
}