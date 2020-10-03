using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Implementations;
using System.Timers;

namespace DeadHand
{
    public class GameController
    {
        internal EmailCommand EmailService { get; set; }

        private System.Timers.Timer _gameTimer;
        private bool _emailNewMessages;
        private TimeLeftCommand _timerService;
        private InsertCodeCommand _codeService;

        public void ChceckEmail()
        {
            if (_emailNewMessages)
            {
                Console.WriteLine("email: there are new messages");
                _emailNewMessages = false;
            }
        }

        internal GameController(EmailCommand emailService,
                                TimeLeftCommand timerService,
                                InsertCodeCommand codeService)
        {
            SetupEmailService(emailService);
            _timerService = timerService;
            _codeService = codeService;
            _codeService.OnSuccesfullCode += StartTimer;
        }

        public void StartTimer()
        {
            if (_gameTimer != null)
            {
                _gameTimer.Dispose();
            }
            _gameTimer = new System.Timers.Timer(60 * 1000);
            _timerService.CurrentTimer = DateTime.Now.AddSeconds(60);
            _gameTimer.Elapsed += _gameTimer_Elapsed;
            _gameTimer.Start();
        }

        private void _gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("elapsed");
        }

        private void SetupEmailService(EmailCommand emailService)
        {
            EmailService = emailService;
            emailService.EmailList = new List<Email>()
            {
                new Email()
                {
                    ReceivedDate = new DateTime(),
                    Subject = "Order no. 583",
                    Sender = "command@aircomm.com",
                    Content = @"Hello Zahary, 
I'm sory, that I have not been in touch, like I promised. But I have a killer story. Literally. 
I cannot provide you with details right now... Because somebody might be watching. I will have to destroy the computer.
Oh God, they will find out, but you might be able to get grasp of what is happening here now just in time
The public must know the truth. 
You are starting with nothing, but just you wait. You will wish not to go down this rabbit hole
But if you want to go there, connect to 16.151.26.247, and use login zbigniewadamczyk and password ErNrS5. Write them down. On a paper. Then query database with 'Palenice'
You remember the polish dictionary I have gave you? You might find it handy now. Or some translation software. Unless you know polish, of course.
Just keep in mind, that secure databeses might be able to detect if you are sniffing to hard. They certainly do not expect somebody from UK to access their databes, so 
try to keep it under 3-6 queries in one session to not raise any suspicions. Once you get some info, make notes, again on a paper. You are good journalist, I still remember our
story from Gulf War in '91. That's why I have to trust you. 
I have to end. They know that I want to blow whistle on them. It's all on your hands now. 
Good luck.


Joanna."
                }
            };
        }

        internal void AddEmail(Email email)
        {
            EmailService.EmailList.Add(email);
            _emailNewMessages = true;
        }
    }
}
