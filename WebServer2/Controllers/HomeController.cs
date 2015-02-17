using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using WebServer2.Models.RepositoryModels;
using WebServer2.Models.ViewModels;
using WebServer2.Services;

namespace WebServer2.Controllers
{
    public class HomeController : Controller
    {
        BeaconService service = new BeaconService();
        
        public ActionResult Index()
        {
            ViewModel viewModel = service.FillViewModel(0);
            
            return View(viewModel);
        }

        
        public ActionResult Select(int busNumber)
        {
            ViewModel viewModel = service.FillViewModel(busNumber);

            return View("Index",viewModel);

        }

        [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 1)] // update evry sec
        public ActionResult GetBusView()
        {
            ViewModel viewModel = service.FillViewModel(0);
            
            return PartialView("_BusView", viewModel);

        }

        public ActionResult BusList()
        {
            var busList = service.GetBusList();
            return View(busList);
        }

        public ActionResult AddBus()
        {
            AddBusModel busModel=new AddBusModel();
            busModel.Beacons=new BusBeacon[3];
            busModel.TypesList = service.GetBusTypes();
            return View(busModel);
        }

        [HttpPost]
        public ActionResult AddBus(AddBusModel model)
        {

            service.AddBus(model);
            return View(model);
        }
    }
}
