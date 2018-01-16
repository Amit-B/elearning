using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
namespace EL.Classes
{
    /// <summary>
    /// ספריית שימוש בפעולות הקשורות באינטרנט (System.Net).
    /// </summary>
    class Net
    {
        public const string MailerAddress = "elearning.program12@gmail.com";
        /// <summary>
        /// שליחת מייל.
        /// </summary>
        /// <param name="to">כתובת לשליחה</param>
        /// <param name="subject">כותרת</param>
        /// <param name="body">טקסט</param>
        /// <param name="from">כתובת השולח</param>
        /// <returns>ערך בוליאני אם השליחה הצליחה</returns>
        public static bool SendMail(string to, string subject, string body, string from = MailerAddress)
        {
            try
            {
                SmtpClient c = new SmtpClient();
                MailMessage m = new MailMessage();
                NetworkCredential sc = new System.Net.NetworkCredential(MailerAddress, "ex72tr74");
                c.Host = "smtp.gmail.com";
                c.Port = 587;
                c.UseDefaultCredentials = false;
                c.Credentials = sc;
                c.EnableSsl = true;
                //m.Attachments.Add(new Attachment(fname));
                m.Body = "ELearning Mail (" + DateTime.Now.ToLongDateString() + "):\r\n\r\n" + body;
                m.Subject = subject;
                m.From = new MailAddress(from);
                m.To.Add(new MailAddress(to));
                c.Send(m);
                m.Dispose();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// שליחה מתקדמת של מייל.
        /// </summary>
        /// <param name="to">רשימת כתובות לשליחה</param>
        /// <param name="subject">כותרת</param>
        /// <param name="body">טקסט</param>
        /// <param name="from">כתובת השולח</param>
        /// <param name="cc">רשימת העתקים</param>
        /// <param name="bcc">רשימת העתקים מוסתרים</param>
        /// <param name="attach">רשימת קבצים לצירוף</param>
        /// <returns>ערך בוליאני אם השליחה הצליחה</returns>
        public static bool SendAdvancedMail(MailAddressCollection to, string subject, string body, string from = MailerAddress, MailAddressCollection cc = null, MailAddressCollection bcc = null, string[] attach = null)
        {
            try
            {
                SmtpClient c = new SmtpClient();
                MailMessage m = new MailMessage();
                NetworkCredential sc = new System.Net.NetworkCredential(MailerAddress, "ex72tr74");
                c.Host = "smtp.gmail.com";
                c.Port = 587;
                c.UseDefaultCredentials = false;
                c.Credentials = sc;
                c.EnableSsl = true;
                if(attach != null) for(int i = 0; i < attach.Length; i++) m.Attachments.Add(new Attachment(attach[i]));
                m.Body = "EL Mail (" + DateTime.Now.ToLongDateString() + "):\r\n\r\n" + body;
                m.Subject = subject;
                m.From = new MailAddress(from);
                for (int i = 0; i < to.Count; i++) m.To.Add(to[i]);
                if (cc != null) for (int i = 0; i < cc.Count; i++) m.CC.Add(cc[i]);
                if (bcc != null) for (int i = 0; i < bcc.Count; i++) m.Bcc.Add(bcc[i]);
                c.Send(m);
                m.Dispose();
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// השמעת דיבור באמצעות Google Translate TTS.
        /// </summary>
        /// <param name="text">טקסט לדיבור</param>
        public static void TTS(string text)
        {
            /*if (Classes.Audio.Status().Length > 0)
                Classes.Audio.Close();
            Classes.Audio.Open("http://translate.google.com/translate_tts?tl=en&q=" + text);
            Classes.Audio.Play();*/
        }
    }
}