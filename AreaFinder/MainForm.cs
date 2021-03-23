using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Loyc.Collections;
using Loyc.Geometry;
using Loyc.Utilities;

namespace AreaFinder
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			Properties = new BindingList<Property>();
			lbPropertiesList.DataSource = Properties;

			// default good values
			tbRGBThreshold.Value = 1675;
			tbHSBThreshold.Value = (int)(0.01f * hsbGuiMultiplier);
			tbPixelAreaRatio.Value = (int)(0.1675f * pixelRatioGuiMultiplier);

			tbRGBThresholdDisplay.Text = tbRGBThreshold.Value.ToString();
			tbHSBThresholdDisplay.Text = (tbHSBThreshold.Value / hsbGuiMultiplier).ToString();
			tbPixelAreaRatioDisplay.Text = (tbPixelAreaRatio.Value / pixelRatioGuiMultiplier).ToString();

		}

		private ImageBuffer originalImg;
		private ImageBuffer debugImg;
		private float hsbGuiMultiplier = 1000f;
		private float pixelRatioGuiMultiplier = 100f;
		private int lastPixelsCount = 0;

		public BindingList<Property> Properties;

		public struct Property
		{
			public Point Centroid;
			public int PixelCount;
			public List<Point> Hull;

			public Property(Point middle, int pixelCount)
			{
				Centroid = middle;
				PixelCount = pixelCount;
				Hull = new List<Point>();
			}

			public string StringBinding => ToString();

			public override string ToString()
			{
				return $"middle={Centroid} pixels={PixelCount}";
			}
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
				originalImg = new ImageBuffer((Bitmap)Image.FromFile(openFileDialog.FileName));
				debugImg = new ImageBuffer(originalImg.Width, originalImg.Height);
				pbImage.Image = originalImg.GetImage();
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

			Properties.Add(prop);
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
			foreach (var prop in Properties)
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

		private float GetAreaRatio() => tbPixelAreaRatio.Value / pixelRatioGuiMultiplier;

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

		private void lbPropertiesList_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				var item = (Property)lbPropertiesList.SelectedItem;
				lbPropertiesList.DataSource = null;
				Properties.Remove(item);
				lbPropertiesList.DataSource = Properties;
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
			Properties.Clear();
			lbPropertiesList.DataSource = Properties;
			lbPropertiesList.Refresh();
			DrawAll();
		}

		//public static IListSource<Point> ComputeConvexHull(List<Point> points, bool sortInPlace = false)
		//{
		//	if (!sortInPlace)
		//		points = new List<Point>(points);
		//	points.Sort((a, b) =>
		//		a.X == b.X ? a.Y.CompareTo(b.Y) : a.X.CompareTo(b.X));

		//	// Importantly, DList provides O(1) insertion at beginning and end
		//	DList<Point> hull = new DList<Point>();
		//	int L = 0, U = 0; // size of lower and upper hulls

		//	// Builds a hull such that the output polygon starts at the leftmost point.
		//	for (int i = points.Count - 1; i >= 0; i--)
		//	{
		//		Point p = points[i], p1;

		//		// build lower hull (at end of output list)
		//		while (L >= 2 && (p1 = hull.Last()).Sub(hull[hull.Count - 2]).Cross(p.Sub(p1)) >= 0)
		//		{
		//			hull.RemoveAt(hull.Count - 1);
		//			L--;
		//		}
		//		hull.PushLast(p);
		//		L++;

		//		// build upper hull (at beginning of output list)
		//		while (U >= 2 && (p1 = hull.First()).Sub(hull[1]).Cross(p.Sub(p1)) <= 0)
		//		{
		//			hull.RemoveAt(0);
		//			U--;
		//		}
		//		if (U != 0) // when U=0, share the point added above
		//			hull.PushFirst(p);
		//		U++;
		//		//Debug.Assert(U + L == hull.Count + 1);
		//	}
		//	hull.RemoveAt(hull.Count - 1);
		//	return hull;
		//}
	}
}
