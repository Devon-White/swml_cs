using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace swml_cs.SignalWireML
{
    public class Play : Instruction
    {
        public Play(IReadOnlyCollection<string> urls = null, string url = null, float? volume = null, string sayVoice = null, float? silence = null, Tuple<float, string> ring = null)
            : base("play", ConstructParams(urls, url, volume, sayVoice, silence, ring))
        {
            // Check for mutually exclusive 'url' and 'urls' parameters
            if (url != null && urls != null)
            {
                throw new ArgumentException("Cannot provide both 'url' and 'urls'. Please provide only one.");
            }
        }

        private static Dictionary<string, object> ConstructParams(IReadOnlyCollection<string> urls, string url, float? volume, string sayVoice, float? silence, ITuple ring)
        {
            var paramsDict = new Dictionary<string, object>
            {
                { "urls", urls },
                { "url", url },
                { "volume", volume },
                { "say_voice", sayVoice },
                { "silence", silence },
                { "ring", ring }
            };

            return paramsDict;
        }
    }

}