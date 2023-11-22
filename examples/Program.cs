using System;
using swml_cs.SignalWireML;

namespace swml_cs.examples
{
    internal class Program // Removed 'abstract' keyword
    {
        private static void Main()
        {
            Console.WriteLine("Initializing SignalWireML...");

            var swml = new SignalWireMl();
            var mainSection = new Section("main");
            var playInstruction = new Play(url: "https://cdn.signalwire.com/default-music/welcome.mp3");
            mainSection.AddInstruction(playInstruction);
            swml.AddSection(mainSection);

            var swmlOutput = swml.GenerateSwml("yaml");
            Console.WriteLine("SWML Output:");
            Console.WriteLine(swmlOutput);

            // If ToString is overridden in SignalWireMl
            Console.WriteLine("SWML ToString Output:");
            Console.WriteLine(swml);
        }
    }
}