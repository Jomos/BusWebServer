using System.Collections.Generic;

namespace WebServer2.Models.RepositoryModels
{
    public class BusType
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Image { get; set; }
        public virtual List<SittingArea> SittingAreas { get; set; }
    }
}