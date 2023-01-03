using System;
using System.Diagnostics;
using System.Threading;
using DeadHand.Commands.Abstracts;
using DeadHand.Commands.Enums;

namespace DeadHand.Commands.Implementations
{
    internal class ECMSCommand : CommandBase
    {
        public override CommandIdentifier Identifier => CommandIdentifier.ecms;

        public override string Description => "D̗̺̝̣̹̝͞o͎̮ͅ ͏̟̞̩̭̫̙n͚͕͓̰̥͎̥o̞̲͍̹̙̻ṱ̪̯̯̪̩ ̺̻͘di̦̞̝̹̲̗̻s͎͇̜͉̪̳a̲̤p̻p̗̱̘o̩̺̼̳̙̳̜͟ị̝̠͙͎̲͖͡n͔͍t̯͇́ ͓̹͚̥̱u͉̖͚̼̻̬̕s̯̝͕̙̜͙";

        public override void Execute(string param = null)
        {
            Console.WriteLine("Starting ECMS");
            Process.Start("https://damoch.itch.io/evil-cult-managment-system");
            Thread.Sleep(5000);
        }
    }
}
