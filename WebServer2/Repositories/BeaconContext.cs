using System.Data.Entity;
using WebServer2.Models.RepositoryModels;

namespace WebServer2.Repositories
{
    public class BeaconContext : DbContext
    {
        public BeaconContext()
        : base("DefaultConnectionString"){}

        public DbSet<Bus> Busses { get; set; }
        public DbSet<BusType> BusTypes { get; set; }
    }
}
