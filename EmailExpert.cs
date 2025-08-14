public static class EmailExpert
{


   
    /// <param name="recieverEmail">correo del receptor</param>
    /// <param name="subject">titulo del correo</param>
    /// <param name="body">cuerpo del correo</param>
    /// <param name="attachmentFiles">Paths completos a archivos para adjuntar</param>
    public static void SendEmail(string recieverEmail, string subject, string body, string[] attachmentFiles)
    {
        string senderEmail = ConfigurationManager.AppSettings["NotificationsEmailSender"];
        string senderAppPassword = ConfigurationManager.AppSettings["NotificationsEmailPassword"];

        using (MailMessage message = new MailMessage())
        {
            message.From = new MailAddress(senderEmail);
            message.To.Add(recieverEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            if (attachmentFiles != null)
            {
                foreach (string filePath in attachmentFiles)
                {
                    if (File.Exists(filePath)) 
                    {
                        message.Attachments.Add(new Attachment(filePath));
                    }
                    else
                    {
                        Console.WriteLine($"Attachment not found: {filePath}");
                    }
                }
            }
            message.IsBodyHtml = true;
            using (SmtpClient smtpClient = new SmtpClient("asd.asd.asd", 587)) 
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderAppPassword);
                smtpClient.EnableSsl = true; 

                Console.WriteLine("Sending email...");

                smtpClient.Send(message);
                Console.WriteLine("Email sent successfully.");
            }
        }
    }

    }
}
