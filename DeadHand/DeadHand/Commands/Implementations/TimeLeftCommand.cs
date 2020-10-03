using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using System;

namespace DeadHand.Commands.Implementations
{
    internal class TimeLeftCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.timeLeft;
        public DateTime CurrentTimer { get; set; }

        public override string Description => "Displays time left until Dead Hand system activation";

        public override void Execute(string param = null)
        {
            var elpased = CurrentTimer - DateTime.Now;
            Console.WriteLine($"Time until Dead Hand activation {elpased.Minutes} minutes {elpased.Seconds} seconds");
        }
    }
}
