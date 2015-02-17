using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebServer2.Controllers;
using WebServer2.Models.ApiModels;
using WebServer2.Models.RepositoryModels;
using WebServer2.Models.ViewModels;
using WebServer2.Repositories;

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

        public IEnumerable<SelectListItem> GetBusTypes()
        {
            List<int> bustTypeList = repository.GetBusTypes();
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (int busType in bustTypeList)
            {
                selectList.Add(new SelectListItem{Text = busType.ToString(),Value = busType.ToString()});
            }
            return selectList;
        }

        public List<BusListItem> GetBusList()
        {
            return repository.GetBusList();
        }

        public void AddBus(AddBusModel busModel)
        {
            throw new NotImplementedException();
        }
    }
}