using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using System;
using System.Threading;

namespace DeadHand.Commands.Implementations
{
    internal class DefragCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.defrag;
        public DeadHandSettings CurrentSettings { get; set; }

        public override string Description => "Fixes fragmentation of hard drive. No parameter";

        public override void Execute(string param = null)
        {  
            Console.Write("Defragmenting hard drive: ");
            Console.WriteLine();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(_rng.Next(50, 800));
                Console.Write("\r{0}% complete", i);
            }

            Console.Write("\r100% complete");
            Console.WriteLine();
            Console.WriteLine("Hard drive defragmented");


            CurrentSettings.DiskFragmentationPercentage = _rng.Next(10, 20);
        }
    }
}
