using DeadHand.Commands.Implementations;
using DeadHand.Scenarios.Abstracts;
using System;

namespace DeadHand.Scenarios.Implementations
{
    internal class ScenarioReader : ScenarioBase
    {
        public ScenarioReader(EmailCommand emailService, CheckRadioCommand radioService, WeatherServiceCommand weatherServiceCommand, DeadHandCommand deadHandCommand) : 
            base(emailService, radioService, weatherServiceCommand, deadHandCommand)
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
    }
}
