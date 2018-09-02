using BetaViews.Core.Framework.Extension;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BetaViews.Core.Framework
{

    public static class MailSend 
   {
        public static string EmailSystem
        {
            get
            {
                return ConfigurationManager.AppSettings["emailSystem"];
            }
           
        }
        public static string PwdMailSystem
        {
            get {
            return ConfigurationManager.AppSettings["pwdMailSystem"];
        }
            
        }
        public static string SMTPServer
        {
            get
            {
                return ConfigurationManager.AppSettings["mailServer"];
            }            
        }
        public static int ServerPort
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["mailPort"]);
            }            
        }
        public static string API {
            get {

                return ConfigurationManager.AppSettings["apikeysendgrid"];
            }
        }



        public static bool SendMailMessageWithAttachFile(string toMail, string subject, string body, string attachFilePath) {
            try
            {
                var from = new MailAddress(EmailSystem);
                var to = new MailAddress(toMail);
                var message = new MailMessage();
                message.From = from;
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                if (attachFilePath.CheckIfFileExists())
                {
                    message.Attachments.Add(new System.Net.Mail.Attachment(attachFilePath));    
                }

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = SMTPServer;
                    smtp.Port = ServerPort; //8889 ou 587 google & effectlab.
                    smtp.EnableSsl = false;   //google precisa estar habilitado.                   
                    smtp.UseDefaultCredentials = false;



                    if (EmailSystem != from.Address)
                        smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                    else
                        smtp.Credentials = new NetworkCredential(EmailSystem, PwdMailSystem);

                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogFile.WriteLog(ex.Message, "Erro.metodo SendMailMessage()");
                return false;
            }
            
        }
        
        public static bool SendMailMessage(string toMail, string subject, string body)
        {

            try
            {
                var from = new MailAddress(EmailSystem);
                var to = new MailAddress(toMail);
                var message = new MailMessage();
                message.From = from;
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = SMTPServer;
                    smtp.Port = ServerPort; //8889 ou 587 google & effectlab.
                    smtp.EnableSsl = false;   //google precisa estar habilitado.                   
                    smtp.UseDefaultCredentials = false;

                    ////if (EmailSystem != from.Address)
                    ////    smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                    ////else
                    //    smtp.Credentials = new NetworkCredential(EmailSystem, PwdMailSystem);

                    if (EmailSystem != from.Address)
                        smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                    else
                        smtp.Credentials = new NetworkCredential(EmailSystem, PwdMailSystem);

                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogFile.WriteLog(ex.Message, "Erro.metodo SendMailMessage()");
                return false;
            }
        }   



        public static bool SendMailMessage(string fromMail, List<string> ToMail, List<string> BccMail, string subject, string body)
        {
            try
            {
                var from = new MailAddress(fromMail);

                var to = new List<MailAddress>();
                if (ToMail != null)  ToMail.ForEach(x => to.Add(new MailAddress(x)));

                var bcc = new List<MailAddress>();
                if (BccMail!=null) BccMail.ForEach(x => bcc.Add(new MailAddress(x)));

                var message = new MailMessage();
                message.From = from;
                to.ForEach(x => message.To.Add(x));
                bcc.ForEach(x => message.Bcc.Add(x));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = SMTPServer;
                    smtp.Port = ServerPort; //8889 ou 587 google & effectlab.
                    smtp.EnableSsl = false;   //google precisa estar habilitado.                   
                    smtp.UseDefaultCredentials = false;
                    
                    if (EmailSystem != from.Address)
                        smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                    else
                        smtp.Credentials = new NetworkCredential(EmailSystem, PwdMailSystem);

                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogFile.WriteLog(ex.Message, "Erro.metodo SendMailMessage()");
                return false;
            }
        }



        public static async Task SendMailMessageAsync(string toMail, string subject, string body)
        {
            try
            {
                var from = new MailAddress(EmailSystem);
                var to = new MailAddress(toMail);
                var message = new MailMessage();
                message.From = from;
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = SMTPServer;
                    smtp.Port = ServerPort; //8889 ou 587 google & effectlab.
                    smtp.EnableSsl = false;   //google precisa estar habilitado.                   
                    smtp.UseDefaultCredentials = false;

                    if (EmailSystem != from.Address)
                        smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                    else
                        smtp.Credentials = new NetworkCredential(EmailSystem, PwdMailSystem);

                  await smtp.SendMailAsync(message);
                }

                //return true;
            }
            catch (Exception ex)
            {
                LogFile.WriteLog(ex.Message, "Erro.metodo SendMailMessage()");
                //return false;
            }
        }


        public static async Task SendGridAsync(string toMail, string nome, string subject, string body) {


            var apiKey = API;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("avaliacoes@pushstars.com.br", "Avaliações");
            var to = new EmailAddress(toMail, nome);
            var htmlContent = body;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);

            var response = await client.SendEmailAsync(msg);


        }



    }
}
