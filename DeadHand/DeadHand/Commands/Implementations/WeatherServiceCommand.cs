using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using System;
using System.Threading;

namespace DeadHand.Commands.Implementations
{
    internal class WeatherServiceCommand : CommandBase
    {
        private string _description;
        private string _report;
        public override CommandIdentifier Identifier => CommandIdentifier.weatherService;
        public bool IsWorking { get; set; }

        public WeatherServiceCommand():base()
        {
            IsWorking = true;
        }

        public void SetCommandData(string description, string report)
        {
            _description = description;
            _report = report;
        }

        public override string Description => _description;

        public override void Execute(string param = null)
        {
            Thread.Sleep(_rng.Next(2, 4) * 1000);
            if (IsWorking && _rng.Next(1,99)>20)
            {
                Console.WriteLine($"Weather report: {_report}");
            }
            else
            {
                Console.WriteLine("Unable to obtain weather data. Try again later.");
            }
        }
    }
}
