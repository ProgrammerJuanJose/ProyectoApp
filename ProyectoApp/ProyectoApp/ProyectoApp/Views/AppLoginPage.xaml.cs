using Acr.UserDialogs;
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
    public partial class AppLoginPage : ContentPage
    {
        UserViewModel viewModel;
        public AppLoginPage()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new UserViewModel();
        }

        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                TxtPassword.IsPassword = false;
            }
            else
            {
                TxtPassword.IsPassword = true;
            }
        }


        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new StartPage());
            //await Navigation.PushAsync(new NavigationPage(new StartPage()));
            await Navigation.PushModalAsync(new UserSingUpPage());
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            //validación del ingreso del usuario a la app 

            if (TxtUserName.Text != null && !string.IsNullOrEmpty(TxtUserName.Text.Trim()) &&
                TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
            {
                //si hay info en los cuadros de texto de email y pass se procede 
                try
                {
                    string username = TxtUserName.Text.Trim();
                    string password = TxtPassword.Text.Trim();

                    bool R = await viewModel.UserAccessValidation(username, password);

                    if (R)
                    {
                        //si la validación es correcta se permite el ingreso al sistema 
                        //igual que el progra 5 vamos a tener un usuario global 

                        GlobalObjects.MyLocalUser = await viewModel.GetUserDataAsync(TxtUserName.Text.Trim());

                        //await Navigation.PushAsync(new StartPage());
                        //await Navigation.PushAsync(new NavigationPage(new StartPage()));
                        await Navigation.PushModalAsync(new StartPage());
                        return;
                    }
                    else
                    {
                        //algo salió mal 

                        await DisplayAlert("User Access Denied", "Username or Password are incorrect", "OK");
                        return;
                    }


                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    //apagamos la animación de carga 
                }


            }
            else
            {
                //si no digito datos indicarle al usuario del requerimiento 

                await DisplayAlert("Data required", "Username and Password are required...", "OK");
                return;
            }
        }
    }
}