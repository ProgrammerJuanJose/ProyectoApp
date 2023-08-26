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
    public partial class ModificarUsuarioPage : ContentPage
    {

        UserViewModel viewModel;
        public ModificarUsuarioPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UserViewModel();

            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            TxtID.Text = "1";
            TxtEmail.Text = "jjchaconarce@gmail.com";
            TxtName.Text = "Juan José";
            TxtPhoneNumber.Text = "8911-9539";
            TxtCedula.Text = "2-0825-0930";
            TxtAddress.Text = "San Miguel";
        }

        private bool ValidateFields()
        {
            bool R = false;

            if (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim()) &&
                TxtCedula.Text != null && !string.IsNullOrEmpty(TxtCedula.Text.Trim()) &&
                TxtPhoneNumber.Text != null && !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
            {
                //en este caso están todos los datos requeridos 

                R = true;
            }
            else
            {
                //si falta algún  dato obligatorio 
                if (TxtName.Text == null || string.IsNullOrEmpty(TxtName.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "The Name is required", "OK");
                    TxtName.Focus();
                    return false;
                }

                if (TxtCedula.Text == null || string.IsNullOrEmpty(TxtCedula.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "The cedula is required", "OK");
                    TxtCedula.Focus();
                    return false;
                }

                if (TxtPhoneNumber.Text == null || string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "The Phone Number is required", "OK");
                    TxtPhoneNumber.Focus();
                    return false;
                }

            }

            return R;
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            //primero hacemos validación de campos requeridos 

            if (ValidateFields())
            {
                //sacar un respaldo del usuario global tal y como está en este momento 
                //por si algo sale mal en el proceso de update, reversar los cambios 
                Usuario BackupLocaluser = new Usuario();
                BackupLocaluser = GlobalObjects.MyLocalUser;

                var answer = await DisplayAlert("???", "Are you sure to continue updating user info?", "Yes", "No");

                if (answer)
                {
                    await DisplayAlert(":)", "User Updated!!!", "OK");
                    await Navigation.PopModalAsync();
                }



            }
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new StartPage());
        }
    }
}