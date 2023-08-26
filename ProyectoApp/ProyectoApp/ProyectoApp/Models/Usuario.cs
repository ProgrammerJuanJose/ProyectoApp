using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace ProyectoApp.Models
{
    public class Usuario
    {
        [Newtonsoft.Json.JsonIgnore]
        public RestRequest Request { get; set; }
        public int UsuarioId { get; set; }

        public string UsuarioCorreo { get; set; } = null!;

        public string UsuarioContrasennia { get; set; } = null!;

        public string UsuarioNombre { get; set; } = null!;

        public string UsuarioCedula { get; set; } = null!;

        public string? UsuarioTelefono { get; set; }

        public string? UsuarioDireccion { get; set; }

        public bool? Activo { get; set; }

        public int UsuarioTipoId { get; set; }
        public virtual UsuarioTipo UsuarioTipo { get; set; } = null!;


        //Funciones especificas de llamada a end points del API 

        //función que permite validar que los datos digitados en la pagina de 
        //applogin sean correctos o no. 
        public async Task<bool> ValidateUserLogin()
        {
            try
            {
                //usaremos el prefijo de la ruta URL del API que se indica en 
                //services\APIConnection para agregar el sufijo y lograr la ruta 
                //completa de consumo del end point que se quiere usar. 

                string RouteSufix = string.Format("Usuarios/ValidateLogin?Correo={0}&Contrasennia={1}",
                                                                       this.UsuarioCorreo, this.UsuarioContrasennia);
                //armamos la ruta completa al endpoint en el API 
                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //agregamos mecanismo de seguridad, en este caso API key
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);

                //ejecutar la llamada al API 
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien 
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

        }

        public async Task<Usuario> GetUserInfo(string PUsuarioCorreo)
        {

            try
            {
                string RouteSufix = string.Format("Users/GetUserInfoByEmail?Pcorreo={0}", PUsuarioCorreo);
                //armamos la ruta completa al endpoint en el API 
                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //agregamos mecanismo de seguridad, en este caso API key
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //ejecutar la llamada al API 
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien 
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    //en el api diseñamos el end point de forma que retorne un list<UserDTO>
                    //pero esta función retorna solo UN objeto de UserDTO, por lo tanto 
                    //debemos seleccionar de la lista el item con el index 0. 

                    //esto puede llegar a servir para muchos propósitos donde un api retorna uno o muchos registro
                    //pero necesitamos solo uno de ellos 

                    var list = JsonConvert.DeserializeObject<List<Usuario>>(response.Content);

                    var item = list[0];

                    return item;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

        }

        public async Task<bool> UpdateUserAsync()
        {
            try
            {
                string RouteSufix = string.Format("Usuarios/{0}", this.UsuarioId);
                //armamos la ruta completa al endpoint en el API 
                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Put);

                //agregamos mecanismo de seguridad, en este caso API key
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);

                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //en el caso de una operación POST debemos serializar el objeto para pasarlo como 
                //json al API

                string SerializedModel = JsonConvert.SerializeObject(this);
                //agregamos el objeto serializado el el cuuerpo del request. 
                Request.AddBody(SerializedModel, GlobalObjects.MimeType);

                //ejecutar la llamada al API 
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien 
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

        }

        public async Task<bool> AddUserAsync()
        {
            try
            {
                string RouteSufix = string.Format("Usuarios");
                //armamos la ruta completa al endpoint en el API 
                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Post);

                //agregamos mecanismo de seguridad, en este caso API key
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);

                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //en el caso de una operación POST debemos serializar el objeto para pasarlo como 
                //json al API

                string SerializedModel = JsonConvert.SerializeObject(this);
                //agregamos el objeto serializado el el cuuerpo del request. 
                Request.AddBody(SerializedModel, GlobalObjects.MimeType);

                //ejecutar la llamada al API 
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien 
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.Created)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

        }

    }

}
