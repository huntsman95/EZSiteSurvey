namespace EZSiteSurvey
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBoxFloorPlan = new PictureBox();
            btnLoadImage = new Button();
            btnClearPoints = new Button();
            btnGenerateHeatmap = new Button();
            listBoxPoints = new ListBox();
            comboBoxBSSID = new ComboBox();
            labelBSSID = new Label();
            labelPoints = new Label();
            statusLabel = new Label();
            btnExportCSV = new Button();
            btnExportHeatmap = new Button();
            btnSaveSurvey = new Button();
            btnLoadSurvey = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxFloorPlan).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxFloorPlan
            // 
            pictureBoxFloorPlan.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxFloorPlan.Location = new Point(12, 60);
            pictureBoxFloorPlan.Name = "pictureBoxFloorPlan";
            pictureBoxFloorPlan.Size = new Size(1496, 920);
            pictureBoxFloorPlan.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxFloorPlan.TabIndex = 0;
            pictureBoxFloorPlan.TabStop = false;
            pictureBoxFloorPlan.Click += pictureBoxFloorPlan_Click;
            pictureBoxFloorPlan.MouseClick += PictureBoxFloorPlan_MouseClick;
            // 
            // btnLoadImage
            // 
            btnLoadImage.Location = new Point(12, 12);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(150, 35);
            btnLoadImage.TabIndex = 1;
            btnLoadImage.Text = "Load Floor Plan";
            btnLoadImage.UseVisualStyleBackColor = true;
            btnLoadImage.Click += BtnLoadImage_Click;
            // 
            // btnClearPoints
            // 
            btnClearPoints.Location = new Point(168, 12);
            btnClearPoints.Name = "btnClearPoints";
            btnClearPoints.Size = new Size(120, 35);
            btnClearPoints.TabIndex = 2;
            btnClearPoints.Text = "Clear Points";
            btnClearPoints.UseVisualStyleBackColor = true;
            btnClearPoints.Click += BtnClearPoints_Click;
            // 
            // btnGenerateHeatmap
            // 
            btnGenerateHeatmap.Location = new Point(1700, 500);
            btnGenerateHeatmap.Name = "btnGenerateHeatmap";
            btnGenerateHeatmap.Size = new Size(359, 35);
            btnGenerateHeatmap.TabIndex = 3;
            btnGenerateHeatmap.Text = "Generate Heatmap";
            btnGenerateHeatmap.UseVisualStyleBackColor = true;
            btnGenerateHeatmap.Click += BtnGenerateHeatmap_Click;
            // 
            // listBoxPoints
            // 
            listBoxPoints.FormattingEnabled = true;
            listBoxPoints.Location = new Point(1538, 90);
            listBoxPoints.Name = "listBoxPoints";
            listBoxPoints.Size = new Size(521, 344);
            listBoxPoints.TabIndex = 4;
            // 
            // comboBoxBSSID
            // 
            comboBoxBSSID.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBSSID.FormattingEnabled = true;
            comboBoxBSSID.Location = new Point(1700, 460);
            comboBoxBSSID.Name = "comboBoxBSSID";
            comboBoxBSSID.Size = new Size(359, 28);
            comboBoxBSSID.TabIndex = 5;
            // 
            // labelBSSID
            // 
            labelBSSID.AutoSize = true;
            labelBSSID.Location = new Point(1538, 463);
            labelBSSID.Name = "labelBSSID";
            labelBSSID.Size = new Size(158, 20);
            labelBSSID.TabIndex = 6;
            labelBSSID.Text = "Select BSSID/Network:";
            // 
            // labelPoints
            // 
            labelPoints.AutoSize = true;
            labelPoints.Location = new Point(1538, 60);
            labelPoints.Name = "labelPoints";
            labelPoints.Size = new Size(119, 20);
            labelPoints.TabIndex = 7;
            labelPoints.Text = "Recorded Points:";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(579, 19);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(262, 20);
            statusLabel.TabIndex = 8;
            statusLabel.Text = "Click on floor plan to record WiFi data";
            // 
            // btnExportCSV
            // 
            btnExportCSV.Location = new Point(1700, 545);
            btnExportCSV.Name = "btnExportCSV";
            btnExportCSV.Size = new Size(359, 35);
            btnExportCSV.TabIndex = 9;
            btnExportCSV.Text = "Export to CSV";
            btnExportCSV.UseVisualStyleBackColor = true;
            btnExportCSV.Click += BtnExportCSV_Click;
            // 
            // btnExportHeatmap
            // 
            btnExportHeatmap.Location = new Point(1700, 590);
            btnExportHeatmap.Name = "btnExportHeatmap";
            btnExportHeatmap.Size = new Size(359, 35);
            btnExportHeatmap.TabIndex = 10;
            btnExportHeatmap.Text = "Export Heatmap Image";
            btnExportHeatmap.UseVisualStyleBackColor = true;
            btnExportHeatmap.Click += BtnExportHeatmap_Click;
            // 
            // btnSaveSurvey
            // 
            btnSaveSurvey.Location = new Point(294, 12);
            btnSaveSurvey.Name = "btnSaveSurvey";
            btnSaveSurvey.Size = new Size(120, 35);
            btnSaveSurvey.TabIndex = 11;
            btnSaveSurvey.Text = "Save Survey";
            btnSaveSurvey.UseVisualStyleBackColor = true;
            btnSaveSurvey.Click += BtnSaveSurvey_Click;
            // 
            // btnLoadSurvey
            // 
            btnLoadSurvey.Location = new Point(420, 12);
            btnLoadSurvey.Name = "btnLoadSurvey";
            btnLoadSurvey.Size = new Size(120, 35);
            btnLoadSurvey.TabIndex = 12;
            btnLoadSurvey.Text = "Load Survey";
            btnLoadSurvey.UseVisualStyleBackColor = true;
            btnLoadSurvey.Click += BtnLoadSurvey_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2071, 1003);
            Controls.Add(btnLoadSurvey);
            Controls.Add(btnSaveSurvey);
            Controls.Add(btnExportHeatmap);
            Controls.Add(btnExportCSV);
            Controls.Add(statusLabel);
            Controls.Add(labelPoints);
            Controls.Add(labelBSSID);
            Controls.Add(comboBoxBSSID);
            Controls.Add(listBoxPoints);
            Controls.Add(btnGenerateHeatmap);
            Controls.Add(btnClearPoints);
            Controls.Add(btnLoadImage);
            Controls.Add(pictureBoxFloorPlan);
            Name = "Form1";
            Text = "EZ Site Survey - WiFi Heatmap Tool";
            ((System.ComponentModel.ISupportInitialize)pictureBoxFloorPlan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxFloorPlan;
        private Button btnLoadImage;
        private Button btnClearPoints;
        private Button btnGenerateHeatmap;
        private ListBox listBoxPoints;
        private ComboBox comboBoxBSSID;
        private Label labelBSSID;
        private Label labelPoints;
        private Label statusLabel;
        private Button btnExportCSV;
        private Button btnExportHeatmap;
        private Button btnSaveSurvey;
        private Button btnLoadSurvey;
    }
}
