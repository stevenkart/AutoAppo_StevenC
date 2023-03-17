using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoAppo_StevenC.Models
{
    public class UserDTO
    {

        //el standard es usar DTOs en los modelos del app, por un asunto de seguridad y facilidad y pasar los json 
        //entre app y api. Obviamente esta facilidad se ve reflejada en cierta complejidad al momento de transformar el DTO en modelo y viceversa a nivel del API.


        public RestRequest Request { get; set; }

        public int IDUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string NumeroTelefono { get; set; } = null!;
        public string Contrasennia { get; set; } = null!;
        public string? Cedula { get; set; }
        public string? Direccion { get; set; }
        public int IdRol { get; set; }
        public int IdEstado { get; set; }
        public string EstadoDescripcion { get; set; } = null!;
        public string RolDescripcion { get; set; } = null!;


        //Funciones
        public async Task<UserDTO> GetUserData(string email)
        {
            try
            {
                //en APIConnection definimos uin prefijo para la ruta de consumo de los end points. Aca tenemos que agregar el resto de la ruta para la funcion que queremos usar
                //dentro del controller


                string RouteSufix = string.Format("Users/GetUserData?Correo={0}", email);

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
                    var list = JsonConvert.DeserializeObject<List<UserDTO>>(response.Content);

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
                string ErrorMsg = ex.Message;

                //almacenar registro de errores en una bitacora para analisis posteriores
                //tambien puede ser enviarlos a un servidor de captura de errores

                throw;
            }
        }

    }
}
