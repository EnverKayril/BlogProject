using BlogProject.SERVICE.Services.IServices;
using BlogProject.SERVICE.Utilities;
using Castle.Core.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailSender(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                // SMTP ayarlarını doğrudan burada tanımlayın
                string smtpHost = "smtp.mailersend.net";  // SMTP sunucusu adresi
                int smtpPort = 587;  // SMTP portu
                string smtpUsername = "MS_XZm6dY@trial-k68zxl2yv854j905.mlsender.net";  // SMTP kullanıcı adı
                string smtpPassword = "kHGcNSSu18EmBxus";  // SMTP şifresi

                // E-posta adreslerinin boş olup olmadığını kontrol et
                if (string.IsNullOrEmpty(smtpUsername))
                    throw new ArgumentNullException(nameof(smtpUsername), "SMTP kullanıcı adı boş olamaz.");
                if (string.IsNullOrEmpty(email))
                    throw new ArgumentNullException(nameof(email), "Alıcı e-posta adresi boş olamaz.");

                // Gönderici ve alıcı e-posta adresleri
                var fromAddress = new MailAddress(smtpUsername, "BlogProject");
                var toAddress = new MailAddress(email);

                // Mail mesajı oluşturma
                var mailMessage = new MailMessage
                {
                    From = fromAddress,
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true  // HTML formatında mı gönderilecek?
                };

                mailMessage.To.Add(toAddress);

                // SMTP istemcisi ile e-postayı gönder
                using (var client = new SmtpClient(smtpHost, smtpPort))
                {
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;  // SSL/TLS kullanmak
                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("E-posta gönderim hatası: " + ex.Message);
                throw;
            }
        }



    }
}
