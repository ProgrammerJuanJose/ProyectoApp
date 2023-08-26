using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.Models
{
    public class UsuarioTipo
    {

        public RestRequest Request { get; set; }
        public int UsuarioTipoId { get; set; }

        public string UsuarioTipoDescripcion { get; set; } = null!;

        public async Task<List<UsuarioTipo>> GetAllUserRolesAsync()
        {
            try
            {
                string RouteSufix = string.Format("UsuarioTipo");
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
                    var list = JsonConvert.DeserializeObject<List<UsuarioTipo>>(response.Content);

                    return list;
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
    }
}
