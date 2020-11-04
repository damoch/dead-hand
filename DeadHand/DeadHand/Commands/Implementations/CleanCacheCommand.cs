using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using System;
using System.Threading;

namespace DeadHand.Commands.Implementations
{
    internal class CleanCacheCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.cleanCache;

        public override string Description => "Perform memory cache cleanup (erases all content of screen)";

        public DeadHandSettings CurrentSettings { get; internal set; }

        public override void Execute(string param = null)
        {
            Console.WriteLine("Starting cache cleanup...");
            Thread.Sleep(_rng.Next(1, 3) * 1000);
            Console.Clear();
            Thread.Sleep(_rng.Next(1, 3) * 1000);
            Console.WriteLine("Done.");
            Thread.Sleep(_rng.Next(1, 3) * 1000);
            CurrentSettings.MemoryCacheUsedPercentage = _rng.Next(10, 15);
        }
    }
}
