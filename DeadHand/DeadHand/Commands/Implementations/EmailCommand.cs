
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
//TODO: add the feature to check previous messages, new message received tone
namespace DeadHand.Commands.Implementations
{
    internal class EmailCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.email;

        public override string Description => "Email client. Usage:\n email list - lists unread messages \n email read - writes first of unread messages\nemail read [number] - read email of specific id (ex: email read 3)";
        private int _currentIndex;
        public Dictionary<int, Email> EmailList { get; set; }

        public EmailCommand()
        {
            _currentIndex = 1;
            EmailList = new Dictionary<int, Email>();
        }

        public void AddEmail(Email email)
        {
            EmailList.Add(_currentIndex++, email);
            Console.WriteLine("email: there are new messages");
            Consts.PlayMelody(Consts.NewEmailSound);
            Console.Write("\n>>> ");
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
                switch (param)
                {
                    case "list":
                        ListEmails();
                        break;
                    case "read":
                        ReadEmails();
                        break;
                    default:
                        if (param.StartsWith("read") &&  param.Contains(' '))
                        {
                            var index = int.Parse(param.Split(' ')[1]);
                            ReadEmails(index);
                        }
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
            if (EmailList.Count == 0)
            {
                Console.WriteLine("email: inbox is empty");
                return;
            }
            foreach (var item in EmailList)
            {
                Console.WriteLine("ID: {0}", item.Key);
                Console.WriteLine("Sender: {0}", item.Value.Sender);
                Console.WriteLine("Date: {0}", item.Value.ReceivedDate);
                Console.WriteLine("Subject: {0}", item.Value.Subject);
                Console.WriteLine("");
            }
        }

        private void ReadEmails(int? indx = null)
        {
            var message = EmailList.Values.FirstOrDefault(x => !x.IsRead);
            if (message == null && indx == null)
            {
                Console.WriteLine("email: no messages to read");
                return;
            }
            else if (indx != null && !EmailList.ContainsKey(indx.Value))
            {
                Console.WriteLine($"email: no message with ID: {indx.Value}");
                return;
            }
            else if(indx != null && EmailList.ContainsKey(indx.Value))
            {
                message = EmailList[indx.Value];
            }
            message.IsRead = true;
            Console.WriteLine("ID: {0}", indx.Value);
            Console.WriteLine("Sender: {0}", message.Sender);
            Console.WriteLine("Date: {0}", message.ReceivedDate);
            Console.WriteLine("Subject: {0}", message.Subject);
            Console.WriteLine(message);
        }
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
        public ProgrammingType ProgrammingType { get; set; }

    }

}
