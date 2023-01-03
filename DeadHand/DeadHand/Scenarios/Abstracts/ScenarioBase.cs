using DeadHand.Commands.Implementations;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DeadHand.Scenarios.Abstracts
{
    internal abstract class ScenarioBase
    {
        public abstract string DelayCode { get; }
        public abstract string ActivationCode { get; }
        public abstract string ShutdownCode { get; }
        public abstract string RadioStationID { get; }
        public abstract Tuple<string, string> WeatherServiceData { get; }
        //TODO: remove unused command references
        public ScenarioBase(EmailCommand emailService,
                            CheckRadioCommand radioService,
                            WeatherServiceCommand weatherServiceCommand,
                            DeadHandCommand deadHandCommand)
        {
            _rng = new Random();
            _emailService = emailService;
            _triggers = new List<System.Timers.Timer>();
            _radioService = radioService;
            _weatherServiceCommand = weatherServiceCommand;
            _deadHandCommand = deadHandCommand;
        }
        public abstract string ScenarioName { get; }
        protected Random _rng;
        protected WeatherServiceCommand _weatherServiceCommand;
        private DeadHandCommand _deadHandCommand;
        protected EmailCommand _emailService;
        protected CheckRadioCommand _radioService;
        protected List<System.Timers.Timer> _triggers;
        protected Dictionary<int, Email> _emails;
        public abstract string EndingLaunchText { get; }
        public abstract string EndingShutdownText { get; }

        public void StartScenario()
        {
            _radioService.SetCommandData(RadioStationID);
            _weatherServiceCommand.SetCommandData(WeatherServiceData.Item1, WeatherServiceData.Item2);
            _deadHandCommand.SetCodes(ActivationCode, ShutdownCode, DelayCode);
            foreach (var email in _emails)
            {
                var newEvent = new System.Timers.Timer(email.Key);
                newEvent.Elapsed += (o, e) =>
                {
                    _emailService.AddEmail(email.Value, true);
                    _radioService.CurrentProgramming = email.Value.ProgrammingType;
                    ((System.Timers.Timer)o).Stop();
                };
                newEvent.Start();
                _triggers.Add(newEvent);
            }
        }

        public void ScenarioEndingLaunch()
        {
            SimulateLaunch();
            Console.Clear();
            Console.WriteLine(EndingLaunchText);
        }

        public void ScenarioEndingShutdown()
        {
            SimulateShutdown();
            Console.Clear();
            Console.WriteLine(EndingShutdownText);
        }

        protected void SimulateShutdown()
        {
            CancellAllEvents();
            Console.Beep(400, 1000);
            Console.Beep(500, 1000);
            Console.Beep(600, 1000);
            Console.Beep(700, 1000);
            Console.Beep(700, 1000);
            Console.Beep(700, 1000);
            Console.Beep(700, 1000);
            Console.Beep(700, 1000);
            Console.Beep(600, 1000);
            Console.Beep(500, 1000);
            Console.Beep(400, 1000);
            Console.Clear();            
            Thread.Sleep(2000);
            foreach (var c in "USER HAS ENTERED CORRECT SHUTDOWN CODE")
            {
                Console.Write(c);
                Console.Beep(900, 30);
                Thread.Sleep(30);
            }
            Thread.Sleep(2000);
            foreach (var c in "WEAPON SYSTEMS DISENGAGED")
            {
                Console.Write(c);
                Console.Beep(900, 30);
                Thread.Sleep(30);
            }
        }

        protected void SimulateLaunch()
        {
            CancellAllEvents();
            Console.Beep(400, 1000);
            Console.Beep(500, 1000);
            Console.Beep(600, 1000);
            Console.Beep(700, 1000);
            Console.Beep(700, 1000);
            Console.Beep(700, 1000);
            Console.Beep(700, 1000);
            Console.Beep(700, 1000);
            Console.Beep(600, 1000);
            Console.Beep(500, 1000);
            Console.Beep(400, 1000);
            Thread.Sleep(500);
            Console.Clear();
            Thread.Sleep(2000);
            foreach (var c in "DEAD HAND SYSTEM ACTIVATED")
            {
                Console.Write(c);
                Console.Beep(900, 30);
                Thread.Sleep(30);
            }
            Thread.Sleep(2000);
            Console.WriteLine("");

            foreach (var c in "PROCEEDING TO LAUNCH BALLISTIC MISSLES TOWARDS DESIGNATED TARGETS")
            {
                Console.Write(c);
                Console.Beep(900, 30);
                Thread.Sleep(30);
            }
            Thread.Sleep(5000);
            Console.Clear();
            foreach (var c in "LOADING TARGETS LIST...")
            {
                Console.Write(c);
                Console.Beep(900, 30);
                Thread.Sleep(30);
            }
            Thread.Sleep(5000);
            Console.WriteLine("");

            Console.WriteLine("Code: Alpha\nType: Air base\nEstimated casualties: 20k");
            Console.Beep(400, 500);
            Thread.Sleep(500);
            Console.WriteLine("Code: Bravo\nType: Naval base\nEstimated casualties: 10k");
            Console.Beep(400, 500);
            Thread.Sleep(500);
            Console.WriteLine("Code: Charlie\nType: Population centre\nEstimated casualties: 1 million");
            Console.Beep(400, 500);
            Thread.Sleep(500);
            Console.WriteLine("Code: Delta\nType: Industrial centre\nEstimated casualties: 500k");
            Console.Beep(400, 500);
            Thread.Sleep(500);
            Console.WriteLine("Code: Echo\nType: Naval base\nEstimated casualties: 500k");
            Console.Beep(400, 300);
            Thread.Sleep(300);
            Console.WriteLine("Code: Foxtrot\nType: Naval base\nEstimated casualties: 10k");
            Console.Beep(400, 300);
            Thread.Sleep(300);
            Console.WriteLine("Code: Golf\nType: Air base\nEstimated casualties: 10k");
            Console.Beep(400, 200);
            Thread.Sleep(200);
            Console.WriteLine("Code: Hotel\nType: Population centre\nEstimated casualties: 100k");
            Console.Beep(400, 200);
            Thread.Sleep(200);
            Console.WriteLine("Code: India\nType: Population centre\nEstimated casualties: 700k");
            Console.Beep(400, 200);
            Thread.Sleep(200);
            Console.WriteLine("Code: India\nType: Ballistic misle launch site\nEstimated casualties: UNKNOWN");
            Console.Beep(400, 200);
            Thread.Sleep(200);
            Console.WriteLine("Code: Juliet\nType: Ballistic misle launch site\nEstimated casualties: UNKNOWN");
            Console.Beep(400, 200);
            Thread.Sleep(200);
            Console.WriteLine("Code: Kilo\nType: Ballistic misle launch site\nEstimated casualties: UNKNOWN");
            Console.Beep(400, 200);
            Console.Beep(400, 50);
            Thread.Sleep(50);

            Console.Clear();
            foreach (var c in "GOOD BYE")
            {
                Console.Write(c);
                Console.Beep(900, 50);
                Thread.Sleep(500);
            }
            Thread.Sleep(5000);
            Console.Clear();
        }

        protected void CancellAllEvents()
        {
            foreach (var item in _triggers)
            {
                item.Stop();
            }
        }
    }
}
