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
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private async void BtnAgregarUsuario_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new UserSingUpPage());
        }

        private async void BtnModificarUsuario_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ModificarUsuarioPage());
        }

        private async void BtnPuntajes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PuntajesPage());
        }

        private async void BtnCerrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AppLoginPage());
        }
    }
}