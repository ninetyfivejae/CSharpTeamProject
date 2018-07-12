using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SignUpProgram
{
    class MailService
    {
        string SMTP_SERVER = "smtp.naver.com";
        int SMTP_PORT = 587;
        string senderID = "메일주소";
        string senderName = "아이디";
        string senderPassword = "비밀번호";
        Database database;
        Exceptions exception;

        public MailService()
        {
            database = Database.getInstance();
            exception = Exceptions.getInstance();
        }

        public bool Send(string phone, string email)
        {
            MemberVO member = database.GetMemberInfo("member", "email", email);

            if (!exception.isDuplicated("member", "email", email))
                return false;

            string[] account = new string[2];

            MailAddress mailFrom = new MailAddress(senderID, senderName);
            MailAddress mailTo = new MailAddress(email);

            SmtpClient client = new SmtpClient(SMTP_SERVER, SMTP_PORT);
            MailMessage message = new MailMessage(mailFrom, mailTo);

            message.Subject = "C# Team Project 아이디 비밀번호 알림 메일";
            message.Body = "아이디 : " + member.MemberUSERNAME + " / 비밀번호 : " + member.MemberPASSWORD;
            message.BodyEncoding = Encoding.UTF8;
            message.SubjectEncoding = Encoding.UTF8;

            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(senderID, senderPassword);
            client.Send(message);
            return true;
        }
    }
}
