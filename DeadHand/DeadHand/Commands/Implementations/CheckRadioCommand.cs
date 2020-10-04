using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using System;
using System.Threading;

namespace DeadHand.Commands.Implementations
{
    internal class CheckRadioCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.checkRadio;
        public ProgrammingType CurrentProgramming { get; set; }

        public override string Description => "Allows to check whether National Radio program 4 is still on the air, and what type of prrogramming is broadcasted";

        public override void Execute(string param = null)
        {
            if (_rng.Next(1, 99) < 20)
            {
                Thread.Sleep(_rng.Next(2, 4) * 1000);
                Console.WriteLine("Can't find National Radio program 4's signal. Try again later");
                return;
            }
            switch (CurrentProgramming)
            {
                case ProgrammingType.normal:
                    Thread.Sleep(_rng.Next(2, 4) * 1000);
                    Console.WriteLine("National Radio program 4 is on the air");
                    Thread.Sleep(_rng.Next(2, 4) * 1000);
                    Console.WriteLine("Currently broadcasting: normal programming");
                    break;
                case ProgrammingType.specialBulletin:
                    Thread.Sleep(_rng.Next(2, 4) * 1000);
                    Console.WriteLine("National Radio program 4 is on the air");
                    Thread.Sleep(_rng.Next(2, 4) * 1000);
                    Console.WriteLine("Currently broadcasting: special news bulletin");
                    break;
                case ProgrammingType.presidentialAddress:
                    Thread.Sleep(_rng.Next(2, 4) * 1000);
                    Console.WriteLine("National Radio program 4 is on the air");
                    Thread.Sleep(_rng.Next(2, 4) * 1000);
                    Console.WriteLine("Currently broadcasting: presidential address");
                    break;
                case ProgrammingType.none:
                    Thread.Sleep(_rng.Next(2, 4) * 1000);
                    Console.WriteLine("Can't find National Radio program 4's signal. Try again later");
                    break;
            }
        }

        internal enum ProgrammingType
    {
        normal, specialBulletin, presidentialAddress, none
    }
    }
}