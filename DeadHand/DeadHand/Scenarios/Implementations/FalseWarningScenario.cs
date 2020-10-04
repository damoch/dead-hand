using DeadHand.Commands.Implementations;
using DeadHand.Scenarios.Abstracts;
using System;
using System.Threading;
using System.Timers;

namespace DeadHand.Scenarios.Implementations
{
    internal class FalseWarningScenario : ScenarioBase
    {
        public FalseWarningScenario(EmailCommand emailService) : base(emailService)
        {

        }

        public override string ScenarioName => "False warning";

        public override void ScenarioEndingLaunch()
        {
            SimulateLaunch();
            Console.Clear();
            Console.WriteLine(@"
EPILOGUE: Asset responsible for Dead Hand system, had mistaken military exercise of 'hostile force' as a ligitimate threat, and allowed automatic retaliation system to perform
nuclear attack.

'Hostile force' seeing hundrets of ballistic missles coming their way, launched their own nuclear attack. Other nations seeing that laucnched their own arsennals, mutually destroying
each other in this process.
" + $"{Environment.UserName} has starved to death, after supplies ended in his bunker\n\n" +
$"Scenario name: False warning\n" +
$"This scenario has one more ending\n" +
$"" +
$"Thanks for playing Dead Hand, developed by kszaku in October of 2020\n");
        }

        public override void ScenarioEndingShutdown()
        {
            throw new System.NotImplementedException();
        }

        public override void StartScenario()
        {
            var sendCivilDangerEmailEvent = new System.Timers.Timer(_rng.Next(1,3) * 60 * 1000); //first email - warning message (1-3 minute)
            sendCivilDangerEmailEvent.Elapsed += SendCivilDangerEmail;
            sendCivilDangerEmailEvent.Start();
            _triggers.Add(sendCivilDangerEmailEvent);

            var sendEvacuateImmediatlyEmailEvent = new System.Timers.Timer(_rng.Next(6, 8) * 60 * 1000); //second email - evacuation message (6-8 minute)
            sendEvacuateImmediatlyEmailEvent.Elapsed += SendEvacuateImmediatlyMessage;
            sendEvacuateImmediatlyEmailEvent.Start();
            _triggers.Add(sendEvacuateImmediatlyEmailEvent);

            var sendAirAttackWarning = new System.Timers.Timer(_rng.Next(9, 12) * 60 * 1000); //third email - air attack warning (9-12 minute)
            sendAirAttackWarning.Elapsed += SendAirAttackWarning;
            sendAirAttackWarning.Start();
            _triggers.Add(sendAirAttackWarning);

            var sendAllClearAirAttackMessage = new System.Timers.Timer(_rng.Next(14, 16) * 60 * 1000); //forth email - all clear message (14 - 16 minute)
            sendAllClearAirAttackMessage.Elapsed += SendAllClearAirAttackMessage_Elapsed;
            sendAllClearAirAttackMessage.Start();
            _triggers.Add(sendAllClearAirAttackMessage);

            var sendCivilDanger2Message = new System.Timers.Timer(_rng.Next(17, 18) * 60 * 1000); //fifth email - civil danger - evacuate immediatly message (17 - 18 minute)
            sendCivilDanger2Message.Elapsed += SendCivilDanger2Message_Elapsed;
            sendCivilDanger2Message.Start();
            _triggers.Add(sendCivilDanger2Message);

            var sendPresidentialAddress = new System.Timers.Timer(_rng.Next(19, 21) * 60 * 1000); //sixth email - presidential address (19 - 21 minute)
            sendPresidentialAddress.Elapsed += SendPresidentialAddress_Elapsed;
            sendPresidentialAddress.Start();
            _triggers.Add(sendPresidentialAddress);

            var sendShutdownKeyMessage = new System.Timers.Timer(_rng.Next(23, 26) * 60 * 1000); //seventh email - shutdown key (23 - 26 minute)
            sendShutdownKeyMessage.Elapsed += SendShutdownKeyMessage_Elapsed;
            sendShutdownKeyMessage.Start();
            _triggers.Add(sendShutdownKeyMessage);

            var sendFinalAllClearMessage = new System.Timers.Timer(_rng.Next(24, 26) * 60 * 1000); //eighth email - all clear
            sendFinalAllClearMessage.Elapsed += SendFinalAllClearMessage_Elapsed;
            sendFinalAllClearMessage.Start();
            _triggers.Add(sendFinalAllClearMessage);
        }
    }
}
