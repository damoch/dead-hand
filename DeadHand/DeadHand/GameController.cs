using System;
using System.Collections.Generic;
using DeadHand.Commands.Implementations;
using System.Timers;
using DeadHand.Scenarios.Abstracts;
using DeadHand.Scenarios.Implementations;
using System.Text;

namespace DeadHand
{
    public class GameController
    {
        internal EmailCommand EmailService { get; set; }

        private System.Timers.Timer _gameTimer;
        private System.Timers.Timer _deadHandMaintenanceTimer;
        private bool _emailNewMessages;
        private TimeLeftCommand _timerService;
        private InsertCodeCommand _codeService;
        private CheckRadioCommand _radioService;
        private DefragCommand _defragCommand;
        private StatusCommand _statusCommand;
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
                                TimeLeftCommand timerService,
                                InsertCodeCommand codeService,
                                CheckRadioCommand checkRadioCommand,
                                DefragCommand defragCommand,
                                StatusCommand statusCommand)
        {
            SetupEmailService(emailService);
            _timerService = timerService;
            _codeService = codeService;
            _radioService = checkRadioCommand;
            _defragCommand = defragCommand;
            _statusCommand = statusCommand;
            _codeService.OnSuccesfullDelayCode += StartTimer;
            DeadHandSettings = new DeadHandSettings()
            {
                MotherboardTemperature = 80,
                MemoryCacheUsedPercentage = 20,
                DiskFragmentationPercentage = 10
            };

            _deadHandMaintenanceTimer = new Timer(_rng.Next(_rng.Next(2, 4) * 60 * 1000));
            _deadHandMaintenanceTimer.Elapsed += _deadHandMaintenanceTimer_Elapsed;
            _deadHandMaintenanceTimer.Start();
            _defragCommand.CurrentSettings = DeadHandSettings;
            _statusCommand.CurrentSettings = DeadHandSettings;

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
            _scenario = new FalseWarningScenario(EmailService, _radioService);
            _scenario.StartScenario();
            _codeService.OnSystemShutdown += _scenario.ScenarioEndingShutdown;

        }

        public void StartTimer()
        {
            if (_gameTimer != null)
            {
                _gameTimer.Dispose();
            }
            _gameTimer = new System.Timers.Timer(7 * 60 * 1000);
            _timerService.CurrentTimer = DateTime.Now.AddSeconds(7 * 60);
            _gameTimer.Elapsed += _gameTimer_Elapsed;
            _gameTimer.Start();
        }

        private void _gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _scenario.ScenarioEndingLaunch();
            _codeService.CancelCommand = true;
        }

        private void SetupEmailService(EmailCommand emailService)
        {
            EmailService = emailService;
            emailService.EmailList = new List<Email>()
            {
                new Email()
                {
                    ReceivedDate = DateTime.Now,
                    Subject = "Order no. 583",
                    Sender = "command@stratcom.com",
                    Content = @"To asset "+Environment.UserName+@", 
Since peace talks in Geneva have been cancelled, and hostile armed force has issued red alert to their strategic weapons division, Strategic Command has issued STANDBY alert.

1. "+Environment.UserName+" has been authorized to enter following Dead Hand activation code: 2DCJ-CA83-8A9H-A9HD" +
"\n2. 7 minutes after activation Dead Hand will automatically launch retaliationary nuclear strike against enemy cities" +
"\n3. In final 2 minutes before activation "+Environment.UserName+" will get the chance to enter following cancellation code: F7SA-USA7-JA98-CDSA. Entering that code will delay retaliation for 7 minutes"+
"\n4. To help "+Environment.UserName+" make decision whether enter the code and wait another 7 minutes, or allow attack to commence, following checks may be performed:"+
"\na) Check whether National Radio program 4 is still broadcasting"+
"\nb) Check whether Naval Wether Service stil issues weather updates"+
"\nIn addition, all National Civil Defense Service broadcasts will be redirected to "+Environment.UserName+" email inbox in text form"+
"\n5. "+Environment.UserName+" is responsible for operation of computer controling Dead Hand system"+
"\n6. Dead Hand may be shut down only by entering shutdown code, that will be issued in ALL CLEAR message from Strategic Command"
                }
            };
        }

        internal void AddEmail(Email email)
        {
            EmailService.EmailList.Add(email);
            _emailNewMessages = true;
        }


    }

    internal class DeadHandSettings
    {
        public int DiskFragmentationPercentage { get; set; }
        public int MemoryCacheUsedPercentage { get; set; }
        public int MotherboardTemperature { get; set; }

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
            sb.AppendLine($"Memory chaed used: {MemoryCacheUsedPercentage}%");

            if (MemoryCacheUsedPercentage > 70)
            {
                sb.Append(" Warning: memory cache cleanup needed");
            }

            return sb.ToString();
        }
    }
}
