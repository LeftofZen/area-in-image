using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Loyc.Collections;
using Loyc.Geometry;

namespace AreaFinder
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			DataSource = new UserSettings();

			// default good values
			//tbRGBThreshold.DataBindings.Add(new Binding("Value", bindingSource, "RGBThreshold"));
			userSettings.RGBThreshold = 1675;

			//tbHSBThreshold.DataBindings.Add("Value", bindingSource, "HSBThreshold");
			userSettings.HSBThreshold = (int)(0.01f * hsbGuiMultiplier);

			//tbPixelAreaRatio.DataBindings.Add("Value", bindingSource, "PixelRatio");
			userSettings.PixelRatio = (int)(0.1675f * pixelRatioGuiMultiplier);

			tbRGBThresholdDisplay.Text = userSettings.RGBThreshold.ToString();
			tbHSBThresholdDisplay.Text = (userSettings.HSBThreshold / hsbGuiMultiplier).ToString();
			tbPixelAreaRatioDisplay.Text = (userSettings.PixelRatio / pixelRatioGuiMultiplier).ToString();

		}

		public object DataSource
		{
			get => userSettings;
			set
			{
				if (value != null)
				{
					userSettings = (UserSettings)value;
					AddProperties();
					LoadImage(userSettings.FilenameOfImage);
					RefreshBinding();
					Refresh();
				}
			}
		}

		//public string DataMember
		//{
		//	get => userSettings;
		//	set => userSettings = value;
		//}

		public void RefreshBinding()
		{
			bindingSource.DataSource = null;
			bindingSource.DataSource = userSettings;
			lbPropertiesList.DataSource = userSettings.Properties;
		}

		private ImageBuffer originalImg;
		private ImageBuffer debugImg;
		private float hsbGuiMultiplier = 1000f;
		private float pixelRatioGuiMultiplier = 100f;
		private int lastPixelsCount = 0;

		private UserSettings userSettings;

		private void AddProperties()
		{
			userSettings.PropertyChanged += (a, b) =>
			{
				switch (b.PropertyName)
				{
					case "FilenameOfImage":
						LoadImage(userSettings.FilenameOfImage);
						break;
				}
			};
		}

		private void btnLoadImage_Click(object sender, EventArgs e)
		{
			using var openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = @"C:\Users\bigba\source\repos\AreaFinder\AreaFinder\content";
			openFileDialog.Filter = "png files (*.png)|*.png|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				userSettings.FilenameOfImage = openFileDialog.FileName;
			}
		}

		void LoadImage(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				return;
			}

			originalImg = new ImageBuffer((Bitmap)Image.FromFile(filename));
			debugImg = new ImageBuffer(originalImg.Width, originalImg.Height);
			pbImage.Image = originalImg.GetImage();
		}


		protected void tsmiSave_OnClick(object sender, EventArgs e)
		{
			using var saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = @"C:\Users\bigba\source\repos\AreaFinder\AreaFinder\content";
			saveFileDialog.Filter = "txt files (*.txt)|*.txt";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.RestoreDirectory = true;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				var filename = saveFileDialog.FileName;
				UserSettings.Save(userSettings, filename);
			}
		}

		protected void tsmiLoad_OnClick(object sender, EventArgs e)
		{
			using var loadFileDialog = new OpenFileDialog();
			loadFileDialog.InitialDirectory = @"C:\Users\bigba\source\repos\AreaFinder\AreaFinder\content";
			loadFileDialog.Filter = "txt files (*.txt)|*.txt";
			loadFileDialog.FilterIndex = 1;
			loadFileDialog.RestoreDirectory = true;

			if (loadFileDialog.ShowDialog() == DialogResult.OK)
			{
				DataSource = UserSettings.Load(loadFileDialog.FileName);
			}
		}

		private void pbImage_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				HandleLeftClick(e);
			}
			else if (e.Button == MouseButtons.Right)
			{
				HandleRightClick(e);
			}
		}

		void HandleLeftClick(MouseEventArgs e)
		{
			if (!originalImg.Contains(e.X, e.Y))
			{
				return;
			}

			var clickedPointColour = originalImg.GetPixel(e.X, e.Y);
			var clickedPoint = new Point(e.X, e.Y);

			btnClickedColour.BackColor = clickedPointColour;

			lastPixelsCount = 0;

			var pixelQueue = new Queue<Point>();
			var listOfFoundPixels = new List<Point>();
			pixelQueue.Enqueue(clickedPoint);

			var bounds = new Rectangle(0, 0, originalImg.Width, originalImg.Height);
			var rgbThreshold = tbRGBThreshold.Value;
			var hsbThreshold = tbHSBThreshold.Value / hsbGuiMultiplier;
			var newPropertyCentroid = Point.Empty;

			while (pixelQueue.Count > 0)
			{
				var nextPixel = pixelQueue.Dequeue();

				var rgbDistance = Distance(clickedPointColour, originalImg.GetPixel(nextPixel));
				var hsbDistance = DistanceInHSB(clickedPointColour, originalImg.GetPixel(nextPixel));
				if (rgbDistance < rgbThreshold || hsbDistance < hsbThreshold)
				{
					lastPixelsCount++;
					debugImg.SetPixel(nextPixel.X, nextPixel.Y, Color.Red);
					listOfFoundPixels.Add(nextPixel);
					newPropertyCentroid.Offset(nextPixel);

					foreach (var n in GetNeighboursInBounds(nextPixel, bounds))
					{
						if (debugImg.IsEmpty(n) && n.X >= 0 && n.Y >= 0 && n.X < originalImg.Width && n.Y < originalImg.Height)
						{
							pixelQueue.Enqueue(n);
							debugImg.SetPixel(n, Color.Blue);
						}
					}
				}
			}

			newPropertyCentroid = new Point(newPropertyCentroid.X / lastPixelsCount, newPropertyCentroid.Y / lastPixelsCount);
			if (lastPixelsCount <= 1) // probably bogus if 0-1 pixels
			{
				return;
			}

			var prop = new Property(newPropertyCentroid, lastPixelsCount);
			var listOfLoycPoints = listOfFoundPixels.Select(p => new Point<int>(p.X, p.Y));
			var hull = PointMath.ComputeConvexHull(listOfLoycPoints);

			foreach (var p in hull)
			{
				prop.Hull.Add(new Point(p.X, p.Y));
			}

			userSettings.Properties.Add(prop);
			//lbPropertiesList.Refresh();

			DrawAll();

			tbPixelCount.Text = $"{lastPixelsCount}";
			UpdateAreaDisplay();
		}

		void HandleRightClick(MouseEventArgs e)
		{

		}

		public void DrawAll()
		{
			var pictureImage = new Bitmap(Width, Height);
			var g = Graphics.FromImage(pictureImage);
			g.DrawImage(originalImg.GetImage(), Point.Empty);
			g.DrawImage(debugImg.GetImage(), Point.Empty);

			var guiImg = DrawGui();
			g.DrawImage(guiImg, Point.Empty);

			pbImage.Image = pictureImage;
		}

		private Bitmap DrawGui()
		{
			var guiImg = new Bitmap(originalImg.Width, originalImg.Height);
			var guiG = Graphics.FromImage(guiImg);
			var rectWidth = 8;
			foreach (var prop in userSettings.Properties)
			{
				// draw hull
				// doesn't work for battleaxe properties - need concave hull
				//guiG.FillPolygon(Brushes.Red, prop.Hull.ToArray());
				//guiG.DrawPolygon(new Pen(Brushes.DarkRed, 3f), prop.Hull.ToArray());

				guiG.FillRectangle(Brushes.White, prop.Centroid.X - rectWidth / 2, prop.Centroid.Y - rectWidth / 2, rectWidth, rectWidth);
				guiG.DrawString(PixelsToArea(prop.PixelCount).ToString(), new Font("Calibri", 12), Brushes.Black, prop.Centroid);
			}

			return guiImg;
		}

		private float GetAreaRatio()
			=> userSettings.PixelRatio / pixelRatioGuiMultiplier;

		private void UpdateAreaDisplay()
		{
			tbArea.Text = $"{PixelsToArea(lastPixelsCount)}";
		}

		public int PixelsToArea(int pixels)
		{
			return (int)(pixels * GetAreaRatio());
		}

		private static int Distance(Color a, Color b)
			=> ((a.R - b.R) * (a.R - b.R))
			 + ((a.G - b.G) * (a.G - b.G))
			 + ((a.B - b.B) * (a.B - b.B));

		private static float DistanceInHSB(Color a, Color b)
			=> (((a.GetHue() / 360f) - (b.GetHue() / 360f)) * ((a.GetHue() / 360f) - (b.GetHue() / 360f)))
			 + ((a.GetSaturation() - b.GetSaturation()) * (a.GetSaturation() - b.GetSaturation()))
			 + ((a.GetBrightness() - b.GetBrightness()) * (a.GetBrightness() - b.GetBrightness()));

		private static IEnumerable<Point> GetNeighboursInBounds(Point p, Rectangle bounds) => GetNeighbours(p).Where(p => bounds.Contains(p));

		private static IEnumerable<Point> GetNeighbours(Point p)
		{
			// using 4 neighbours instead of 8 means we find a boundary against a line of diagonal pixels where 8 will pass over it
			yield return new Point(p.X + 1, p.Y + 0);
			yield return new Point(p.X - 1, p.Y + 0);
			yield return new Point(p.X + 0, p.Y + 1);
			yield return new Point(p.X + 0, p.Y - 1);

			//for (var y = -1; y < 2; ++y)
			//{
			//	for (var x = -1; x < 2; ++x)
			//	{
			//		if (x != 0 || y != 0)
			//		{
			//			yield return new Point(p.X + x, p.Y + y);
			//		}
			//	}
			//}
		}

		private void tbRGBThreshold_ValueChanged(object sender, EventArgs e)
		{
			userSettings.RGBThreshold = tbRGBThreshold.Value;
			tbRGBThresholdDisplay.Text = userSettings.RGBThreshold.ToString();
		}

		private void tbHSBThreshold_ValueChanged(object sender, EventArgs e)
		{
			userSettings.HSBThreshold = tbHSBThreshold.Value;
			tbHSBThresholdDisplay.Text = (userSettings.HSBThreshold / hsbGuiMultiplier).ToString();
		}

		private void tbPixelAreaRatio_ValueChanged(object sender, EventArgs e)
		{
			userSettings.PixelRatio = tbPixelAreaRatio.Value;
			tbPixelAreaRatioDisplay.Text = (userSettings.PixelRatio / pixelRatioGuiMultiplier).ToString();
			UpdateAreaDisplay();
		}

		private void lbPropertiesList_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				var item = (Property)lbPropertiesList.SelectedItem;
				lbPropertiesList.DataSource = null;
				userSettings.Properties.Remove(item);
				lbPropertiesList.DataSource = userSettings.Properties;
				lbPropertiesList.Refresh();
				DrawAll();
			}
		}

		private void btnClearDebugImage_Click(object sender, EventArgs e)
		{
			debugImg.Clear();
			DrawAll();
		}

		private void btnDeleteAllProperties_Click(object sender, EventArgs e)
		{
			lbPropertiesList.DataSource = null;
			userSettings.Properties.Clear();
			lbPropertiesList.DataSource = userSettings.Properties;
			lbPropertiesList.Refresh();
			DrawAll();
		}
	}
}
