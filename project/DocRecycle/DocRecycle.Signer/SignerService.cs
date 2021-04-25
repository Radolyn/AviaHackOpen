#region

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using TemplateEngine.Docx;

#endregion

namespace DocRecycle.Signer
{
    public static class SignerService
    {
        public static string Preview(string template, SignerContext context)
        {
            var values = GetData(context);

            var res = SaveAsDocx(template, values);

            return res;
        }

        private static Content GetData(SignerContext context)
        {
            var passport = context.User.Documents.First(x => x.Type.Name == "Паспорт");
            var birthDate = DateTime.Parse(context.User.Documents.First(x => x.Type.Name == "Дата рождения").Value);

            // todo: include
            if (context.SignImage != null)
            {
                var ms = new byte[context.SignImage.Length];
                context.SignImage.Read(ms, 0, ms.Length);
            }

            var values = new Content(
                new FieldContent("Full name",
                    $"{context.User.FirstName} {context.User.MiddleName} {context.User.LastName}"),
                new FieldContent("Birth year", birthDate.Year.ToString()),
                new FieldContent("Sign date", DateTime.Now.ToString("dd.MM.yy")),
                // new ImageContent("Sign", ms),
                new FieldContent("Sign decryption", context.User.LastName)
            );

            return values;
        }

        private static string SaveAsDocx(string template, Content values)
        {
            var filename = Path.GetTempFileName() + ".docx";
            File.Copy(template, filename, true);
            var outFile = new TemplateProcessor(filename).SetRemoveContentControls(true);

            outFile.FillContent(values);
            outFile.SaveChanges();

            outFile.Dispose();

            return filename;
        }

        public static string Sign(string template, SignerContext context)
        {
            var values = GetData(context);

            var filename = SaveAsDocx(template, values);

            var res = DocxToPdf.ConvertToPdf(filename);
            SendMail(context, res);

            return res;
        }

        private static void SendMail(SignerContext context, string filename)
        {
            using var f = File.Open(filename, FileMode.Open);

            var sender = new MailAddress("sltkval1@gmail.com");
            var receiver = new MailAddress(context.User.Email);

            var message = new MailMessage(sender, receiver);

            message.Subject = "Копия документа";

            message.Body = "Высылаем копию подписанного документа.";
            message.Attachments.Add(new Attachment(f, "document.pdf"));

            // todo: remove hard coded
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("???/", "????"),
                EnableSsl = true
            };

            smtp.Send(message);
        }
    }
}