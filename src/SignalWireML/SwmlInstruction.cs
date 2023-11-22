using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using swml_cs.SignalWireML;

namespace swml_cs.SignalWireML
{
    public class Instruction
    {
        public string InstructionType { get; private set; }
        private Dictionary<string, object> Params { get; set; }
        private string Name { get; set; }

        protected Instruction(string instructionType, Dictionary<string, object> parameters, string name = null)
        {
            InstructionType = instructionType;
            Params = FilterParameters(parameters);
            Name = name;
        }

        private static Dictionary<string, object> FilterParameters(Dictionary<string, object> parameters)
        {
            var filteredParams = new Dictionary<string, object>();
            foreach (var param in parameters)
            {
                if (param.Value != null && param.Key != "class_name")
                {
                    filteredParams.Add(param.Key, param.Value);
                }
            }

            return filteredParams;
        }

        public virtual object Serialize()
        {
            object SerializeRecursively(object obj)
            {
                switch (obj)
                {
                    case Instruction instruction:
                        return instruction.Serialize();
                    case Dictionary<string, object> dict:
                        var serializedDict = new Dictionary<string, object>();
                        foreach (var kvp in dict)
                        {
                            serializedDict[kvp.Key] = SerializeRecursively(kvp.Value);
                        }

                        return serializedDict;
                    case List<object> list:
                        return list.Select(SerializeRecursively).ToList();
                    default:
                        return obj;
                }
            }

            var serializedParams = SerializeRecursively(Params);

            if (!string.IsNullOrEmpty(Name))
            {
                if (serializedParams == null || (serializedParams is Dictionary<string, object> dict && !dict.Any()))
                    return Name; // Return name only if there are no parameters
                return new Dictionary<string, object> { { Name, serializedParams } };
            }
            else
            {
                return serializedParams; // Return only the serialized parameters if no name is provided
            }
        }
    }
}



