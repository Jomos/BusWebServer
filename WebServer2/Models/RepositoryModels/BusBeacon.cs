namespace WebServer2.Models.RepositoryModels
{
    public class BusBeacon
    {
        public int Id { get; set; }        
        public string BeaconUUID { get; set; }
        public int BeaconMajor { get; set; }
        public int BeaconMinor { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }  
    }
}