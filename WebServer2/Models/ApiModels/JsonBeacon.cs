namespace WebServer2.Models.ApiModels
{
    public class JsonBeacon
    {
        public string UUID { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public double Accuracy { get; set; }
        public string Proximity { get; set; }        
    }
}
