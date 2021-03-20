﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AreaFinder
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			tbRGBThresholdDisplay.Text = tbRGBThreshold.Value.ToString();
			tbHSBThresholdDisplay.Text = (tbHSBThreshold.Value / hsbGuiMultiplier).ToString();
			tbPixelAreaRatioDisplay.Text = (tbPixelAreaRatio.Value / pixelRatioGuiMultiplier).ToString();
		}

		ImageBuffer originalImg;
		float hsbGuiMultiplier = 1000f;
		float pixelRatioGuiMultiplier = 100f;
		int lastPixelsCount = 0;

		private void btnLoadImage_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = @"C:\Users\bigba\source\repos\AreaFinder\AreaFinder\content";
				openFileDialog.Filter = "png files (*.png)|*.png|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
				openFileDialog.FilterIndex = 1;
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					originalImg = new ImageBuffer((Bitmap)Image.FromFile(openFileDialog.FileName));
					pbImage.Image = originalImg.GetImage();
				}
			}
		}

		private void pbImage_MouseClick(object sender, MouseEventArgs e)
		{
			if (!originalImg.Contains(e.X, e.Y))
				return;

			var clickedPointColour = originalImg.GetPixel(e.X, e.Y);
			var clickedPoint = new Point(e.X, e.Y);

			btnClickedColour.BackColor = clickedPointColour;

			lastPixelsCount = 0;

			var pixelFrontier = new HashSet<Point>();
			pixelFrontier.Add(clickedPoint);

			var debugImg = new ImageBuffer(originalImg.Width, originalImg.Height);
			var bounds = new Rectangle(0, 0, originalImg.Width, originalImg.Height);
			var rgbThreshold = tbRGBThreshold.Value;
			var hsbThreshold = tbHSBThreshold.Value / hsbGuiMultiplier;

			while (pixelFrontier.Count > 0)
			{
				var nextPixel = pixelFrontier.First();

				var rgbDistance = Distance(clickedPointColour, originalImg.GetPixel(nextPixel));
				var hsbDistance = DistanceInHSB(clickedPointColour, originalImg.GetPixel(nextPixel));
				if (rgbDistance < rgbThreshold || hsbDistance < hsbThreshold)
				{
					lastPixelsCount++;
					debugImg.SetPixel(nextPixel.X, nextPixel.Y, Color.Red);

					foreach (var n in GetNeighboursInBounds(nextPixel, bounds))
					{
						if (debugImg.IsEmpty(n))
						{
							if (n.X >= 0 && n.Y >= 0 && n.X < originalImg.Width && n.Y < originalImg.Height)
							{
								pixelFrontier.Add(n);
							}
						}
					}
				}

				pixelFrontier.Remove(nextPixel);
			}

			var pictureImage = new Bitmap(Width, Height);
			var g = Graphics.FromImage(pictureImage);
			g.DrawImage(originalImg.GetImage(), Point.Empty);
			g.DrawImage(debugImg.GetImage(), Point.Empty);
			pbImage.Image = pictureImage;

			tbPixelCount.Text = $"{lastPixelsCount}";
			UpdateAreaDisplay();
		}

		float GetAreaRatio() => tbPixelAreaRatio.Value / pixelRatioGuiMultiplier;

		void UpdateAreaDisplay()
		{
			tbArea.Text = $"{(int)(lastPixelsCount * GetAreaRatio())}";
		}

		static int Distance(Color a, Color b)
			=> ((a.R - b.R) * (a.R - b.R))
			 + ((a.G - b.G) * (a.G - b.G))
			 + ((a.B - b.B) * (a.B - b.B));

		static float DistanceInHSB(Color a, Color b)
			=> ((a.GetHue() / 360f - b.GetHue() / 360f) * (a.GetHue() / 360f - b.GetHue() / 360f))
			 + ((a.GetSaturation() - b.GetSaturation()) * (a.GetSaturation() - b.GetSaturation()))
			 + ((a.GetBrightness() - b.GetBrightness()) * (a.GetBrightness() - b.GetBrightness()));

		IEnumerable<Point> GetNeighboursInBounds(Point p, Rectangle bounds) => GetNeighbours(p).Where(p => bounds.Contains(p));

		IEnumerable<Point> GetNeighbours(Point p)
		{
			yield return new Point(p.X + 1, p.Y);
			yield return new Point(p.X - 1, p.Y);
			yield return new Point(p.X, p.Y + 1);
			yield return new Point(p.X, p.Y - 1);

			//for (var y = -1; y < 2; ++y)
			//{
			//	for (var x = -1; x < 2; ++x)
			//	{
			//		if (x != 0 || y != 0)
			//			yield return new Point(p.X + x, p.Y + y);
			//	}
			//}
		}

		private void tbThreshold_ValueChanged(object sender, EventArgs e)
		{
			tbRGBThresholdDisplay.Text = tbRGBThreshold.Value.ToString();
		}

		private void tbHSBThreshold_ValueChanged(object sender, EventArgs e)
		{
			tbHSBThresholdDisplay.Text = (tbHSBThreshold.Value / hsbGuiMultiplier).ToString();
		}

		private void tbPixelAreaRatio_ValueChanged(object sender, EventArgs e)
		{
			tbPixelAreaRatioDisplay.Text = (tbPixelAreaRatio.Value / pixelRatioGuiMultiplier).ToString();
			UpdateAreaDisplay();
		}
	}
}