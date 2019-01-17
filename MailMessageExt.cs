using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;

namespace Communications.Models
{
    public static class MailMessageExt
    {
        public static void Save(this MailMessage message, string filename, bool addUnsentHeader = true)
        {
            using (var filestream = File.Open(filename, FileMode.Create))
            {
                if (addUnsentHeader)
                {
                    var binaryWriter = new BinaryWriter(filestream);
                    //Write the Unsent header to the file so the mail client knows this mail must be presented in "New message" mode
                    binaryWriter.Write(System.Text.Encoding.UTF8.GetBytes("X-Unsent: 1" + Environment.NewLine));
                }

                var assembly = typeof(SmtpClient).Assembly;
                var mailWriterType = assembly.GetType("System.Net.Mail.MailWriter");

                // Get reflection info for MailWriter contructor
                var mailWriterContructor = mailWriterType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(Stream) }, null);

                // Construct MailWriter object with our FileStream
                var mailWriter = mailWriterContructor.Invoke(new object[] { filestream });

                // Get reflection info for Send() method on MailMessage
                var sendMethod = typeof(MailMessage).GetMethod("Send", BindingFlags.Instance | BindingFlags.NonPublic);

                sendMethod.Invoke(message, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { mailWriter, true, true }, null);

                // Finally get reflection info for Close() method on our MailWriter
                var closeMethod = mailWriter.GetType().GetMethod("Close", BindingFlags.Instance | BindingFlags.NonPublic);

                // Call close method
                closeMethod.Invoke(mailWriter, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { }, null);
            }
        }

        //public static void Save(this MailMessage Message, string FileName)
        //{
        //    Assembly assembly = typeof(SmtpClient).Assembly;
        //    Type _mailWriterType =
        //      assembly.GetType("System.Net.Mail.MailWriter");

        //    using (FileStream _fileStream =
        //           new FileStream(FileName, FileMode.Create))
        //    {
        //        // Get reflection info for MailWriter contructor
        //        ConstructorInfo _mailWriterContructor =
        //            _mailWriterType.GetConstructor(
        //                BindingFlags.Instance | BindingFlags.NonPublic,
        //                null,
        //                new Type[] { typeof(Stream) },
        //                null);

        //        // Construct MailWriter object with our FileStream
        //        object _mailWriter =
        //          _mailWriterContructor.Invoke(new object[] { _fileStream });

        //        // Get reflection info for Send() method on MailMessage
        //        MethodInfo _sendMethod =
        //            typeof(MailMessage).GetMethod(
        //                "Send",
        //                BindingFlags.Instance | BindingFlags.NonPublic);

        //        // Call method passing in MailWriter
        //        _sendMethod.Invoke(
        //            Message,
        //            BindingFlags.Instance | BindingFlags.NonPublic,
        //            null,
        //            new object[] { _mailWriter, true },
        //            null);

        //        // Finally get reflection info for Close() method on our MailWriter
        //        MethodInfo _closeMethod =
        //            _mailWriter.GetType().GetMethod(
        //                "Close",
        //                BindingFlags.Instance | BindingFlags.NonPublic);

        //        // Call close method
        //        _closeMethod.Invoke(
        //            _mailWriter,
        //            BindingFlags.Instance | BindingFlags.NonPublic,
        //            null,
        //            new object[] { },
        //            null);
        //    }
        //}
    }
}