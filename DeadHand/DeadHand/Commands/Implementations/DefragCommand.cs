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
            Console.WriteLine("Starting defragmentation of hard drive... This might take some time");
            Thread.Sleep(_rng.Next(2, 4) * 1000);
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine($"\rDefragmentation: {i}%");
                Thread.Sleep(_rng.Next(50, 800));
            }
            Console.WriteLine("Operation succesfull!");
            CurrentSettings.DiskFragmentationPercentage = _rng.Next(10, 20);
        }
    }
}
