using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeadHand.Commands.Implementations
{
    //TODO: Data should be loaded from scenario
    internal class WeatherServiceCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.weatherService;
        public bool IsWorking { get; set; }

        public WeatherServiceCommand():base()
        {
            IsWorking = true;
        }

        public override string Description => "Delivers latest update from Naval Weather Service";

        public override void Execute(string param = null)
        {
            Thread.Sleep(_rng.Next(2, 4) * 1000);
            if (IsWorking && _rng.Next(1,99)>20)
            {
                Console.WriteLine("Weather report:\nTemperature: 25°C\nWind: 10km/h\nCloudy\nHumidity: 10%");
            }
            else
            {
                Console.WriteLine("Unable to obtain weather data. Try again later.");
            }
        }
    }
}
