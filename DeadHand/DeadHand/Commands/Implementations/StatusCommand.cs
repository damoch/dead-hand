using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using System;

namespace DeadHand.Commands.Implementations
{
    internal class StatusCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.status;
        public DeadHandSettings CurrentSettings { get; set; }

        public override string Description => "Current status of main Dead Hand computer. No parameters";

        public override void Execute(string param = null)
        {
            Console.WriteLine("Current status of main Dead Hand computer:");
            Console.WriteLine(CurrentSettings);
        }
    }
}
