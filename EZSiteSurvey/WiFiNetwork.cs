namespace EZSiteSurvey
{
    public class WiFiNetwork
    {
        public string SSID { get; set; } = string.Empty;
        public string BSSID { get; set; } = string.Empty;
        public int SignalStrength { get; set; } // RSSI in dBm
        public int Channel { get; set; }
        public int Frequency { get; set; } // MHz
        public string Band { get; set; } = string.Empty; // 2.4GHz or 5GHz

        public WiFiNetwork(string ssid, string bssid, int signalStrength, int channel, int frequency, string band)
        {
            SSID = ssid;
            BSSID = bssid;
            SignalStrength = signalStrength;
            Channel = channel;
            Frequency = frequency;
            Band = band;
        }

        public override string ToString()
        {
            return $"{SSID} ({BSSID}) - {SignalStrength} dBm - {Band} Ch{Channel}";
        }
    }
}
