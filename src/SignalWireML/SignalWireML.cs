using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
// Use Newtonsoft.Json for JSON serialization

// Use YamlDotNet for YAML serialization


namespace swml_cs.SignalWireML
{
    public class SignalWireMl
    {
        private readonly Dictionary<string, Section> _sections;

        public SignalWireMl()
        {
            _sections = new Dictionary<string, Section>();
        }

        public Section AddSection(Section newSection)
        {
            if (_sections.ContainsKey(newSection.Name))
            {
                throw new ArgumentException($"Section with name '{newSection.Name}' already exists.");
            }

            _sections[newSection.Name] = newSection;
            return newSection;
        }

        public Section AddSection(string sectionName)
        {
            if (_sections.ContainsKey(sectionName))
            {
                throw new ArgumentException($"Section with name '{sectionName}' already exists.");
            }

            var section = new Section(sectionName);
            _sections[section.Name] = section;
            return section;
        }

        private Dictionary<string, object> SerializeSections()
        {
            var serializedSections = new Dictionary<string, object>();
            foreach (var section in _sections.Values)
            {
                serializedSections[section.Name] = SerializeSectionActions(section);
            }
            return serializedSections;
        }

        private static List<object> SerializeSectionActions(Section section)
        {
            var serializedActions = new List<object>();
            foreach (var action in section.InstructionActions)  // Assuming Section has a public property 'Actions' which is a list of Instruction
            {
                serializedActions.Add(action);  // Serialize each action
            }
            return serializedActions;
        }

        public string GenerateSwml(string dataFormat = "json")
        {
            if (_sections.Count == 0)
            {
                throw new InvalidOperationException("No sections found. Please add at least one section to the SignalWireML object.");
            }

            var serializedSections = SerializeSections();

            if (dataFormat.Equals("json", StringComparison.OrdinalIgnoreCase))
            {
                return JsonConvert.SerializeObject(new { sections = serializedSections });
            }
            else if (dataFormat.Equals("yaml", StringComparison.OrdinalIgnoreCase))
            {
                var serializer = new SerializerBuilder().Build();
                return serializer.Serialize(new { sections = serializedSections });
            }
            else
            {
                throw new ArgumentException($"Invalid data format '{dataFormat}'. Valid formats are 'json' and 'yaml'.");
            }
        }
    }
}
