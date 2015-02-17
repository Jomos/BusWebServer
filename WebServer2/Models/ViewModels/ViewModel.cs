using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using WebServer2.Models.ApiModels;

namespace WebServer2.Models.ViewModels
{
    public class ViewModel
    {
        [DisplayName("Number of passengers")]
        public int NumberOfPassengers { get; set; }
        [DisplayName("Sitting passengers")]
        public int SittingPassengers { get; set; }
        [DisplayName("Standing passengers")]
        public int StandingPassangers { get; set; }
        public List<Person> Persons { get; set; }
        public string BusNumber { get; set; }
        public IEnumerable<SelectListItem> BusNumbers { get; set; }
        public string BusImage { get; set; }
        public string JSON { get; set; }
    }
}