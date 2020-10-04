using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using System;
using System.Threading;

namespace DeadHand.Commands.Implementations
{
    internal class InsertCodeCommand : CommandBase
    {
        private string _delayCode = "F7SA-USA7-JA98-CDSA";
        private string _activationCode = "2DCJ-CA83-8A9H-A9HD";
        private string _shutdownCode = "FA7S-I82B-HEY4-HWEF";
        public override CommandIdentifier Identifier => CommandIdentifier.deadHand;
        public Action OnSuccesfullDelayCode { get; set; }
        public Action OnSystemShutdown { get; set; }

        public override string Description => "Controls Dead Hand system";
        public bool CancelCommand { get; set; }

        public override void Execute(string param = null)
        {
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
                Console.WriteLine("Code correct. Next Dead Hand activation in 7 minutes");
                OnSuccesfullDelayCode.Invoke();
            }
            else if(result == _activationCode)
            {
                Console.WriteLine("Code correct. Dead Hand activation in 7 minutes");
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
