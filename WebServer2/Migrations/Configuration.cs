using System.Collections.Generic;
using System.Data.Entity.Migrations;
using WebServer2.Models.RepositoryModels;
using WebServer2.Repositories;

namespace WebServer2.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BeaconContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WebServer2.Repositories.BeaconContext";
        }

        protected override void Seed(BeaconContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Busses.AddOrUpdate(new Bus
            {
                Number = 1,
                TypeId = 1,
                Beacons = new List<BusBeacon>
            {
                new BusBeacon{ BeaconUUID = "73676723-7400-0000-ffff-0000ffff0005", BeaconMajor = 2, BeaconMinor = 682, XPosition = 1, YPosition = 0},
                new BusBeacon{ BeaconUUID = "73676723-7400-0000-ffff-0000ffff0006", BeaconMajor = 2, BeaconMinor = 682, XPosition = 5, YPosition = 0},
                new BusBeacon{ BeaconUUID = "73676723-7400-0000-ffff-0000ffff0007", BeaconMajor = 2, BeaconMinor = 682, XPosition = 10, YPosition = 0}
            }
            });

            context.BusTypes.AddOrUpdate(new BusType[]
            {
                new BusType
                {
                    Type = "Bus type 1",
                    Image = "bus1.jpeg",
                    SittingAreas = new List<SittingArea>
                {
                    new SittingArea {XFrom = 0, XTo = 0.7, YFrom = 0, YTo = 2.55},
                    new SittingArea {XFrom = 0.8, XTo = 4.4, YFrom = 1.7, YTo = 2.55},
                    new SittingArea {XFrom = 6.1, XTo = 9.8, YFrom = 1.7, YTo = 2.55},
                    new SittingArea {XFrom = 1.8, XTo = 5, YFrom = 0, YTo = 1},
                    new SittingArea {XFrom = 6.1, XTo = 9.8, YFrom = 0, YTo = 1}
                }
                },
                new BusType
                {
                    Type = "Bus type 2",
                    Image = "bus1.jpeg",
                    SittingAreas = new List<SittingArea>
                {
                    new SittingArea {XFrom = 0, XTo = 0.7, YFrom = 0, YTo = 2.55},
                    new SittingArea {XFrom = 0.8, XTo = 4.4, YFrom = 1.7, YTo = 2.55},
                    new SittingArea {XFrom = 6.1, XTo = 9.8, YFrom = 1.7, YTo = 2.55},
                    new SittingArea {XFrom = 1.8, XTo = 5, YFrom = 0, YTo = 1},
                    new SittingArea {XFrom = 6.1, XTo = 9.8, YFrom = 0, YTo = 1}
                }
                }
            });
        }
    }
}
