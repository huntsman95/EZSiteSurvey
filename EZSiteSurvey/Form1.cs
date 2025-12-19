using System.Text;

namespace EZSiteSurvey
{
    public partial class Form1 : Form
    {
        private Image? floorPlanImage;
        private List<WiFiScanPoint> scanPoints = new List<WiFiScanPoint>();
        private WiFiScanner wifiScanner = new WiFiScanner();

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLoadImage_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Title = "Select Floor Plan Image";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        floorPlanImage = Image.FromFile(ofd.FileName);
                        pictureBoxFloorPlan.Image = floorPlanImage;
                        statusLabel.Text = "Floor plan loaded. Click on the image to record WiFi data.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnClearPoints_Click(object? sender, EventArgs e)
        {
            scanPoints.Clear();
            listBoxPoints.Items.Clear();
            comboBoxBSSID.Items.Clear();
            statusLabel.Text = "All points cleared.";

            if (floorPlanImage != null)
            {
                pictureBoxFloorPlan.Image = floorPlanImage;
            }
        }

        private Point? ConvertPictureBoxToImageCoordinates(Point pictureBoxPoint)
        {
            if (floorPlanImage == null || pictureBoxFloorPlan.Image == null)
                return null;

            float pbWidth = pictureBoxFloorPlan.ClientSize.Width;
            float pbHeight = pictureBoxFloorPlan.ClientSize.Height;
            float imgWidth = floorPlanImage.Width;
            float imgHeight = floorPlanImage.Height;

            float imageAspect = imgWidth / imgHeight;
            float containerAspect = pbWidth / pbHeight;

            float zoomFactor;
            float offsetX = 0;
            float offsetY = 0;

            if (imageAspect > containerAspect)
            {
                zoomFactor = pbWidth / imgWidth;
                offsetY = (pbHeight - (imgHeight * zoomFactor)) / 2;
            }
            else
            {
                zoomFactor = pbHeight / imgHeight;
                offsetX = (pbWidth - (imgWidth * zoomFactor)) / 2;
            }

            float displayedWidth = imgWidth * zoomFactor;
            float displayedHeight = imgHeight * zoomFactor;

            if (pictureBoxPoint.X < offsetX || pictureBoxPoint.X > offsetX + displayedWidth ||
                pictureBoxPoint.Y < offsetY || pictureBoxPoint.Y > offsetY + displayedHeight)
            {
                return null;
            }

            int imageX = (int)((pictureBoxPoint.X - offsetX) / zoomFactor);
            int imageY = (int)((pictureBoxPoint.Y - offsetY) / zoomFactor);

            return new Point(imageX, imageY);
        }

        private async void PictureBoxFloorPlan_MouseClick(object? sender, MouseEventArgs e)
        {
            if (floorPlanImage == null)
            {
                MessageBox.Show("Please load a floor plan image first.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Point? imagePoint = ConvertPictureBoxToImageCoordinates(e.Location);
            
            if (imagePoint == null)
            {
                statusLabel.Text = "Please click within the image bounds.";
                return;
            }

            try
            {
                statusLabel.Text = "Scanning WiFi networks...";
                Application.DoEvents();

                List<WiFiNetwork> networks = await wifiScanner.ScanNetworks();

                if (networks.Count == 0)
                {
                    statusLabel.Text = "No WiFi networks found.";
                    MessageBox.Show("No WiFi networks detected. Make sure WiFi is enabled.", "No Networks", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                WiFiScanPoint scanPoint = new WiFiScanPoint(imagePoint.Value, networks);
                scanPoints.Add(scanPoint);

                listBoxPoints.Items.Add(scanPoint.ToString());

                UpdateBSSIDComboBox();

                DrawPointsOnFloorPlan();

                statusLabel.Text = $"Point recorded at ({imagePoint.Value.X}, {imagePoint.Value.Y}) - {networks.Count} networks found";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Error scanning WiFi";
                MessageBox.Show($"Error scanning WiFi: {ex.Message}", "Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGenerateHeatmap_Click(object? sender, EventArgs e)
        {
            if (floorPlanImage == null)
            {
                MessageBox.Show("Please load a floor plan image first.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (scanPoints.Count == 0)
            {
                MessageBox.Show("No scan points recorded. Click on the floor plan to record WiFi data.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxBSSID.SelectedItem == null)
            {
                MessageBox.Show("Please select a BSSID/Network from the dropdown.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string selectedBSSID = comboBoxBSSID.SelectedItem.ToString()!;
                statusLabel.Text = "Generating heatmap...";
                Application.DoEvents();

                Bitmap heatmap = GenerateHeatmap(selectedBSSID);
                pictureBoxFloorPlan.Image = heatmap;

                statusLabel.Text = $"Heatmap generated for {selectedBSSID}";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Error generating heatmap";
                MessageBox.Show($"Error generating heatmap: {ex.Message}", "Heatmap Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportCSV_Click(object? sender, EventArgs e)
        {
            if (scanPoints.Count == 0)
            {
                MessageBox.Show("No scan points to export. Click on the floor plan to record WiFi data first.", 
                    "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files|*.csv";
                sfd.Title = "Export WiFi Survey Data";
                sfd.FileName = $"WiFiSurvey_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportToCSV(sfd.FileName);
                        statusLabel.Text = $"Data exported to {Path.GetFileName(sfd.FileName)}";
                        MessageBox.Show($"Successfully exported {scanPoints.Count} scan points to CSV.", 
                            "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        statusLabel.Text = "Error exporting CSV";
                        MessageBox.Show($"Error exporting CSV: {ex.Message}", 
                            "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportToCSV(string filePath)
        {
            var csv = new StringBuilder();

            // CSV Header - now includes Channel, Frequency, and Band
            csv.AppendLine("Point_Number,X_Coordinate,Y_Coordinate,Scan_Time,SSID,BSSID,Signal_Strength_dBm,Channel,Frequency_MHz,Band");

            // Export each scan point with all its networks
            int pointNumber = 1;
            foreach (var scanPoint in scanPoints)
            {
                foreach (var network in scanPoint.Networks)
                {
                    // Escape any commas or quotes in SSID
                    string escapedSSID = EscapeCSVField(network.SSID);
                    
                    csv.AppendLine($"{pointNumber},{scanPoint.Location.X},{scanPoint.Location.Y}," +
                                 $"{scanPoint.ScanTime:yyyy-MM-dd HH:mm:ss},{escapedSSID}," +
                                 $"{network.BSSID},{network.SignalStrength},{network.Channel}," +
                                 $"{network.Frequency},{network.Band}");
                }
                pointNumber++;
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        private string EscapeCSVField(string field)
        {
            // If the field contains comma, quote, or newline, wrap it in quotes
            if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
            {
                // Escape any existing quotes by doubling them
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }

        private void UpdateBSSIDComboBox()
        {
            HashSet<string> allBSSIDs = new HashSet<string>();

            foreach (var point in scanPoints)
            {
                foreach (var network in point.Networks)
                {
                    // Include Band and Channel in the display text
                    string displayText = $"{network.SSID} ({network.BSSID}) - {network.Band} Ch{network.Channel}";
                    allBSSIDs.Add(displayText);
                }
            }

            comboBoxBSSID.Items.Clear();
            foreach (var bssid in allBSSIDs.OrderBy(b => b))
            {
                if (!comboBoxBSSID.Items.Contains(bssid))
                {
                    comboBoxBSSID.Items.Add(bssid);
                }
            }

            if (comboBoxBSSID.Items.Count > 0 && comboBoxBSSID.SelectedItem == null)
            {
                comboBoxBSSID.SelectedIndex = 0;
            }
        }

        private void DrawPointsOnFloorPlan()
        {
            if (floorPlanImage == null) return;

            Bitmap bmp = new Bitmap(floorPlanImage);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                foreach (var point in scanPoints)
                {
                    g.FillEllipse(Brushes.Red, point.Location.X - 5, point.Location.Y - 5, 10, 10);
                    g.DrawEllipse(Pens.White, point.Location.X - 5, point.Location.Y - 5, 10, 10);
                }
            }

            pictureBoxFloorPlan.Image = bmp;
        }

        private Bitmap GenerateHeatmap(string selectedNetwork)
        {
            Bitmap heatmap = new Bitmap(floorPlanImage!.Width, floorPlanImage.Height);

            string bssid = ExtractBSSID(selectedNetwork);

            List<(Point Location, int SignalStrength)> dataPoints = new List<(Point, int)>();

            foreach (var scanPoint in scanPoints)
            {
                var network = scanPoint.Networks.FirstOrDefault(n => n.BSSID == bssid);
                if (network != null)
                {
                    dataPoints.Add((scanPoint.Location, network.SignalStrength));
                }
            }

            if (dataPoints.Count == 0)
            {
                using (Graphics g = Graphics.FromImage(heatmap))
                {
                    g.DrawImage(floorPlanImage, 0, 0);
                }
                return heatmap;
            }

            using (Graphics g = Graphics.FromImage(heatmap))
            {
                g.DrawImage(floorPlanImage, 0, 0);

                // Use absolute RSSI ranges instead of relative min/max
                // Typical WiFi RSSI ranges from -100 dBm (weakest) to -30 dBm (strongest)
                const int absoluteMinRSSI = -100;
                const int absoluteMaxRSSI = -30;

                for (int y = 0; y < heatmap.Height; y++)
                {
                    for (int x = 0; x < heatmap.Width; x++)
                    {
                        double totalWeight = 0;
                        double weightedSignal = 0;

                        foreach (var dataPoint in dataPoints)
                        {
                            double distance = Math.Sqrt(Math.Pow(x - dataPoint.Location.X, 2) + Math.Pow(y - dataPoint.Location.Y, 2));

                            if (distance < 1) distance = 1;

                            double weight = 1.0 / Math.Pow(distance, 2);
                            totalWeight += weight;
                            weightedSignal += weight * dataPoint.SignalStrength;
                        }

                        if (totalWeight > 0)
                        {
                            double interpolatedSignal = weightedSignal / totalWeight;

                            // Normalize based on absolute RSSI range
                            double normalized = (interpolatedSignal - absoluteMinRSSI) / (absoluteMaxRSSI - absoluteMinRSSI);
                            normalized = Math.Max(0, Math.Min(1, normalized));

                            Color heatColor = GetHeatmapColor(normalized);

                            using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, heatColor)))
                            {
                                g.FillRectangle(brush, x, y, 1, 1);
                            }
                        }
                    }
                }

                foreach (var dataPoint in dataPoints)
                {
                    g.FillEllipse(Brushes.White, dataPoint.Location.X - 4, dataPoint.Location.Y - 4, 8, 8);
                    g.DrawEllipse(Pens.Black, dataPoint.Location.X - 4, dataPoint.Location.Y - 4, 8, 8);
                }
            }

            return heatmap;
        }

        private string ExtractBSSID(string selectedNetwork)
        {
            // Format is now: "SSID (BSSID) - Band ChChannel"
            // We need to extract just the BSSID part between parentheses
            int startIndex = selectedNetwork.IndexOf('(') + 1;
            int endIndex = selectedNetwork.IndexOf(')');
            
            if (startIndex > 0 && endIndex > startIndex)
            {
                return selectedNetwork.Substring(startIndex, endIndex - startIndex);
            }
            
            // Fallback if format doesn't match
            return selectedNetwork;
        }

        private Color GetHeatmapColor(double normalized)
        {
            // 12-color gradient based on absolute signal strength
            // Each color represents approximately 5.83 dBm range (-100 to -30 dBm = 70 dBm range / 12 colors)
            
            if (normalized < 0.0833)      // -100 to -94.2 dBm
                return Color.FromArgb(139, 0, 0);        // Dark Red
            else if (normalized < 0.1667) // -94.2 to -88.3 dBm
                return Color.FromArgb(220, 20, 60);      // Crimson
            else if (normalized < 0.25)   // -88.3 to -82.5 dBm
                return Color.FromArgb(255, 0, 0);        // Red
            else if (normalized < 0.3333) // -82.5 to -76.7 dBm
                return Color.FromArgb(255, 69, 0);       // Orange Red
            else if (normalized < 0.4167) // -76.7 to -70.8 dBm
                return Color.FromArgb(255, 140, 0);      // Dark Orange
            else if (normalized < 0.5)    // -70.8 to -65 dBm
                return Color.FromArgb(255, 165, 0);      // Orange
            else if (normalized < 0.5833) // -65 to -59.2 dBm
                return Color.FromArgb(255, 215, 0);      // Gold
            else if (normalized < 0.6667) // -59.2 to -53.3 dBm
                return Color.FromArgb(255, 255, 0);      // Yellow
            else if (normalized < 0.75)   // -53.3 to -47.5 dBm
                return Color.FromArgb(173, 255, 47);     // Yellow Green
            else if (normalized < 0.8333) // -47.5 to -41.7 dBm
                return Color.FromArgb(124, 252, 0);      // Lawn Green
            else if (normalized < 0.9167) // -41.7 to -35.8 dBm
                return Color.FromArgb(0, 255, 0);        // Green
            else                          // -35.8 to -30 dBm
                return Color.FromArgb(0, 128, 0);        // Dark Green (Excellent)
        }

        private void pictureBoxFloorPlan_Click(object sender, EventArgs e)
        {

        }
    }
}
