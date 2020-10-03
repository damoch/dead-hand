
using System;
using System.Collections.Generic;
using System.Linq;
using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;

namespace DeadHand.Commands.Implementations
{
    internal class EmailCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.email;

        public override string Description => "Email client. Usage:\n email list - lists unread messages \n email read - writes first of unread messages";

        public List<Email> EmailList { get; set; }

        public EmailCommand()
        {
            EmailList = new List<Email>();
        }

        public override void Execute(string param = null)
        {
            if(param == null)
            {
                Console.WriteLine("email: option needed");
                return;
            }
            try
            {
                var option = Enum.Parse(typeof(EmailOptions), param);

                switch (option)
                {
                    case EmailOptions.list:
                        ListEmails();
                        break;
                    case EmailOptions.read:
                        ReadFirstEmails();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("email: Unknown option");
            }
        }

        private void ListEmails()
        {
            var unreadMessages = EmailList.Where(x => !x.IsRead).ToList();
            if (unreadMessages.Count == 0)
            {
                Console.WriteLine("No new messages");
                return;
            }

            foreach (var item in unreadMessages)
            {
                Console.WriteLine("Sender: {0}", item.Sender);
                Console.WriteLine("Date: {0}", item.ReceivedDate);
                Console.WriteLine("Subject: {0}", item.Subject);
                Console.WriteLine("");
            }
        }

        private void ReadFirstEmails()
        {
            var unreadMessage = EmailList.FirstOrDefault(x => !x.IsRead);
            if (unreadMessage == null)
            {
                Console.WriteLine("No messages to read");
                return;
            }
            unreadMessage.IsRead = true;
            Console.WriteLine("Sender: {0}", unreadMessage.Sender);
            Console.WriteLine("Date: {0}", unreadMessage.ReceivedDate);
            Console.WriteLine("Subject: {0}", unreadMessage.Subject);
            Console.WriteLine(unreadMessage);
        }
    }


    enum EmailOptions {
        list, read
    }

    internal class Email
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool IsRead { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }

        public int SpookyAdd { get; set; }

    }
}
