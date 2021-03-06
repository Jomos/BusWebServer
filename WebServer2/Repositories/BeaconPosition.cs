﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using WebGrease.Css.Extensions;
using WebServer2.Models.RepositoryModels;
using WebServer2.Models.ViewModels;

namespace WebServer2.Repositories

{
    public class BeaconPosition
    {
        public List<BusBeacon> GetBeacon(int busNumber)
        {
            using (var db = new BeaconContext())
            {
                db.Database.Log += Log;

                return db.Busses.Single(x => x.Number == busNumber).Beacons.ToList();
            }
        }

        public int GetBusNumber(string uuid, int major, int minor)
        {
            using (var db = new BeaconContext())
            {
                //String test = db.Database.Connection.ConnectionString;
#if DEBUG
                db.Database.Log += Log;
#endif
                return db.Busses.Single(x => x.Beacons.Any(y => y.BeaconUUID == uuid && y.BeaconMajor == major && y.BeaconMinor == minor)).Number;
            }
        }

        public List<SittingArea> GetSeats(int busId)
        {
            using (var db = new BeaconContext())
            {
                db.Database.Log += Log;
                List<SittingArea> sittingAreas =
                    db.BusTypes.Include(x => x.SittingAreas).Single(x => x.Id == busId).SittingAreas.ToList();
                return sittingAreas;
            }
        }

        public string GetImageName(int busId)
        {
            using (var db = new BeaconContext())
            {
                db.Database.Log += Log;
                string imageName = db.BusTypes.Single(x => x.Id == busId).Image;
                return imageName;
            }
        }

        public List<BusType> GetBusTypesList()
        {
            using (var db = new BeaconContext())
            {
                return db.BusTypes.ToList();
            }
        } 

        private void Log(string s)
        {
            Debug.Write(s);
        }

        //Alternativ utan databas
        //public List<BusBeacon> GetBeacon(int busNumber)
        //{
        //    return new List<BusBeacon>{
        //        new BusBeacon{ BeaconUUID = "73676723-7400-0000-ffff-0000ffff0005", BeaconMajor = 2, BeaconMinor = 682, XPosition = 1, YPosition = 0},
        //        new BusBeacon{ BeaconUUID = "73676723-7400-0000-ffff-0000ffff0006", BeaconMajor = 2, BeaconMinor = 682, XPosition = 5, YPosition = 0},
        //        new BusBeacon{ BeaconUUID = "73676723-7400-0000-ffff-0000ffff0007", BeaconMajor = 2, BeaconMinor = 682, XPosition = 10, YPosition = 0}
        //    };

        //}

        //public int GetBusNumber(string uuid, int major, int minor)
        //{
        //    return 1;
        //}

        //public List<SittingArea> GetSeats(int busNumber)
        //{
        //    return new List<SittingArea>{
        //            new SittingArea {XFrom = 0, XTo = 0.7, YFrom = 0, YTo = 2.55},
        //            new SittingArea {XFrom = 0.8, XTo = 4.4, YFrom = 1.7, YTo = 2.55},
        //            new SittingArea {XFrom = 6.1, XTo = 9.8, YFrom = 1.7, YTo = 2.55},
        //            new SittingArea {XFrom = 1.8, XTo = 5, YFrom = 0, YTo = 1},
        //            new SittingArea {XFrom = 6.1, XTo = 9.8, YFrom = 0, YTo = 1}
        //        };
        //}

        //public string GetImageName(int busNumber)
        //{
        //    return "bus1.jpeg";
        //}

        public List<Models.ViewModels.BusListItem> GetBusList()
        {
            using (var db = new BeaconContext())
            {
                List<BusListItem> busses =new List<BusListItem>();
                List<BusType> types = db.BusTypes.ToList();
                if (db.Busses != null)
                    db.Busses.ForEach(
                        x => busses.Add(new BusListItem {Number = x.Number, Type = types.Single(y=>y.Id==x.TypeId).Type,Id = x.Id}));
                return busses;
            }
        }

        public Models.RepositoryModels.Bus GetBus(int busId)
        {
            using (var db = new BeaconContext())
            {
                //var bus = db.Busses.Single(x => x.Number == busNumber);
                var bus = db.Busses.Include(x=>x.Beacons).Single(x => x.Id == busId);
                return bus;
            }
        }

        public void AddBus(Bus busModel)
        {
            using (var db = new BeaconContext())
            {
                db.Busses.Add(busModel);
                db.SaveChanges();
            }
        }

        public void DeleteBus(int busNumber)
        {
            using (var db = new BeaconContext())
            {
                var bus = db.Busses.Include(x => x.Beacons).Single(x => x.Number == busNumber);
                db.Busses.Remove(bus);
                db.SaveChanges();
            }
        }

        public void UpdateBus(BusModel model)
        {
            using (var db = new BeaconContext())
            {
                var bus = db.Busses.Include(x => x.Beacons).Single(x => x.Id == model.BusId);
                bus.Number = model.BusNumber;
                bus.TypeId = model.TypeId;
                for (int i = 0; i < 3; i++)
                {
                    bus.Beacons[i].BeaconUUID = model.Beacons[i].BeaconUUID;
                    bus.Beacons[i].BeaconMajor = model.Beacons[i].BeaconMajor;
                    bus.Beacons[i].BeaconMinor = model.Beacons[i].BeaconMinor;
                    bus.Beacons[i].XPosition = model.Beacons[i].XPosition;
                    bus.Beacons[i].YPosition = model.Beacons[i].YPosition;
                }
                db.SaveChanges();
            }
        }

        public List<BusType> GetBusTypes()
        {
            using (var db = new BeaconContext())
            {
                List<BusType> types = db.BusTypes.Include(x => x.SittingAreas).ToList();
                return types;
            }
        }

        internal BusType GetBusType(int id)
        {
            using (var db = new BeaconContext())
            {
                BusType type = db.BusTypes.Include(x => x.SittingAreas).Single(x=>x.Id==id);
                return type;
            }
        }

        internal void UpdateBusType(BusType model)
        {
            using(var db = new BeaconContext())
            {
                BusType type = db.BusTypes.Include(x => x.SittingAreas).Single(x => x.Id == model.Id);
                type.Image = model.Image;
                type.Type = model.Type;
                //Delete deleted SittingAreas
                List<SittingArea> deletedSittingAreas = new List<SittingArea>();
                foreach(var sittigArea in type.SittingAreas)
                {
                    if (!model.SittingAreas.Any(x => x.Id == sittigArea.Id)) { deletedSittingAreas.Add(sittigArea);}
                }
                for(int i = 0; i < deletedSittingAreas.Count; i++)
                {
                    type.SittingAreas.Remove(deletedSittingAreas[i]);
                }
                //Update and add SittingAreas
                foreach (var sittigArea in model.SittingAreas)
                {
                    if(sittigArea.Id != 0)
                    {
                        SittingArea sa = type.SittingAreas.Single(x => x.Id == sittigArea.Id);
                    }
                    else SittingArea sa = new SittingArea();
                    
                }
            }
        }
    }
}
