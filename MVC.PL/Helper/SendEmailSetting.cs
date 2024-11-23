using DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace MVC.PL.Helper
{
    public class SendEmailSetting
    {
        public static void SendEmail (Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ahmedkamal22101999@gmail.com", "xszs fxes msgs ptmz");

            client.Send("ahmedkamal22101999@gmail.com", email.To, email.Title, email.Body);
        }
    }
}
