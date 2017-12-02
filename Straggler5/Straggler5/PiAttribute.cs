using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Straggler5
{
    public class PiAttribute
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty(PropertyName = "measures")]
        public List<Measure> Measures { get; set; }
    }
}
