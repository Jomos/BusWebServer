using System.Collections.Generic;

namespace WebServer2.Models.RepositoryModels
{
    public class Bus
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int TypeId { get; set; }
        public virtual List<BusBeacon> Beacons { get; set; }
    }
}