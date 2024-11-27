using HospitalML.EmailService.EmailAtuh;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Security;
//using System.Net.Mail;

namespace HospitalML.EmailService
{
    public class MailService:IMailService
    {
        private EmailInformation _option { get; set; }

        public MailService(IOptions<EmailInformation> option) {
            _option = option.Value;
        }

        public void SendEmail(Email email)
        {


           
            var mail = new MimeMessage() 
            {
                Sender=MailboxAddress.Parse(_option.Email),
                Subject=email.Subject                

            };


            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(MailboxAddress.Parse(_option.Email));
            var Builder = new BodyBuilder();
            Builder.TextBody = email.Body;
            mail.Body = Builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_option.Host,_option.Port,MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_option.Email, _option.Password);

            smtp.Send(mail);
            smtp.Disconnect(true);




        }
    }
}
