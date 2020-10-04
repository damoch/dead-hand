using DeadHand.Commands.Implementations;
using DeadHand.Scenarios.Abstracts;
using System;
using System.Timers;

namespace DeadHand.Scenarios.Implementations
{
    internal class FalseWarningScenario : ScenarioBase
    {
        public FalseWarningScenario(EmailCommand emailService) : base(emailService)
        {

        }

        public override string ScenarioName => "False warning";

        public override void StartScenario()
        {
            var sendCivilDangerEmailEvent = new Timer(_rng.Next(1,3) * 60 * 1000); //first email - warning message (1-3 minute)
            sendCivilDangerEmailEvent.Elapsed += SendCivilDangerEmail;
            sendCivilDangerEmailEvent.Start();
            _triggers.Add(sendCivilDangerEmailEvent);

            var sendEvacuateImmediatlyEmailEvent = new Timer(_rng.Next(6, 8) * 60 * 1000); //second email - evacuation message (6-8 minute)
            sendEvacuateImmediatlyEmailEvent.Elapsed += SendEvacuateImmediatlyMessage;
            sendEvacuateImmediatlyEmailEvent.Start();
            _triggers.Add(sendEvacuateImmediatlyEmailEvent);

            var sendAirAttackWarning = new Timer(_rng.Next(9, 12) * 60 * 1000); //third email - air attack warning (9-12 minute)
            sendAirAttackWarning.Elapsed += SendAirAttackWarning;
            sendAirAttackWarning.Start();
            _triggers.Add(sendAirAttackWarning);

            var sendAllClearAirAttackMessage = new Timer(_rng.Next(14, 16) * 60 * 1000); //forth email - all clear message (14 - 16 minute)
            sendAllClearAirAttackMessage.Elapsed += SendAllClearAirAttackMessage_Elapsed;
            sendAllClearAirAttackMessage.Start();
            _triggers.Add(sendAllClearAirAttackMessage);

            var sendCivilDanger2Message = new Timer(_rng.Next(17, 18) * 60 * 1000); //fifth email - civil danger - evacuate immediatly message (17 - 18 minute)
            sendCivilDanger2Message.Elapsed += SendCivilDanger2Message_Elapsed;
            sendCivilDanger2Message.Start();
            _triggers.Add(sendCivilDanger2Message);

            var sendPresidentialAddress = new Timer(_rng.Next(19, 21) * 60 * 1000); //sixth email - presidential address (19 - 21 minute)
            sendPresidentialAddress.Elapsed += SendPresidentialAddress_Elapsed;
            sendPresidentialAddress.Start();
            _triggers.Add(sendPresidentialAddress);

            var sendShutdownKeyMessage = new Timer(_rng.Next(23, 26) * 60 * 1000); //seventh email - shutdown key (23 - 26 minute)
            sendShutdownKeyMessage.Elapsed += SendShutdownKeyMessage_Elapsed;
            sendShutdownKeyMessage.Start();
            _triggers.Add(sendShutdownKeyMessage);
        }
    }
}
