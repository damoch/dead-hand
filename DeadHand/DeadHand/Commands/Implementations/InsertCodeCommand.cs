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
        public override CommandIdentifier Identifier => CommandIdentifier.deadHand;
        public Action OnSuccesfullDelayCode { get; set; }
        private Random _rng = new Random();

        public override string Description => "Controls Dead Hand system";
        public bool CancelCommand { get; set; }

        public override void Execute(string param = null)
        {
            Console.WriteLine("Welcome to Dead Hand. Please enter code and press enter: ");
            var result = Console.ReadLine();
            Console.WriteLine("Checking code...");
            Thread.Sleep(_rng.Next(1, 99) * 1000);
            if (CancelCommand)
            {
                return;
            }
            if (_rng.Next(1, 99) < 20)
            {
                Console.WriteLine("An error occured. Pleasy try again.");
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
            else
            {
                Console.WriteLine("Code incorrect");
            }
        }
    }
}
