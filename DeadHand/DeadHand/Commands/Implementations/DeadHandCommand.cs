using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using System;
using System.Threading;

namespace DeadHand.Commands.Implementations
{
    internal class DeadHandCommand : CommandBase
    {
        //TODO: move codes to scenario
        private string _delayCode = "F7SA-USA7-JA98-CDSA";
        private string _activationCode = "2DCJ-CA83-8A9H-A9HD";
        private string _shutdownCode = "FA7S-I82B-HEY4-HWEF";
        public override CommandIdentifier Identifier => CommandIdentifier.deadHand;
        public Action OnSuccesfullDelayCode { get; set; }
        public Action OnSystemShutdown { get; set; }

        public override string Description => "Controls Dead Hand system\nUsage:\ndeadHand timeLeft - shows time until Dead Hand activation\nWithout parameters - shows prompt to enter Dead Hand code";
        public bool CancelCommand { get; set; }
        public DateTime AcceptCodeTime { get; internal set; }
        public DateTime CurrentTimer { get; internal set; }

        public override void Execute(string param = null)
        {
            if(param == "timeLeft")
            {
                if(CurrentTimer == new DateTime())
                {
                    Console.WriteLine("Dead Hand countdown not started yet.");
                    return;
                }
                var elpased = CurrentTimer - DateTime.Now;
                Console.WriteLine($"Time until Dead Hand activation {elpased.Minutes} minutes {elpased.Seconds} seconds");
                return;
            }
            else if(param != null)
            {
                Console.WriteLine($"Unknown parameter: {param}");
                return;
            }
            Console.WriteLine("Welcome to Dead Hand. Please enter code and press enter: ");
            var result = Console.ReadLine();
            Console.WriteLine("Checking code...");
            Thread.Sleep(_rng.Next(1, 15) * 1000);
            if (CancelCommand)
            {
                return;
            }
            if (_rng.Next(1, 99) < 20)
            {
                Console.WriteLine("An error occured. Pleasy try again later.");
                return;
            }

            if (result == _delayCode && OnSuccesfullDelayCode != null)
            {
                if((AcceptCodeTime - DateTime.Now).Minutes > 4)
                {
                    Console.WriteLine("Too early to enter delay code!");
                    return;

                }
                Console.WriteLine("Code correct. Next Dead Hand activation in 10 minutes");
                OnSuccesfullDelayCode.Invoke();
            }
            else if(result == _activationCode)
            {
                Console.WriteLine("Code correct. Dead Hand activation in 10 minutes");
                OnSuccesfullDelayCode.Invoke();
            }
            else if(result == _shutdownCode)
            {
                CancelCommand = true;
                OnSystemShutdown.Invoke();
            }
            else
            {
                Console.WriteLine("Code incorrect");
            }
        }
    }
}
