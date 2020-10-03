using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;
using System;

namespace DeadHand.Commands.Implementations
{
    internal class InsertCodeCommand : CommandBase
    {
        private string _code = "F7SA-USA7-JA98-CDSA";
        public override CommandIdentifier Identifier => CommandIdentifier.insertCode;
        public Action OnSuccesfullCode { get; set; }

        public override string Description => "Allows you to insert code to cancel Dead Hand activation";

        public override void Execute(string param = null)
        {
            Console.WriteLine("Please insert deactivation code and press enter: ");
            var result = Console.ReadLine();

            if(result == _code)
            {
                Console.WriteLine("Code correct. Next Dead Hand activation in 7 minutes");
                OnSuccesfullCode.Invoke();
            }
            else
            {
                Console.WriteLine("Code incorrect");
            }
        }
    }
}
