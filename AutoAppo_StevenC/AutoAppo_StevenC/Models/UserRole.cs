using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoAppo_StevenC.Models
{
    public class UserRole
    {

        public RestRequest Request { get; set; }

        public int UserRoleId { get; set; }
        public string UserRoleDescription { get; set; }



        //Funciones
        public async Task<List<UserRole>> GetAllUserRoleList()
        {
            try
            {
                
                string RouteSufix = string.Format("UserRoles");

                //con esto obtenemos la ruta completa de consumo
                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //Agregamos la info de la llave de seguridad (ApiKey)
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //ejecucion de la llamada al controlador
                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    //usamos newtonsoft para descomponer paquete json que nos llega del API
                    var list = JsonConvert.DeserializeObject<List<UserRole>>(response.Content);
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;

                //almacenar registro de errores en una bitacora para analisis posteriores
                //tambien puede ser enviarlos a un servidor de captura de errores

                throw;
            }
        }

    }
}
