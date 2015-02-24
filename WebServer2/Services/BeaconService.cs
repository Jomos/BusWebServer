using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebServer2.Controllers;
using WebServer2.Models.ApiModels;
using WebServer2.Models.RepositoryModels;
using WebServer2.Models.ViewModels;
using WebServer2.Repositories;
using Bus = WebServer2.Models.RepositoryModels.Bus;

namespace WebServer2.Services
{
    public struct Point
    {
        public double x;
        public double y;
    }

    public class BeaconService
    {
        private  BeaconPosition repository = new BeaconPosition();

        public Point Position(double x1, double x2, double x3, double r1, double r2, double r3)
        {
            Point p;
            BeaconService s=new BeaconService();
            double[] a = { x2 - x1, x3 - x2, x3 - x1 };
            double[] b = { r2, r3, r3 };
            double[] c = { r1, r2, r1 };
            Point[] pk=new Point[3];
            pk[0] = s.Left(a[0], b[0], c[0]);
            pk[1]=s.Right(a[1], b[1], c[1]);
            pk[2]=s.Left(a[2], b[2], c[2]);
            int j = 0;
            double sum = 0;
            foreach (var d in pk)
            {
                if (!Double.IsNaN(d.y))
                {
                    sum += d.y;
                    j++;
                }
            }
            p.y = sum / j;
            sum = 0;
            j = 0;
            if (r1 > (x3 - x1))
            {
                for (int i = 0; i < 3; i++)
                {
                    if (!Double.IsNaN(pk[i].x))
                    {
                        if (i == 0 || i == 2)
                        {
                            sum += pk[i].x + x1;
                        }
                        else
                        {
                            sum += pk[i].x + x3;
                        }
                        j++;
                    }
                }
                p.x = sum / j;
            }
            else if (r3 > (x3 - x1))
            {
                for (int i = 0; i < 3; i++)
                {
                    if (!Double.IsNaN(pk[i].x))
                    {
                        if (i == 0 || i == 2)
                        {
                            sum += x1 - pk[i].x;
                        }
                        else
                        {
                            sum += x3 - pk[i].x;
                        }
                        j++;
                    }
                }
                p.x = sum / j;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    if (!Double.IsNaN(pk[i].x))
                    {
                        if (i == 0 || i == 2)
                        {
                            sum += pk[i].x + x1;
                        }
                        else
                        {
                            sum += x3 - pk[i].x;
                        }
                        j++;
                    }
                }
                p.x = sum / j;
            }
            return p;
        }

        public Point Left(double a, double b, double c)
        {
            Point p;
            p.y = c * Math.Sin(Math.Acos((c * c + a * a - b * b) / (2 * c * a)));//vinkel B
            p.x = Math.Sqrt(c * c - p.y * p.y);
            return p;
        }

        public Point Right(double a, double b, double c)
        {
            Point p;
            p.y = c * Math.Sin(Math.Acos((c * c + a * a - b * b) / (2 * c * a)));//vinkel B
            p.x = Math.Sqrt(b * b - p.y * p.y);
            return p;
        }

        public bool IsSitting(Point p,List<SittingArea> seats )
        {
            bool sitting = false;
            foreach (var seat in seats)
            {
                if (!sitting && ((p.x < seat.XTo && p.x > seat.XFrom) && (p.y > seat.YFrom && p.y < seat.YTo))) sitting = true;
            }
            return sitting;
        }

        public Models.ViewModels.ViewModel FillViewModel(int busNumber)
        {
            ViewModel viewModel = new ViewModel();
            viewModel.Persons = new List<Person>();
            
            if (ValuesController.BusList != null)
            {
                if (busNumber == 0) busNumber = ValuesController.BusList[0].BusNumber;
                var repository = new BeaconPosition();
                viewModel.BusImage = repository.GetImageName(busNumber);
                var actualBus = ValuesController.BusList.Single(x => x.BusNumber == busNumber);
                var passengers = actualBus.Passengers;
                viewModel.BusNumber = actualBus.BusNumber.ToString();
                viewModel.NumberOfPassengers = passengers.Count;
                viewModel.SittingPassengers = passengers.Count(x => x.Sitting == true);
                viewModel.StandingPassangers = passengers.Count(x => x.Sitting == false);
                
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var bus in ValuesController.BusList)
                {
                    items.Add(new SelectListItem { Text = "Bus " + bus.BusNumber, Value = bus.BusNumber.ToString() });
                }
                viewModel.BusNumbers = items.OrderBy(x => x.Text);
               

                foreach (var passenger in passengers)
                {
                    viewModel.Persons.Add(new Person { XPosition = (int)(passenger.XPosition * 33.3 + 10), YPosition = (int)(90 - passenger.YPosition * 29.4) });
                }
            }
            return viewModel;
        }

        public List<TypeListItem> GetBusTypesList()
        {
            List<BusType> bustTypeList = repository.GetBusTypesList();
            List<TypeListItem> selectList = new List<TypeListItem>();
            foreach (BusType busType in bustTypeList)
            {
                selectList.Add(new TypeListItem{Text = busType.Type,Value = busType.Id});
            }
            return selectList;
        }

        public List<BusListItem> GetBusList()
        {
            return repository.GetBusList();
        }

        public void AddBus(BusModel busModel)
        {
            Bus bus = new Bus {Number = busModel.BusNumber, TypeId = busModel.TypeId, Beacons = new List<BusBeacon>(),Id = busModel.BusId};
            foreach (var beacon in busModel.Beacons)
            {
                bus.Beacons.Add(beacon);
            }
            repository.AddBus(bus);
        }

        internal BusModel GetBusList(int busId)
        {
            Bus repositoryBus = repository.GetBus(busId);
            List<BusType> repositoryTypeList = repository.GetBusTypesList();
            BusModel bus = new BusModel
            {
                BusId = busId,
                TypeId = repositoryBus.TypeId,
                BusNumber = repositoryBus.Number,
                Type = repositoryTypeList.Single(x=>x.Id==repositoryBus.TypeId).Type,
                Beacons = new BusBeacon[3],
                TypesList = new List<TypeListItem>()
            };
            for (int i = 0; i < repositoryBus.Beacons.Count; i++)
            {
                bus.Beacons[i] = repositoryBus.Beacons[i];
            }
            List<BusType> bustTypeList = repository.GetBusTypesList();
            foreach (BusType busType in bustTypeList)
            {
                bus.TypesList.Add(new TypeListItem { Text = busType.Type, Value = busType.Id });
            }
            return bus;
        }

        internal void DeleteBus(int busNumber)
        {
            repository.DeleteBus(busNumber);
        }

        internal void UpdateBus(BusModel model)
        {
            repository.UpdateBus(model);
        }

        internal List<BusType> GetBusTypes()
        {
            return repository.GetBusTypes();
        }

        internal BusType GetBusType(int id)
        {
            return repository.GetBusType(id);
        }

        internal void UpdateBusType(BusType model)
        {
            List<SittingArea> sittingAreasNulList=new List<SittingArea>();
            for(int i=0;i<model.SittingAreas.Count;i++)
            {
                if(model.SittingAreas[i].XFrom == 0 && model.SittingAreas[i].XTo == 0 && model.SittingAreas[i].YFrom == 0 && model.SittingAreas[i].YTo == 0)
                {
                    sittingAreasNulList.Add(model.SittingAreas[i]);
                }
            }
            if(sittingAreasNulList.Count>0)
            {
                for(int i = 0; i <sittingAreasNulList.Count; i++)
                {
                    model.SittingAreas.Remove(sittingAreasNulList[i]);
                }
            }
            repository.UpdateBusType(model);
        }
    }
}