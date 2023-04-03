using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAppo_StevenC.Models
{
    public class Email
    {
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }




        public bool SendEmail()
        {
            bool R = false;

            try
            {

                if (!string.IsNullOrEmpty(SendTo) && !string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Message))
                {
                    System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();

                    //TODO: crear email valido para enviar correo
                    email.From = new System.Net.Mail.MailAddress("progra6umcacr@gmail.com");
                    email.Subject = Subject;
                    email.Body = Message;
                    email.To.Add(SendTo);

                    email.IsBodyHtml = false;

                    System.Net.Mail.SmtpClient server = new System.Net.Mail.SmtpClient();
                    server.Host = "smtp.gmail.com";
                    server.Port = 587;

                    server.EnableSsl = true;
                    server.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    server.Credentials = new System.Net.NetworkCredential("progra6umcacr@gmail.com", "txkzosgwwhipzjpr");
                    server.Send(email);
                    R = true;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return R;
        
        }


    }
}
