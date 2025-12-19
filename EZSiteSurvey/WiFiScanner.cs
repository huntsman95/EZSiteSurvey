using ManagedNativeWifi;

namespace EZSiteSurvey
{
    public class WiFiScanner
    {
        public async Task<List<WiFiNetwork>> ScanNetworks()
        {
            var networks = new List<WiFiNetwork>();

            try
            {
                // Trigger a fresh WiFi scan
                await NativeWifi.ScanNetworksAsync(timeout: TimeSpan.FromSeconds(5));

                // Small delay to ensure scan completes
                await Task.Delay(1000);

                // Enumerate all available network groups (SSIDs with their BSSIDs)
                var networkGroups = NativeWifi.EnumerateAvailableNetworkGroups();

                foreach (var group in networkGroups)
                {
                    string ssid = group.Ssid.ToString();
                    
                    // Skip hidden networks (empty SSID)
                    if (string.IsNullOrWhiteSpace(ssid))
                        continue;

                    // Each group contains one or more BSSs (access points with same SSID)
                    foreach (var bss in group.BssNetworks)
                    {
                        string bssid = bss.Bssid.ToString();
                        // ManagedNativeWifi uses LinkQuality (0-100), convert to RSSI-like value
                        // RSSI typically ranges from -100 to -30 dBm
                        int signalStrength = -100 + (int)(bss.LinkQuality / 2);

                        // Get channel, frequency, and band information
                        int channel = bss.Channel;
                        int frequency = bss.Frequency; // in MHz
                        string band = bss.Band.ToString(); // e.g., "TwoPointFourGHz" or "FiveGHz"

                        // Format band to be more readable
                        string formattedBand = FormatBand(band);

                        // Create a WiFiNetwork for each BSSID
                        networks.Add(new WiFiNetwork(ssid, bssid, signalStrength, channel, frequency, formattedBand));
                    }
                }

                // Sort by signal strength (strongest first)
                return networks.OrderByDescending(n => n.SignalStrength).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"WiFi scan failed: {ex.Message}", ex);
            }
        }

        private string FormatBand(string band)
        {
            // Convert enum name to readable format
            return band switch
            {
                "TwoPointFourGHz" => "2.4GHz",
                "FiveGHz" => "5GHz",
                "SixGHz" => "6GHz",
                _ => band
            };
        }
    }
}
