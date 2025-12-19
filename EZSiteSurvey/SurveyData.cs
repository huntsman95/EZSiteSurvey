namespace EZSiteSurvey
{
    public class SurveyData
    {
        public string? FloorPlanPath { get; set; }
        public List<WiFiScanPoint> ScanPoints { get; set; } = new List<WiFiScanPoint>();
        public DateTime SavedDate { get; set; }
        public string Version { get; set; } = "1.0";
    }
}
