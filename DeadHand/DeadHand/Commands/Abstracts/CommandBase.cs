using System;
using System.Collections.Generic;
using DeadHand.Commands.Enums;
using DeadHand.Commands.Implementations;

namespace DeadHand.Commands.Abstracts
{
    internal abstract class CommandBase
    {
        private static Dictionary<CommandIdentifier, CommandBase> _implementations = new Dictionary<CommandIdentifier, CommandBase> {
            { CommandIdentifier.help, new HelpCommand() },
            { CommandIdentifier.email, new EmailCommand() },
            { CommandIdentifier.shutdown, new ShutdownCommand() },
            { CommandIdentifier.timeLeft, new TimeLeftCommand() },
            { CommandIdentifier.deadHand, new InsertCodeCommand() },
            { CommandIdentifier.checkRadio, new CheckRadioCommand() },
            { CommandIdentifier.weatherService, new WeatherServiceCommand() },
            { CommandIdentifier.defrag, new DefragCommand() }
        };
        public abstract CommandIdentifier Identifier { get; }
        public abstract string Description { get; }
        protected Random _rng = new Random();

        public abstract void Execute(string param = null);

        public static CommandBase GetByIdentifier(string identifier)
        {
            try
            {
                var enumVal = (CommandIdentifier)Enum.Parse(typeof(CommandIdentifier), identifier);
                return _implementations[enumVal];

            }
            catch
            {
                return null;
            }
        }

        public static void AddCommand(CommandBase command)
        {
            if (_implementations.ContainsKey(command.Identifier))
            {
                return;
            }
            _implementations.Add(command.Identifier, command);

        }

        public void WriteAllInstalled()
        {
            foreach (var item in _implementations.Keys)
            {
                Console.WriteLine(item);
            }
        }
    }
}
