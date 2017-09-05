namespace ILearn.WebApi.Utilities
{
    using System;
    using System.Configuration;
    using System.Net.Mail;

    public class EmailUtility
    {
        public static string SendEmail(string emailTo, string subject, string body)
        {

            string emailFrom = ConfigurationManager.AppSettings.Get("SendingEmail");
            string emailPassword = ConfigurationManager.AppSettings.Get("SendingPassword");
            string host = ConfigurationManager.AppSettings.Get("EmailHost");
            bool enableSsl = bool.Parse(ConfigurationManager.AppSettings.Get("EmailEnableSSL"));
            int port = int.Parse(ConfigurationManager.AppSettings.Get("EmailPort"));

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = enableSsl
            };

            //SmtpClient client = new SmtpClient();

            //client.Host = Host;

            //client.Port = Port;

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(emailFrom, emailPassword);

            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(emailFrom);
            msg.To.Add(new MailAddress(emailTo));

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = "<html><head></head><body>" + body + "</body>";

            try
            {
                client.Send(msg);
                return "Your message has been successfully sent.";
            }
            catch (Exception ex)
            {
                return "Error occured while sending your message." + ex.Message;
            }
        }
    }
}
