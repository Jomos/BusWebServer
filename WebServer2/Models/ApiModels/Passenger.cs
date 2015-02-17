namespace WebServer2.Models.ApiModels
{
    public class Passenger
    {
        public string Id { get; set; }
        public bool Sitting { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
    }
}