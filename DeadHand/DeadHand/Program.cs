﻿using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using DeadHand.Commands.Implementations;
using System;
using System.Threading;

namespace DeadHand
{
    class Program
    {
        private static GameController _gameController;

        public static GameController GameController { get => _gameController; }

        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Green;
            SetupGame();
#if !DEBUG
            SimulateOSStart();
#endif
            while (true)
            {
                DecodeCommand(CommandPrompt());
            }
        }

        private static void SetupGame()
        {
            //FIXIT: this does not feel good...
            _gameController = new GameController((EmailCommand)CommandBase.GetByIdentifier(CommandIdentifier.email.ToString()), 
                                                 (DeadHandCommand)CommandBase.GetByIdentifier(CommandIdentifier.deadHand.ToString()),
                                                 (CheckRadioCommand)CommandBase.GetByIdentifier(CommandIdentifier.checkRadio.ToString()),
                                                 (DefragCommand)CommandBase.GetByIdentifier(CommandIdentifier.defrag.ToString()),
                                                 (StatusCommand)CommandBase.GetByIdentifier(CommandIdentifier.status.ToString()),
                                                 (CleanCacheCommand)CommandBase.GetByIdentifier(CommandIdentifier.cleanCache.ToString()),
                                                 (WeatherServiceCommand)CommandBase.GetByIdentifier(CommandIdentifier.weatherService.ToString()));
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
            //TODO: Better OS start
            var rnd = new Random(); 
            Console.Write("Booting GuardianOS 93 by GuardSoft (Military version)");
            for (int i = 0; i < rnd.Next(2, 8); i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine("MemoryTest: OK");
            Console.Write("Logging in");
            for (int i = 0; i < rnd.Next(2, 8); i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine($"Hello {Environment.UserName}!");
            Console.Write("Checking weapon system status");
            for (int i = 0; i < rnd.Next(2, 8); i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine("All systems nominal");
            Console.WriteLine("Starting command prompt");
            Thread.Sleep(500);
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
