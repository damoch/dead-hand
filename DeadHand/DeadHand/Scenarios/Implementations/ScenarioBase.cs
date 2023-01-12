using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using DeadHand.Commands.Implementations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace DeadHand.Scenarios.Abstracts
{
    internal class ScenarioBase
    {
        public  Tuple<int, int> DiskFragmentationPercentageChanges { get; set; }
        public  Tuple<int, int> MemoryCacheUsedPercentageChanges { get; set; }
        public  Tuple<int, int> MotherboardTemperatureChanges { get; set; }
        public  string DelayCode { get; set; }
        public  string ActivationCode { get; set; }
        public  string ShutdownCode { get; set; }
        public  string RadioStationID { get; set; }
        public  Tuple<string, string> WeatherServiceData { get; set; }
        public  string ScenarioName { get; set; }
        protected Random _rng;
        protected WeatherServiceCommand _weatherServiceCommand;
        private DeadHandCommand _deadHandCommand;
        protected EmailCommand _emailService;
        protected CheckRadioCommand _radioService;
        protected List<System.Timers.Timer> _triggers;
        public Dictionary<int, Email> Emails { get; set; }
        public string EndingLaunchText { get; set; }
        public string EndingShutdownText { get; set; }
        public  int MotherboardTemperature { get; set; }
        public  int MemoryCacheUsedPercentage { get; set; }
        public  int DiskFragmentationPercentage { get; set; }

        public void StartScenario()
        {
            _radioService.SetCommandData(RadioStationID);
            _weatherServiceCommand.SetCommandData(WeatherServiceData.Item1, WeatherServiceData.Item2);
            _deadHandCommand.SetCodes(ActivationCode, ShutdownCode, DelayCode);
            foreach (var email in Emails)
            {
                var newEvent = new System.Timers.Timer(email.Key * Consts.TimerMinute);
                newEvent.Elapsed += (o, e) =>
                {
                    _emailService.AddEmail(email.Value);
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
            Shutdown();
        }

        private void Shutdown()
        {
            Console.WriteLine("PRESS ANY KEY TO EXIT");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public void ScenarioEndingShutdown()
        {
            SimulateShutdown();
            Console.Clear();
            Console.WriteLine(EndingShutdownText);
            Shutdown();
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
            Console.WriteLine("");
            foreach (var c in "WEAPON SYSTEMS DISENGAGED")
            {
                Console.Write(c);
                Console.Beep(900, 30);
                Thread.Sleep(30);
            }
            Thread.Sleep(5000);
            Console.Clear();
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

        internal bool ToJson(string name)
        {
            try
            {
                var json = JsonConvert.SerializeObject(this, Formatting.Indented);
                
#if !DEBUG
var bytes = Encoding.UTF8.GetBytes(json);
var base64 = Convert.ToBase64String(bytes);
json = base64;
#endif

                File.WriteAllText($"{name}.json", json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public static ScenarioBase FromJson(string name, DeadHandCommand command)
        {
            try
            {
                var json = File.ReadAllText(name);
#if !DEBUG
                var bytes = Convert.FromBase64String(json);
                json = Encoding.UTF8.GetString(bytes);
#endif
                
                var scenario = JsonConvert.DeserializeObject<ScenarioBase>(json);
                scenario._deadHandCommand = command;
                return scenario;
            }

            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex);
#else
Console.WriteLine("Error loading scenario");
#endif
                return null;
            }
        }
    }
}
