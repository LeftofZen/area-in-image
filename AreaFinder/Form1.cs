using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AreaFinder
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		Bitmap originalImage;
		Bitmap pictureImage;

		private void btnLoadImage_Click(object sender, EventArgs e)
		{
			var filename = @"C:\Users\bigba\source\repos\AreaFinder\AreaFinder\content\googong.png";
			//var filename = @"C:\Users\bigba\source\repos\AreaFinder\AreaFinder\content\test.png";
			originalImage = (Bitmap)Image.FromFile(filename);
			pbImage.Image = originalImage;
		}

		private void pbImage_MouseClick(object sender, MouseEventArgs e)
		{
			var colour = originalImage.GetPixel(e.X, e.Y);
			var neighbours = GetNeighbours(new Point(e.X, e.Y));

			var debugBmp = new Bitmap(originalImage);
			var pictureImage = new Bitmap(originalImage);

			// only for visualisation
			btnClickedColour.BackColor = colour;

			var pixelsFound = new HashSet<Point>();
			var pixelsLookedAt = new HashSet<Point>();
			// add original point
			pixelsFound.Add(new Point(e.X, e.Y));
			pixelsLookedAt.Add(new Point(e.X, e.Y));

			var pixelFrontier = new HashSet<Point>();
			foreach (var n in neighbours)
			{
				pixelFrontier.Add(n);
				pixelsLookedAt.Add(n);
			}

			var threshold = 1000;
			// check neighbour colours
			while(pixelFrontier.Count > 0)
			{
				var nextPixel = pixelFrontier.First();

				var dist = Distance(colour, originalImage.GetPixel(nextPixel.X, nextPixel.Y));
				if (dist < threshold)
				{
					// add pixel to found
					pixelsFound.Add(nextPixel);
					debugBmp.SetPixel(nextPixel.X, nextPixel.Y, Color.Red);

					// add neighbours to frontier
					var nn = GetNeighbours(nextPixel);
					foreach (var n in nn)
					{
						if (!pixelsFound.Contains(n) && pixelsLookedAt.Add(n))
						{
							if (n.X >= 0 && n.Y >= 0 && n.X < originalImage.Width && n.Y < originalImage.Height)
							{
								pixelFrontier.Add(n);
							}
							//debugBmp.SetPixel(n.X, n.Y, Color.Blue);
						}
					}
				}

				pixelFrontier.Remove(nextPixel);

			}

			var g = Graphics.FromImage(pictureImage);
			g.Clear(Color.White);
			g.DrawImage(originalImage, Point.Empty);
			g.DrawImage(debugBmp, Point.Empty);
			pbImage.Image = pictureImage;

			var ratio = 1230f / 2450f;
			var count = pixelsFound.Count();
			tbPixelCount.Text = $"{count}";
			tbArea.Text = $"{(int)(count * ratio)}";

			//Console.WriteLine("");
		}

		static int Distance(Color a, Color b)
			=> ((a.R - b.R) * (a.R - b.R))
			 + ((a.G - b.G) * (a.G - b.G))
			 + ((a.B - b.B) * (a.B - b.B));

		IEnumerable<Point> GetNeighbours(Point p)
		{
			for (int y = -1; y < 2; ++y)
			{
				for (int x = -1; x < 2; ++x)
				{
					if (x == 0 && y == 0)
						continue;
					yield return new Point(p.X + x, p.Y + y);
				}
			}
		}
	}
}
