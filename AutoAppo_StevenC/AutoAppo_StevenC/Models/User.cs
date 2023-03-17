using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoAppo_StevenC.Models
{
    public class User
    {

        public RestRequest Request { get; set; }
        

        public User()
        {

        }

        //atributos para la clase. En este ejemplo usaremos los atributos identicos a la clase 
        //Posteriormente usaremos los DTO

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string LoginPassword { get; set; } = null!;
        public string? CardId { get; set; }
        public string? Address { get; set; }
        public int UserRoleId { get; set; }
        public int UserStatusId { get; set; }

        public virtual UserRole? UserRole { get; set; } = null!;
        public virtual UserStatus? UserStatus { get; set; } = null!;


        //Funciones
        public async Task<bool> ValidateLogin()
        {
            try
            {
                //en APIConnection definimos uin prefijo para la ruta de consumo de los end points. Aca tenemos que agregar el resto de la ruta para la funcion que queremos usar
                //dentro del controller


                string RouteSufix = string.Format("Users/ValidateUserLogin?pUserName={0}&pPassword={1}", this.Email, this.LoginPassword);

                //con esto obtenemos la ruta completa de consumo
                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;
                
                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //Agregamos la info de la llave de seguridad (ApiKey)
                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //ejecucion de la llamada al controlador
                RestResponse response = await client.ExecuteAsync(Request);

                HttpStatusCode statusCode= response.StatusCode;

                if(statusCode == HttpStatusCode.OK)
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
                string ErrorMsg = ex.Message;

                //almacenar registro de errores en una bitacora para analisis posteriores
                //tambien puede ser enviarlos a un servidor de captura de errores

                throw;
            }
        }


        public async Task<bool> AddUser()
        {
            try
            {
                
                string RouteSufix = string.Format("Users");

                //con esto obtenemos la ruta completa de consumo
                string URL = Services.APIConnection.ProductionURLPrefix + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Post);

                //Agregamos la info de la llave de seguridad (ApiKey)

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //En este caso tenemos que enviar un JSON al API con la data del usuario que se quiere agregar

                string SerializedModel = JsonConvert.SerializeObject(this);

              

                Request.AddBody(SerializedModel, GlobalObjects.MimeType);


                //ejecucion de la llamada al controlador
                RestResponse response = await client.ExecuteAsync(Request);

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
                string ErrorMsg = ex.Message;

                //almacenar registro de errores en una bitacora para analisis posteriores
                //tambien puede ser enviarlos a un servidor de captura de errores

                throw;
            }
        }




    }
}
