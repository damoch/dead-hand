using System;
using DeadHand.Commands.Implementations;
using System.Timers;
using DeadHand.Scenarios.Abstracts;
using DeadHand.Scenarios.Implementations;
using System.Text;

namespace DeadHand
{
    public class GameController
    {
        private EmailCommand _emailService;
        private Timer _gameTimer;
        private Timer _gameEnterCodeTimer;
        private Timer _deadHandMaintenanceTimer;
        private bool _emailNewMessages;
        private DeadHandCommand _deadHandService;
        private CheckRadioCommand _radioService;
        private DefragCommand _defragCommand;
        private StatusCommand _statusCommand;
        private WeatherServiceCommand _weatherServiceCommand;
        private CleanCacheCommand _cleanCacheCommand;
        private ScenarioBase _scenario;
        private Random _rng = new Random();
        internal DeadHandSettings DeadHandSettings { get; set; }
        public void ChceckEmail()
        {
            if (_emailNewMessages)
            {
                Console.WriteLine("email: there are new messages");
                _emailNewMessages = false;
            }
        }

        internal GameController(EmailCommand emailService,
                                DeadHandCommand codeService,
                                CheckRadioCommand checkRadioCommand,
                                DefragCommand defragCommand,
                                StatusCommand statusCommand,
                                CleanCacheCommand cleanCacheCommand,
                                WeatherServiceCommand weatherServiceCommand)
        {
            _emailService = emailService;
            _deadHandService = codeService;
            _radioService = checkRadioCommand;
            _defragCommand = defragCommand;
            _statusCommand = statusCommand;
            _cleanCacheCommand = cleanCacheCommand;
            _weatherServiceCommand = weatherServiceCommand;
            _deadHandService.OnSuccesfullDelayCode += StartTimer;
            DeadHandSettings = new DeadHandSettings()
            {
                MotherboardTemperature = 80,
                MemoryCacheUsedPercentage = 20,
                DiskFragmentationPercentage = 10
            };

            _deadHandMaintenanceTimer = new Timer(_rng.Next(2, 4) * 60 * 1000);
            _deadHandMaintenanceTimer.Elapsed += _deadHandMaintenanceTimer_Elapsed;
            _deadHandMaintenanceTimer.Start();
            _defragCommand.CurrentSettings = DeadHandSettings;
            _statusCommand.CurrentSettings = DeadHandSettings;
            _cleanCacheCommand.CurrentSettings = DeadHandSettings;

            CreateTimeline();
        }

        private void _deadHandMaintenanceTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DeadHandSettings.DiskFragmentationPercentage += _rng.Next(0, 40);
            DeadHandSettings.MemoryCacheUsedPercentage += _rng.Next(0, 40);
            DeadHandSettings.MotherboardTemperature = _rng.Next(60, 120);
        }

        private void CreateTimeline()
        {
            _scenario = new FalseWarningScenario(_emailService, _radioService, _weatherServiceCommand, _deadHandService);
            _scenario.StartScenario();
            _deadHandService.OnSystemShutdown += _scenario.ScenarioEndingShutdown;
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
            _gameTimer = new Timer(10 * 60 * 1000);
            _gameEnterCodeTimer = new Timer(4 * 60 * 1000);
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
