using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using DocumentFormat.OpenXml.Spreadsheet;

namespace EstimationPortal.Utility
{
    public class EmailService
    {
        public static void EmailWithVSignup(string UserElId, string FirmName, string CName, string MoNo, string Password, string callbackUrl)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress("notification@gmmpfaudler.com");
                message.To.Add(UserElId);
                message.Subject = "Login Credential for Document Management Portal";
                message.Body = "Hello " + CName + ",<br/><br/>Name of Firm: "
                    + FirmName +
                    "<br />Contact Person Name: "
                    + CName +
                    "<br />Contact Person Mobile : "
                    + MoNo +
                    "<br />UserId : "
                    + UserElId +
                    "<br />Password : "
                    + Password +
                  "<br /><br />Please Click <a href=\"" + callbackUrl + "\">here</a> to proceed further on Vendor Registration<br/><br/><i>This is auto generated mail. Do not reply</i><br /><br />Regards<br /><b>GMM PFaudler</b>";
                // message.Body = "Please Click here to Login <a href=\"" + callbackUrl + "\">here</a>";
                message.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient();
                //smtp.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
                //smtp.Host = "smtp.office365.com";
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new System.Net.NetworkCredential("notification@gmmpfaudler.com", "N0t!f!c@t10n");
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Send(message);

            }
        }
        public static void SendEmailInquiryCreated(string subject, string emailBody, string attachmentPath = null, List<string> ccEmails = null, string TE_Email=null,string PE_Email=null,string EE_Email=null)
        {
            
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress("notification@gmmpfaudler.com");
                if (!string.IsNullOrEmpty(TE_Email))
                {
                    message.To.Add(TE_Email);
                }
                if (!string.IsNullOrEmpty(PE_Email))
                {
                    message.To.Add(PE_Email);
                }
                if (!string.IsNullOrEmpty(EE_Email))
                {
                    message.To.Add(EE_Email);
                }
                if (ccEmails != null && ccEmails.Count > 0)
                {
                    foreach (var cc in ccEmails)
                    {
                        if (!string.IsNullOrWhiteSpace(cc))
                        {
                            message.CC.Add(cc.Trim());
                        }
                    }
                }

                message.Subject = subject;
                message.Body = emailBody;
                message.IsBodyHtml = true;
                // Check if an attachment path was provided and if the file actually exists
                if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
                {
                    Attachment attachment = new Attachment(attachmentPath);
                    message.Attachments.Add(attachment);
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
                smtp.Host = "smtp.office365.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("notification@gmmpfaudler.com", "N0t!f!c@t10n");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
        }
        
    }
}