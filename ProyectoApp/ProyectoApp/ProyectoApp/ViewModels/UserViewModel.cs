using ProyectoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public Usuario MiUsuario { get; set; }

        public UsuarioTipo MiTipoU { get; set; }

        public UserViewModel()
        {
            MiUsuario = new Usuario();
            MiTipoU = new UsuarioTipo();
        }

        //Funciones

        public async Task<Usuario> GetUserDataAsync(string pCorreo)
        {
            if (IsBusy) return null;
            IsBusy = true;

            try
            {
                Usuario usuario = new Usuario();

                usuario = await MiUsuario.GetUserInfo(pCorreo);

                if (usuario == null) return null;

                return usuario;

            }
            catch (Exception)
            {
                return null;
                throw;
            }
            finally { IsBusy = false; }


        }

        public async Task<List<UsuarioTipo>> GetUserTypesAsync()
        {
            try
            {
                List<UsuarioTipo> tipos = new List<UsuarioTipo>();

                tipos = await MiTipoU.GetAllUserRolesAsync();

                if (tipos == null)
                {
                    return null;
                }

                return tipos;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AddUserAsync(string pCorreo,
                                             string pContrasennia,
                                             string pNombre,
                                             string pCedula,
                                             string pTelefono,
                                             string pDireccion,
                                             int pUsuarioTipoId)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                // MyUser = new User();

                MiUsuario.UsuarioCorreo = pCorreo;
                MiUsuario.UsuarioContrasennia = pContrasennia;
                MiUsuario.UsuarioNombre = pNombre;
                MiUsuario.UsuarioCedula = pCedula;
                MiUsuario.UsuarioTelefono = pTelefono;
                MiUsuario.UsuarioDireccion = pDireccion;
                MiUsuario.UsuarioTipoId = pUsuarioTipoId;

                bool R = await MiUsuario.AddUserAsync();

                return R;

            }
            catch (Exception)
            {

                throw;

            }
            finally { IsBusy = false; }

        }

        public async Task<bool> UpdateUser(Usuario pUser)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MiUsuario = pUser;

                bool R = await MiUsuario.UpdateUserAsync();

                return R;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally { IsBusy = false; }
        }

        public async Task<bool> UserAccessValidation(string pCorreo, string pContrasennia)
        {
            //debemos poder controlar que no se ejecute la operación más de una vez 
            //en este caso hay una funcionalidad pensada para eso en BaseViewModel que 
            //fue heredada al definir esta clase. 
            //Se usará una propiedad llamada "IsBusy" para indicar que está en proceso de ejecución
            //mientras su valor sea verdadero 

            //control de bloqueo de funcionalidad 
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MiUsuario.UsuarioCorreo = pCorreo;
                MiUsuario.UsuarioContrasennia = pContrasennia;

                bool R = await MiUsuario.ValidateUserLogin();

                return R;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                return false;

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
