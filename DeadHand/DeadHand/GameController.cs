using System;
using DeadHand.Commands.Implementations;
using System.Timers;
using DeadHand.Scenarios.Implementations;
using System.Text;
using DeadHand.Commands.Enums;
using DeadHand.Commands.Abstracts;
using System.IO;
using System.Collections.Generic;

namespace DeadHand
{
    public class GameController
    {
        private Timer _gameTimer;
        private Timer _gameEnterCodeTimer;
        private Timer _deadHandMaintenanceTimer;
        private bool _emailNewMessages;
        private DeadHandCommand _deadHandService;
        private DefragCommand _defragCommand;
        private StatusCommand _statusCommand;
        private CleanCacheCommand _cleanCacheCommand;
        private ScenarioBase _scenario;
        private Random _rng = new Random();
        private Dictionary<int, ScenarioBase> _scenarios = new Dictionary<int, ScenarioBase>();
        internal DeadHandSettings DeadHandSettings { get; set; }
        public void ChceckEmail()
        {
            if (_emailNewMessages)
            {
                Console.WriteLine("email: there are new messages");
                _emailNewMessages = false;
            }
        }

        internal GameController()
        {
            _deadHandService = (DeadHandCommand)CommandBase.GetByIdentifier(CommandIdentifier.deadHand.ToString());
            _defragCommand = (DefragCommand)CommandBase.GetByIdentifier(CommandIdentifier.defrag.ToString());
            _statusCommand = (StatusCommand)CommandBase.GetByIdentifier(CommandIdentifier.status.ToString());
            _cleanCacheCommand = (CleanCacheCommand)CommandBase.GetByIdentifier(CommandIdentifier.cleanCache.ToString());
            _deadHandService.OnSuccesfullDelayCode += StartTimer;
            
            LoadScenarios();
        }

        private void LoadScenarios()
        {
            //get list of files from scenario folder
            var files = Directory.GetFiles("Data/Scenarios");
            var id = 0;
            //for each file
            foreach (var file in files)
            {
                //load scenario with exception handling
                try
                {
                    var scenario = ScenarioBase.FromBinary(file, _deadHandService);
                    if (scenario != null)
                    {
                        _scenarios.Add(id, scenario);
                        id++;
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine($"Error loading scenario file {file}: {ex.Message}");
#endif
                }
            }
            
        }

        public Dictionary<int, string> GetScenarios()
        {
            var scenarios = new Dictionary<int, string>();
            foreach (var scenario in _scenarios)
            {
                scenarios.Add(scenario.Key, scenario.Value.ScenarioName);
            }
            return scenarios;
        }

        private void _deadHandMaintenanceTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DeadHandSettings.DiskFragmentationPercentage += _rng.Next(_scenario.DiskFragmentationPercentageChanges.Item1, _scenario.DiskFragmentationPercentageChanges.Item2);
            DeadHandSettings.MemoryCacheUsedPercentage += _rng.Next(_scenario.MemoryCacheUsedPercentageChanges.Item1, _scenario.MemoryCacheUsedPercentageChanges.Item2);
            DeadHandSettings.MotherboardTemperature = _rng.Next(_scenario.MotherboardTemperatureChanges.Item1, _scenario.MotherboardTemperatureChanges.Item2);
        }

        public void CreateTimeline(int id)
        {
            _scenario = _scenarios[id];
            DeadHandSettings = new DeadHandSettings()
            {
                MotherboardTemperature = _scenario.MotherboardTemperature,
                MemoryCacheUsedPercentage = _scenario.MemoryCacheUsedPercentage,
                DiskFragmentationPercentage = _scenario.DiskFragmentationPercentage
            };
            _defragCommand.CurrentSettings = DeadHandSettings;
            _statusCommand.CurrentSettings = DeadHandSettings;
            _cleanCacheCommand.CurrentSettings = DeadHandSettings;
            _deadHandMaintenanceTimer = new Timer(_rng.Next(2, 4) * Consts.TimerMinute);
            _deadHandMaintenanceTimer.Elapsed += _deadHandMaintenanceTimer_Elapsed;
            _deadHandMaintenanceTimer.Start();
            _scenario.StartScenario();
            _deadHandService.OnSystemShutdown += _scenario.ScenarioEndingShutdown;
            _deadHandService.OnSystemShutdown += () => { _gameTimer.Stop();  };
        }

        public void StartTimer()
        {
            if (_gameTimer != null)
            {
                _gameTimer.Dispose();
            }
            if(_gameEnterCodeTimer != null)
            {
                _gameEnterCodeTimer.Dispose();
            }
            _gameTimer = new Timer(10 * Consts.TimerMinute);
            _gameEnterCodeTimer = new Timer(6 * Consts.TimerMinute);
            _gameEnterCodeTimer.Elapsed += _gameEnterCodeTimer_Elapsed;
            _deadHandService.CurrentTimer = DateTime.Now.AddMinutes(10);
            _deadHandService.AcceptCodeTime = DateTime.Now.AddMinutes(6);
            _gameTimer.Elapsed += _gameTimer_Elapsed;
            _gameTimer.Start();
            _gameEnterCodeTimer.Start();
        }

        private void _gameEnterCodeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Beep(600, 1000);
            Console.Beep(400, 1000);
            Console.Beep(600, 1000);
            Console.Beep(400, 1000);
            Console.WriteLine("Warning: Dead hand will launch in 4 minutes. Enter delay code, if you want to delay the launch");
            ((Timer)sender).Stop();
        }

        private void _gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _scenario.ScenarioEndingLaunch();
            _deadHandService.CancelCommand = true;
        }
    }

    internal class DeadHandSettings
    {
        public int DiskFragmentationPercentage { get; set; }
        public int MemoryCacheUsedPercentage { get; set; }
        public int MotherboardTemperature { get; set; }

        //TODO: Read these values from scenario
        public bool NeedsMaintenance { get => DiskFragmentationPercentage > 50 || MemoryCacheUsedPercentage > 70; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Motherboard Temperature: {MotherboardTemperature}°C");
            sb.Append($"Disk Fragmentation: {DiskFragmentationPercentage}%");

            if(DiskFragmentationPercentage > 50)
            {
                sb.Append(" Warning: defragmentation needed");
            }
            sb.AppendLine("");
            sb.AppendLine($"Memory cache used: {MemoryCacheUsedPercentage}%");

            if (MemoryCacheUsedPercentage > 70)
            {
                sb.Append(" Warning: memory cache cleanup needed");
            }

            return sb.ToString();
        }
    }
}
