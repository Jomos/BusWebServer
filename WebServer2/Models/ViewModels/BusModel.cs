using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebServer2.Models.RepositoryModels;

namespace WebServer2.Models.ViewModels
{
    public class BusModel
    {
        public int BusId { get; set; }
        public int TypeId { get; set; }
        [DisplayName("Bus number")]
        public int BusNumber { get; set; }
        [DisplayName("Bus type")]
        public List<TypeListItem> TypesList { get; set; }
        public string Type { get; set; }
        public BusBeacon[] Beacons { get; set; }
    }

    public class TypeListItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}
