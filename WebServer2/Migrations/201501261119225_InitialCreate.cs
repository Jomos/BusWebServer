using System.Data.Entity.Migrations;

namespace WebServer2.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BeaconPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UUID = c.String(),
                        Major = c.Int(nullable: false),
                        Minor = c.Int(nullable: false),
                        XPosition = c.Double(nullable: false),
                        YPosition = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusBeacons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusNumber = c.Int(nullable: false),
                        BeaconMajor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BusBeacons");
            DropTable("dbo.BeaconPositions");
        }
    }
}
