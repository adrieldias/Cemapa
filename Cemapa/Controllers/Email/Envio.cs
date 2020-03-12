using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Cemapa.Models;

namespace Cemapa.Controllers.Email
{
    public static class Envio
    {
        private static readonly Entities db = new Entities();

        public static void EnviaEmail(List<string> eDestinos, string assunto, string corpo)
        {
            TB_EMAIL wConfigEmail = (from e in db.TB_EMAIL where e.COD_EMAIL == 1 select e).FirstOrDefault();

            if (wConfigEmail != null)
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = wConfigEmail.DESC_SMTP;
                    smtp.Port = wConfigEmail.NUM_PORTA_SMTP;
                    smtp.EnableSsl = wConfigEmail.IND_SSL == "S";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(wConfigEmail.DESC_EMAIL, wConfigEmail.DESC_SENHA);

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(wConfigEmail.DESC_EMAIL);

                        foreach (string destino in eDestinos)
                        {
                            mail.To.Add(new MailAddress(destino));
                        }

                        mail.Subject = assunto;
                        mail.Body = corpo;

                        smtp.Send(mail);
                    }
                }
            }
        }
    }
}