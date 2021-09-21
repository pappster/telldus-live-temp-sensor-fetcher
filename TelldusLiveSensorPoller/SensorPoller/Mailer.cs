using System.Net;
using System.Net.Mail;

namespace SensorPoller
{
    public static class Mailer
    {
        public static void SendMail(EmailSettings settings, string json)
        {
            var client = new SmtpClient(settings.SmtpServer, settings.SmtpServerPort)
            {
                Credentials = new NetworkCredential(settings.SenderEmail, settings.SenderPassword),
                EnableSsl = true
            };

            client.Send(settings.SenderEmail, settings.ReceiverEmail, "New sensor data", json);
        }
    }
}
