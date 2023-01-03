using DeadHand.Commands.Implementations;
using DeadHand.Scenarios.Abstracts;
using System;

namespace DeadHand.Scenarios.Implementations
{
    internal class ScenarioReader : ScenarioBase
    {
        public ScenarioReader(EmailCommand emailService, CheckRadioCommand radioService) : base(emailService, radioService)
        {
            //TODO: Implement
        }
        public override string ScenarioName => throw new NotImplementedException();

        public override string EndingLaunchText => throw new NotImplementedException();

        public override string EndingShutdownText => throw new NotImplementedException();
    }
}
