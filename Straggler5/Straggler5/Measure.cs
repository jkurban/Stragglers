using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Straggler5
{
    public class Measure
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
