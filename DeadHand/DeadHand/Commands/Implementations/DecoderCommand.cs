using System;
using System.Text;
using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;

namespace DeadHand.Commands.Implementations
{
    internal class DecoderCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.decoder;

        public override string Description => "Software used to decode encrypted text. No parameters";

        public override void Execute(string param = null)
        {
            Console.WriteLine("Type (or paste) text to decode, and press enter");
            var input = Console.ReadLine();
            var result = Convert.FromBase64String(input);
            if(result.Length == 0)
            {
                Console.WriteLine("No input");
                return;

            }
            var base64Decoded = ASCIIEncoding.ASCII.GetString(result);

            Console.WriteLine("Result:");
            Console.WriteLine(base64Decoded);
        }
    }
}
