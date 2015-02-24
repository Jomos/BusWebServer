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

        
        public ActionResult Select(int busId)
        {
            ViewModel viewModel = service.FillViewModel(busId);

            return View("Index",viewModel);

        }

        [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 1)] // update evry sec
        public ActionResult GetBusView()
        {
            ViewModel viewModel = service.FillViewModel(0);
            
            return PartialView("_BusView", viewModel);

        }

        [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 1)] // update evry sec
        public ActionResult GetBusView(int id)
        {
            ViewModel viewModel = service.FillViewModel(id);

            return PartialView("_BusView", viewModel);

        }

        public ActionResult BusList()
        {
            var busList = service.GetBusList();
            return View(busList);
        }

        public ActionResult AddBus()
        {
            BusModel busModel=new BusModel();
            busModel.Beacons=new BusBeacon[3];
            busModel.Type = "";
            busModel.TypesList = service.GetBusTypesList();
            return View(busModel);
        }

        [HttpPost]
        public ActionResult AddBus(BusModel model)
        {
            var busList = service.GetBusList();
            return View("BusList",busList);
        }

        public ActionResult Edit(int id)
        {
            BusModel bus=service.GetBusList(id);
            return View(bus);
        }

        [HttpPost]
        public ActionResult Edit(BusModel model)
        {
            service.UpdateBus(model);
            var busList = service.GetBusList();
            return View("BusList",busList);
        }

        public ActionResult Details(int id)
        {
            BusModel bus = service.GetBusList(id);
            return View(bus);
        }

        public ActionResult Delete(int id)
        {
            service.DeleteBus(id);
            return View("BusList");
        }

        public ActionResult TypeList()
        {
            List<BusType> types = service.GetBusTypes();
            return View(types);
        }

        public ActionResult EditType(int id)
        {
            BusType type = service.GetBusType(id);
            return View(type);
        }

        [HttpPost]
        public ActionResult EditType(BusType model, string btnSubmit)
        {
            if(btnSubmit == "Add Sitting Area")
            {
                model.SittingAreas.Add(new SittingArea());
                return View(model);
            }
            else
            {
                service.UpdateBusType(model);
                return View("TypeList");
            }
        }

        public ActionResult CreateType()
        {
            throw new System.NotImplementedException();
        }
    }
}
