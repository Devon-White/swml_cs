using System;
using System.Collections.Generic;

namespace swml_cs.SignalWireML
{
    public class Section
    {
        public string Name { get; private set; }
        internal readonly List<object> InstructionActions;

        public Section(string name)
        {
            Name = name;
            InstructionActions = new List<object>();
        }

        public void AddInstruction(Instruction instruction)
        {
            InstructionActions.Add(instruction.Serialize());
        }

        // Example translation of a method
        public void Play(string url = null, List<string> urls = null, float? volume = null, string sayVoice = null, float? silence = null, Tuple<float, string> ring = null)
        {
            AddInstruction(new Play(urls, url, volume, sayVoice, silence, ring));
        }

    }
}