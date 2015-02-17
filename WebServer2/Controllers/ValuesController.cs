using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using WebServer2.Models.ApiModels;
using WebServer2.Models.ViewModels;
using WebServer2.Repositories;
using WebServer2.Services;

namespace WebServer2.Controllers
{
    public class ValuesController : ApiController
    {
        public static List<Bus> BusList { get; set; }
        public string Value = "";

        // GET: api/BusBeacons
        public string GetBusBeacons()
        {
            //string s = new JavaScriptSerializer().Serialize(BusList);
            string s = JsonConvert.SerializeObject(BusList);
            return s;
        }

        // POST: api/Values
        public void Post([FromBody]string value)
        {
            BeaconService service = new BeaconService();
            var jsonObject = new JavaScriptSerializer().Deserialize<JsonObject>(value);
            var beaconUUID = jsonObject.Beacons[0].UUID;
            var beaconMajor = jsonObject.Beacons[0].Major;
            var beaconMinor = jsonObject.Beacons[0].Minor;
            var repository = new BeaconPosition();
            int busNumber = repository.GetBusNumber(beaconUUID,beaconMajor,beaconMinor);
            List<WebServer2.Models.RepositoryModels.SittingArea> seats = repository.GetSeats(busNumber);
            if (BusList == null) BusList = new List<Bus>();
            var bus = BusList.SingleOrDefault(z => z.BusNumber == busNumber);
            if (bus == null)
            {
                bus = new Bus { BusNumber = busNumber };
                BusList.Add(bus);
            }
            if (jsonObject.Beacons.Count >= 3)
            {
                Point point;
                var beaconPositions = repository.GetBeacon(busNumber);
                point = service.Position(
                    beaconPositions[0].XPosition,
                    beaconPositions[1].XPosition,
                    beaconPositions[2].XPosition,
                    jsonObject.Beacons[0].Accuracy,
                    jsonObject.Beacons[1].Accuracy,
                    jsonObject.Beacons[2].Accuracy
                    );
                if (bus.Passengers == null) bus.Passengers = new List<Passenger>();
                if (bus.Time < jsonObject.DateTime)
                {
                    bus.Time = jsonObject.DateTime;
                    bus.Longitude = jsonObject.Longitude;
                    bus.Latitude = jsonObject.Latitude;
                }
                if (jsonObject.Type == "Exit")
                {
                    var passenger = bus.Passengers.Single(p => p.Id == jsonObject.Id);
                    bus.Passengers.Remove(passenger);
                }
                else if (bus.Passengers.All(p => p.Id != jsonObject.Id))
                {
                    bus.Passengers.Add(new Passenger { Id = jsonObject.Id, Sitting = service.IsSitting(point,seats),XPosition = point.x,YPosition = point.y});
                }
                else
                {
                    var passenger = bus.Passengers.Single(p => p.Id == jsonObject.Id);
                    passenger.Sitting = service.IsSitting(point,seats);
                    passenger.XPosition = point.x;
                    passenger.YPosition = point.y;
                }
            }
        }
    }
}