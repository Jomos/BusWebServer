using System;
using System.Collections.Generic;

namespace WebServer2.Models.ApiModels
{
    public class JsonObject
    {
        public string Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Speed { get; set; }
        public string Course { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public virtual List<JsonBeacon> Beacons { get; set; }
    }
}
