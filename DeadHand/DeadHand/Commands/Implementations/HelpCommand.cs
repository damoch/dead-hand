using System;
using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;

namespace DeadHand.Commands.Implementations
{
    internal class HelpCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.help;

        public override string Description => "Provides info about command. Usage: 'help commandName'";

        public override void Execute(string param = null)
        {
            if(param == null)
            {
                Console.WriteLine(Description);

                WriteAllInstalled();
                return;
            }
            var cmd = GetByIdentifier(param);
            if(cmd == null)
            {
                Console.WriteLine("Unknown command {0},", param);
            }
            Console.WriteLine(cmd.Description);
        }
    }
}
