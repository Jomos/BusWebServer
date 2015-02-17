using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebGrease.Css.Extensions;
using WebServer2.Models.RepositoryModels;
using WebServer2.Models.ViewModels;

namespace WebServer2.Repositories

{
    public class BeaconPosition
    {
//        public List<BusBeacon> GetBeacon(int busNumber)
//        {
//            using(var db=new BeaconContext()){
//                db.Database.Log += Log;
                
//                return db.Busses.Single(x => x.Number == busNumber).Beacons.ToList();
//            }
//        }

//        public int GetBusNumber(string uuid,int major,int minor)
//        {
//            using (var db = new BeaconContext())
//            {
//                //String test = db.Database.Connection.ConnectionString;
//#if DEBUG
//                db.Database.Log += Log;
//#endif                
//                return db.Busses.Single(x => x.Beacons.Any(y => y.BeaconUUID == uuid && y.BeaconMajor == major && y.BeaconMinor == minor)).Number;                
//            }
//        }

//        public List<SittingArea> GetSeats(int busNumber)
//        {
//            using (var db = new BeaconContext())
//            {
//                db.Database.Log += Log;
//                int type= db.Busses.Single(x => x.Number == busNumber).Type;
//                return db.BusTypes.Single(x => x.Type == type).SittingAreas.ToList();
//            }
//        }

//        public string GetImageName(int busNumber)
//        {
//            using (var db = new BeaconContext())
//            {
//                db.Database.Log += Log;
//                int type = db.Busses.Single(x => x.Number == busNumber).Type;
//                return db.BusTypes.Single(y => y.Type == type).Image;
//            }
//        }

        public List<int> GetBusTypes()
        {
            using (var db = new BeaconContext())
            {
                return db.BusTypes.Select(x => x.Type).ToList();
            }
        } 

        private void Log(string s)
        {
            Debug.Write(s);
        }

        //Alternativ utan databas
        public List<BusBeacon> GetBeacon(int busNumber)
        {
            return new List<BusBeacon>{
                new BusBeacon{ BeaconUUID = "73676723-7400-0000-ffff-0000ffff0005", BeaconMajor = 2, BeaconMinor = 682, XPosition = 1, YPosition = 0},
                new BusBeacon{ BeaconUUID = "73676723-7400-0000-ffff-0000ffff0006", BeaconMajor = 2, BeaconMinor = 682, XPosition = 5, YPosition = 0},
                new BusBeacon{ BeaconUUID = "73676723-7400-0000-ffff-0000ffff0007", BeaconMajor = 2, BeaconMinor = 682, XPosition = 10, YPosition = 0}
            };

        }

        public int GetBusNumber(string uuid, int major, int minor)
        {
            return 1;
        }

        public List<SittingArea> GetSeats(int busNumber)
        {
            return new List<SittingArea>{
                    new SittingArea {XFrom = 0, XTo = 0.7, YFrom = 0, YTo = 2.55},
                    new SittingArea {XFrom = 0.8, XTo = 4.4, YFrom = 1.7, YTo = 2.55},
                    new SittingArea {XFrom = 6.1, XTo = 9.8, YFrom = 1.7, YTo = 2.55},
                    new SittingArea {XFrom = 1.8, XTo = 5, YFrom = 0, YTo = 1},
                    new SittingArea {XFrom = 6.1, XTo = 9.8, YFrom = 0, YTo = 1}
                };
        }

        public string GetImageName(int busNumber)
        {
            return "bus1.jpeg";
        }

        public List<Models.ViewModels.BusListItem> GetBusList()
        {
            using (var db = new BeaconContext())
            {
                List<BusListItem> busses =new List<BusListItem>();
                if (db.Busses != null)
                    db.Busses.ForEach(
                        x => busses.Add(new BusListItem {Number = x.Number, Type = x.Type}));
                return busses;
            }
            
        }
    }
}
