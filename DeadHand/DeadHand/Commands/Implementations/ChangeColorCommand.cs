using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;

namespace DeadHand.Commands.Implementations
{
    internal class ChangeColorCommand : CommandBase
    {
        public override string Description => "Change the color of the console font";
        public override CommandIdentifier Identifier => CommandIdentifier.changeColor;
        

        public override void Execute(string param = null)
        {
            try
            {
                var color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), param);
                Console.ForegroundColor = color;
            }
            catch
            {
                Console.WriteLine("Invalid color");
            }
        }
    }
}
