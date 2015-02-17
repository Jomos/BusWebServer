using System;
using System.Collections.Generic;

namespace WebServer2.Models.ApiModels
{
    public class Bus
    {
        public int BusNumber { get; set; }
        public string BusLine { get; set; }
        public List<Passenger> Passengers { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public DateTime Time { get; set; }
        
    }
}
