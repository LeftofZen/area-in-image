
namespace AreaFinder
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.btnLoadImage = new System.Windows.Forms.Button();
			this.pbImage = new System.Windows.Forms.PictureBox();
			this.btnClickedColour = new System.Windows.Forms.Button();
			this.tbPixelCount = new System.Windows.Forms.TextBox();
			this.tbArea = new System.Windows.Forms.TextBox();
			this.tbRGBThreshold = new System.Windows.Forms.TrackBar();
			this.tbRGBThresholdDisplay = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tbHSBThresholdDisplay = new System.Windows.Forms.TextBox();
			this.tbHSBThreshold = new System.Windows.Forms.TrackBar();
			this.tbPixelAreaRatio = new System.Windows.Forms.TrackBar();
			this.tbPixelAreaRatioDisplay = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.lbPropertiesList = new System.Windows.Forms.ListBox();
			this.btnClearDebugImage = new System.Windows.Forms.Button();
			this.btnClearProperties = new System.Windows.Forms.Button();
			this.pnMain = new System.Windows.Forms.Panel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.msiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiLoad = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
			this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbRGBThreshold)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbHSBThreshold)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbPixelAreaRatio)).BeginInit();
			this.pnMain.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
			this.SuspendLayout();
			//
			// btnLoadImage
			//
			this.btnLoadImage.Location = new System.Drawing.Point(12, 12);
			this.btnLoadImage.Name = "btnLoadImage";
			this.btnLoadImage.Size = new System.Drawing.Size(200, 23);
			this.btnLoadImage.TabIndex = 0;
			this.btnLoadImage.Text = "Load Image";
			this.btnLoadImage.UseVisualStyleBackColor = true;
			this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
			//
			// pbImage
			//
			this.pbImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbImage.Location = new System.Drawing.Point(363, 165);
			this.pbImage.Name = "pbImage";
			this.pbImage.Size = new System.Drawing.Size(938, 557);
			this.pbImage.TabIndex = 1;
			this.pbImage.TabStop = false;
			this.pbImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseClick);
			//
			// btnClickedColour
			//
			this.btnClickedColour.Enabled = false;
			this.btnClickedColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClickedColour.Location = new System.Drawing.Point(112, 51);
			this.btnClickedColour.Name = "btnClickedColour";
			this.btnClickedColour.Size = new System.Drawing.Size(100, 23);
			this.btnClickedColour.TabIndex = 2;
			this.btnClickedColour.UseVisualStyleBackColor = true;
			//
			// tbPixelCount
			//
			this.tbPixelCount.Location = new System.Drawing.Point(112, 80);
			this.tbPixelCount.Name = "tbPixelCount";
			this.tbPixelCount.ReadOnly = true;
			this.tbPixelCount.Size = new System.Drawing.Size(100, 23);
			this.tbPixelCount.TabIndex = 3;
			//
			// tbArea
			//
			this.tbArea.Location = new System.Drawing.Point(112, 109);
			this.tbArea.Name = "tbArea";
			this.tbArea.ReadOnly = true;
			this.tbArea.Size = new System.Drawing.Size(100, 23);
			this.tbArea.TabIndex = 4;
			//
			// tbRGBThreshold
			//
			this.tbRGBThreshold.LargeChange = 50;
			this.tbRGBThreshold.Location = new System.Drawing.Point(386, 12);
			this.tbRGBThreshold.Maximum = 5000;
			this.tbRGBThreshold.Minimum = 1;
			this.tbRGBThreshold.Name = "tbRGBThreshold";
			this.tbRGBThreshold.Size = new System.Drawing.Size(460, 45);
			this.tbRGBThreshold.SmallChange = 5;
			this.tbRGBThreshold.TabIndex = 5;
			this.tbRGBThreshold.TickFrequency = 100;
			this.tbRGBThreshold.Value = 2000;
			this.tbRGBThreshold.ValueChanged += new System.EventHandler(this.tbRGBThreshold_ValueChanged);
			//
			// tbRGBThresholdDisplay
			//
			this.tbRGBThresholdDisplay.Location = new System.Drawing.Point(852, 12);
			this.tbRGBThresholdDisplay.Name = "tbRGBThresholdDisplay";
			this.tbRGBThresholdDisplay.ReadOnly = true;
			this.tbRGBThresholdDisplay.Size = new System.Drawing.Size(100, 23);
			this.tbRGBThresholdDisplay.TabIndex = 6;
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 15);
			this.label1.TabIndex = 7;
			this.label1.Text = "Pixel Colour";
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 15);
			this.label2.TabIndex = 8;
			this.label2.Text = "Pixel Count";
			//
			// label3
			//
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 15);
			this.label3.TabIndex = 9;
			this.label3.Text = "Estimated m²";
			//
			// label4
			//
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(245, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(135, 15);
			this.label4.TabIndex = 10;
			this.label4.Text = "Flood Fill RGB Threshold";
			//
			// label5
			//
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(245, 67);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(135, 15);
			this.label5.TabIndex = 13;
			this.label5.Text = "Flood Fill HSB Threshold";
			//
			// tbHSBThresholdDisplay
			//
			this.tbHSBThresholdDisplay.Location = new System.Drawing.Point(852, 63);
			this.tbHSBThresholdDisplay.Name = "tbHSBThresholdDisplay";
			this.tbHSBThresholdDisplay.ReadOnly = true;
			this.tbHSBThresholdDisplay.Size = new System.Drawing.Size(100, 23);
			this.tbHSBThresholdDisplay.TabIndex = 12;
			//
			// tbHSBThreshold
			//
			this.tbHSBThreshold.LargeChange = 10;
			this.tbHSBThreshold.Location = new System.Drawing.Point(386, 63);
			this.tbHSBThreshold.Maximum = 100;
			this.tbHSBThreshold.Minimum = 1;
			this.tbHSBThreshold.Name = "tbHSBThreshold";
			this.tbHSBThreshold.Size = new System.Drawing.Size(460, 45);
			this.tbHSBThreshold.TabIndex = 11;
			this.tbHSBThreshold.TickFrequency = 5;
			this.tbHSBThreshold.Value = 10;
			this.tbHSBThreshold.ValueChanged += new System.EventHandler(this.tbHSBThreshold_ValueChanged);
			//
			// tbPixelAreaRatio
			//
			this.tbPixelAreaRatio.LargeChange = 10;
			this.tbPixelAreaRatio.Location = new System.Drawing.Point(386, 114);
			this.tbPixelAreaRatio.Maximum = 100;
			this.tbPixelAreaRatio.Minimum = 1;
			this.tbPixelAreaRatio.Name = "tbPixelAreaRatio";
			this.tbPixelAreaRatio.Size = new System.Drawing.Size(460, 45);
			this.tbPixelAreaRatio.TabIndex = 14;
			this.tbPixelAreaRatio.TickFrequency = 5;
			this.tbPixelAreaRatio.Value = 50;
			this.tbPixelAreaRatio.ValueChanged += new System.EventHandler(this.tbPixelAreaRatio_ValueChanged);
			//
			// tbPixelAreaRatioDisplay
			//
			this.tbPixelAreaRatioDisplay.Location = new System.Drawing.Point(852, 114);
			this.tbPixelAreaRatioDisplay.Name = "tbPixelAreaRatioDisplay";
			this.tbPixelAreaRatioDisplay.ReadOnly = true;
			this.tbPixelAreaRatioDisplay.Size = new System.Drawing.Size(100, 23);
			this.tbPixelAreaRatioDisplay.TabIndex = 15;
			//
			// label6
			//
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(245, 117);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(94, 15);
			this.label6.TabIndex = 16;
			this.label6.Text = "Pixel to m² Ratio";
			//
			// lbPropertiesList
			//
			this.lbPropertiesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbPropertiesList.FormattingEnabled = true;
			this.lbPropertiesList.ItemHeight = 15;
			this.lbPropertiesList.Location = new System.Drawing.Point(12, 195);
			this.lbPropertiesList.Name = "lbPropertiesList";
			this.lbPropertiesList.Size = new System.Drawing.Size(345, 514);
			this.lbPropertiesList.TabIndex = 17;
			this.lbPropertiesList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbPropertiesList_KeyUp);
			//
			// btnClearDebugImage
			//
			this.btnClearDebugImage.Location = new System.Drawing.Point(194, 166);
			this.btnClearDebugImage.Name = "btnClearDebugImage";
			this.btnClearDebugImage.Size = new System.Drawing.Size(163, 23);
			this.btnClearDebugImage.TabIndex = 18;
			this.btnClearDebugImage.Text = "Clear Debug Rendering";
			this.btnClearDebugImage.UseVisualStyleBackColor = true;
			this.btnClearDebugImage.Click += new System.EventHandler(this.btnClearDebugImage_Click);
			//
			// btnClearProperties
			//
			this.btnClearProperties.Location = new System.Drawing.Point(12, 165);
			this.btnClearProperties.Name = "btnClearProperties";
			this.btnClearProperties.Size = new System.Drawing.Size(163, 23);
			this.btnClearProperties.TabIndex = 19;
			this.btnClearProperties.Text = "Delete All Properties";
			this.btnClearProperties.UseVisualStyleBackColor = true;
			this.btnClearProperties.Click += new System.EventHandler(this.btnDeleteAllProperties_Click);
			//
			// pnMain
			//
			this.pnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnMain.BackColor = System.Drawing.SystemColors.Control;
			this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnMain.Controls.Add(this.btnClearProperties);
			this.pnMain.Controls.Add(this.btnClearDebugImage);
			this.pnMain.Controls.Add(this.lbPropertiesList);
			this.pnMain.Controls.Add(this.label6);
			this.pnMain.Controls.Add(this.tbPixelAreaRatioDisplay);
			this.pnMain.Controls.Add(this.tbPixelAreaRatio);
			this.pnMain.Controls.Add(this.label5);
			this.pnMain.Controls.Add(this.tbHSBThresholdDisplay);
			this.pnMain.Controls.Add(this.tbHSBThreshold);
			this.pnMain.Controls.Add(this.label4);
			this.pnMain.Controls.Add(this.label3);
			this.pnMain.Controls.Add(this.label2);
			this.pnMain.Controls.Add(this.label1);
			this.pnMain.Controls.Add(this.tbRGBThresholdDisplay);
			this.pnMain.Controls.Add(this.tbRGBThreshold);
			this.pnMain.Controls.Add(this.tbArea);
			this.pnMain.Controls.Add(this.tbPixelCount);
			this.pnMain.Controls.Add(this.btnClickedColour);
			this.pnMain.Controls.Add(this.pbImage);
			this.pnMain.Controls.Add(this.btnLoadImage);
			this.pnMain.Location = new System.Drawing.Point(0, 27);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(1315, 740);
			this.pnMain.TabIndex = 20;
			//
			// menuStrip1
			//
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiFile});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1315, 24);
			this.menuStrip1.TabIndex = 21;
			this.menuStrip1.Text = "menuStrip1";
			//
			// msiFile
			//
			this.msiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoad,
            this.tsmiSave});
			this.msiFile.Name = "msiFile";
			this.msiFile.Size = new System.Drawing.Size(37, 20);
			this.msiFile.Text = "File";
			//
			// tsmiLoad
			//
			this.tsmiLoad.Name = "tsmiLoad";
			this.tsmiLoad.Size = new System.Drawing.Size(100, 22);
			this.tsmiLoad.Text = "Load";
			this.tsmiLoad.Click += new System.EventHandler(this.tsmiLoad_OnClick);
			//
			// tsmiSave
			//
			this.tsmiSave.Name = "tsmiSave";
			this.tsmiSave.Size = new System.Drawing.Size(100, 22);
			this.tsmiSave.Text = "Save";
			this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_OnClick);
			//
			// MainForm
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1315, 767);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Area Finder";
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbRGBThreshold)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbHSBThreshold)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbPixelAreaRatio)).EndInit();
			this.pnMain.ResumeLayout(false);
			this.pnMain.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLoadImage;
		private System.Windows.Forms.PictureBox pbImage;
		private System.Windows.Forms.Button btnClickedColour;
		private System.Windows.Forms.TextBox tbPixelCount;
		private System.Windows.Forms.TextBox tbArea;
		private System.Windows.Forms.TrackBar tbRGBThreshold;
		private System.Windows.Forms.TextBox tbRGBThresholdDisplay;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbHSBThresholdDisplay;
		private System.Windows.Forms.TrackBar tbHSBThreshold;
		private System.Windows.Forms.TrackBar tbPixelAreaRatio;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbPixelAreaRatioDisplay;
		private System.Windows.Forms.ListBox lbPropertiesList;
		private System.Windows.Forms.Button btnClearDebugImage;
		private System.Windows.Forms.Button btnClearProperties;
		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem msiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiLoad;
		private System.Windows.Forms.ToolStripMenuItem tsmiSave;
		private System.Windows.Forms.BindingSource bindingSource;
	}
}
