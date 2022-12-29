using DeadHand.Commands.Implementations;
using DeadHand.Scenarios.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeadHand.Scenarios.Implementations
{
    internal class ScenarioReader : ScenarioBase
    {
        public ScenarioReader(EmailCommand emailService, CheckRadioCommand radioService) : base(emailService, radioService)
        {
            //TODO: Implement
        }
        public override string ScenarioName => throw new NotImplementedException();

        public override void ScenarioEndingLaunch()
        {
            throw new NotImplementedException();
        }

        public override void ScenarioEndingShutdown()
        {
            throw new NotImplementedException();
        }

        public override void StartScenario()
        {
            throw new NotImplementedException();
        }
    }
}
