
namespace AreaFinder
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
			this.btnLoadImage = new System.Windows.Forms.Button();
			this.pbImage = new System.Windows.Forms.PictureBox();
			this.btnClickedColour = new System.Windows.Forms.Button();
			this.tbPixelCount = new System.Windows.Forms.TextBox();
			this.tbArea = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
			this.SuspendLayout();
			//
			// btnLoadImage
			//
			this.btnLoadImage.Location = new System.Drawing.Point(12, 12);
			this.btnLoadImage.Name = "btnLoadImage";
			this.btnLoadImage.Size = new System.Drawing.Size(75, 23);
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
			this.pbImage.Location = new System.Drawing.Point(12, 41);
			this.pbImage.Name = "pbImage";
			this.pbImage.Size = new System.Drawing.Size(834, 439);
			this.pbImage.TabIndex = 1;
			this.pbImage.TabStop = false;
			this.pbImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseClick);
			//
			// btnClickedColour
			//
			this.btnClickedColour.Enabled = false;
			this.btnClickedColour.Location = new System.Drawing.Point(94, 13);
			this.btnClickedColour.Name = "btnClickedColour";
			this.btnClickedColour.Size = new System.Drawing.Size(75, 23);
			this.btnClickedColour.TabIndex = 2;
			this.btnClickedColour.UseVisualStyleBackColor = true;
			//
			// tbPixelCount
			//
			this.tbPixelCount.Location = new System.Drawing.Point(175, 12);
			this.tbPixelCount.Name = "tbPixelCount";
			this.tbPixelCount.Size = new System.Drawing.Size(100, 23);
			this.tbPixelCount.TabIndex = 3;
			//
			// tbArea
			//
			this.tbArea.Location = new System.Drawing.Point(281, 12);
			this.tbArea.Name = "tbArea";
			this.tbArea.Size = new System.Drawing.Size(100, 23);
			this.tbArea.TabIndex = 4;
			//
			// Form1
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(858, 492);
			this.Controls.Add(this.tbArea);
			this.Controls.Add(this.tbPixelCount);
			this.Controls.Add(this.btnClickedColour);
			this.Controls.Add(this.pbImage);
			this.Controls.Add(this.btnLoadImage);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLoadImage;
		private System.Windows.Forms.PictureBox pbImage;
		private System.Windows.Forms.Button btnClickedColour;
		private System.Windows.Forms.TextBox tbPixelCount;
		private System.Windows.Forms.TextBox tbArea;
	}
}

