namespace EZSiteSurvey
{
    public class WiFiScanPoint
    {
        public Point Location { get; set; }
        public List<WiFiNetwork> Networks { get; set; }
        public DateTime ScanTime { get; set; }

        public WiFiScanPoint(Point location, List<WiFiNetwork> networks)
        {
            Location = location;
            Networks = networks;
            ScanTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Point ({Location.X}, {Location.Y}) - {Networks.Count} networks - {ScanTime:HH:mm:ss}";
        }
    }
}
