using DeadHand.Commands.Implementations;
using DeadHand.Scenarios.Abstracts;
using System;

namespace DeadHand.Scenarios.Implementations
{
    internal class ScenarioReader : ScenarioBase
    {
        public ScenarioReader(DeadHandCommand deadHandCommand) : base(deadHandCommand)
        {
            //TODO: Implement
        }
        public override string ScenarioName => throw new NotImplementedException();

        public override string EndingLaunchText => throw new NotImplementedException();

        public override string EndingShutdownText => throw new NotImplementedException();

        public override string DelayCode => throw new NotImplementedException();

        public override string ActivationCode => throw new NotImplementedException();

        public override string ShutdownCode => throw new NotImplementedException();

        public override string RadioStationID => throw new NotImplementedException();

        public override Tuple<string, string> WeatherServiceData => throw new NotImplementedException();

        public override int MotherboardTemperature => throw new NotImplementedException();

        public override int MemoryCacheUsedPercentage => throw new NotImplementedException();

        public override int DiskFragmentationPercentage => throw new NotImplementedException();

        public override Tuple<int, int> DiskFragmentationPercentageChanges => throw new NotImplementedException();

        public override Tuple<int, int> MemoryCacheUsedPercentageChanges => throw new NotImplementedException();

        public override Tuple<int, int> MotherboardTemperatureChanges => throw new NotImplementedException();
    }
}
