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
    public class AddBusModel
    {
        [DisplayName("Bus number")]
        public int BusNumber { get; set; }
        [DisplayName("Bus type")]
        public IEnumerable<SelectListItem> TypesList { get; set; }
        public int Type { get; set; }
        public BusBeacon[] Beacons { get; set; }
    }
}
