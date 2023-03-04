using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using DeadHand.Commands.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DeadHand
{
    class Program
    {
        private static GameController _gameController;
        private static Dictionary<int, string> _scenarios;

        public static GameController GameController { get => _gameController; }

        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Green;
            SetupGame();
            PickScenario();
#if !DEBUG
            SimulateOSStart();
#endif
            while (true)
            {
                DecodeCommand(CommandPrompt());
            }
        }

        private static void PickScenario()
        {
            Console.WriteLine("Welcome to Dead Hand");
            Console.WriteLine("Pick scenario by entering its number and pressing ENTER:");
            foreach (var scenario in _scenarios)
            {
                Console.WriteLine($"{scenario.Key} - {scenario.Value}");
            }
            while (true)
            {
                try
                {
                    var scenarioId = int.Parse(Console.ReadLine());
                    _gameController.CreateTimeline(scenarioId);
                    Console.Clear();
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid scenario number");
#if DEBUG
                    Console.WriteLine(ex.Message);
#endif
                }

            }

            
        }

        private static void SetupGame()
        {
            _gameController = new GameController();
            _scenarios = _gameController.GetScenarios();
        }

        private static void DecodeCommand(string command)
        {
            if (_gameController.DeadHandSettings.NeedsMaintenance)
            {
                Thread.Sleep(8000);
                Console.WriteLine($"This seems to take longer than usual. Try to use {CommandIdentifier.status} command on info, about what can be done to resolve this issue");
                Thread.Sleep(14000);
            }
            var commTab = command.Split(' ');
            var commName = commTab[0];

            try
            {
                var cmd = CommandBase.GetByIdentifier(commName);

                if(commTab.Length == 1)
                {
                    cmd.Execute();
                }
                if(commTab.Length == 2)
                {
                    cmd.Execute(commTab[1]);
                }
                if(commTab.Length > 2)
                {
                    cmd.Execute(commTab[1] + " " + commTab[2]);
                }
            }
            catch
            {
                Console.WriteLine("Unknown command {0}", commName);
            }
        }

        private static void SimulateOSStart()
        {
            var rnd = new Random();
            Console.WriteLine("Graphics device detected, setting up...");
            Thread.Sleep(2500);
            Console.WriteLine("Boozer test");
            Console.Beep(600, 600);
            Console.WriteLine("OK");
            Console.WriteLine("Computer configuration:");
            Console.WriteLine("CPU: DEC21064-CA-166 1GHz");
            Console.Write("Memory check");
            for (int i = 0; i < rnd.Next(2, 8); i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine("256MB detected\n");
            Thread.Sleep(1000);
            Console.WriteLine("Listing IDE devices...");
            Thread.Sleep(2000);
            Console.WriteLine("A: 3+1⁄2-inch HD ");
            Thread.Sleep(1000);
            Console.WriteLine("B: Zip drive");
            Thread.Sleep(500);
            Console.WriteLine("D: CD-ROM drive");
            Thread.Sleep(500);
            Console.WriteLine("F: Tape device");
            Thread.Sleep(3000);
            Console.WriteLine("C: HDD Drive");
            Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine("Booting from default storage device...");
            Thread.Sleep(3000);
            Console.Clear();
            var logo = File.ReadAllLines("Data/GuardianOSlogo.txt");
            foreach (var line in logo)
            {
                foreach (var c in line)
                {
                    Console.Write(c);
                }
                Thread.Sleep(50);
                Console.Write('\n');
            }
            Consts.PlayMelody(Consts.OSStartSound);
            Thread.Sleep(3000);
            Console.Clear();
            //TODO: Better OS start
            Console.Write("Starting GuardianOS");
            for (int i = 0; i < rnd.Next(2, 8); i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.Write("\nLogging in");
            for (int i = 0; i < rnd.Next(2, 8); i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine($"Hello {Environment.UserName}!");
            Console.Write("Establishing connection to Dead Hand main computer");
            for (int i = 0; i < rnd.Next(2, 8); i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine("Connection OK!");
            Console.Write("Checking weapon system status");
            for (int i = 0; i < rnd.Next(2, 8); i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine("All systems nominal");
            Console.WriteLine("Starting command prompt");
            Thread.Sleep(1000);
            Console.WriteLine("OK!");
            Console.WriteLine("Type 'help' to get list of commands");
        }

        private static string CommandPrompt()
        {
            _gameController.ChceckEmail();
            Console.Write(">>> ");
            return Console.ReadLine();
        }
    }
}
