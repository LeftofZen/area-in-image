using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace AreaFinder
{
	public class ImageBuffer
	{
		public ImageBuffer(int width, int height)
		{
			buf = new Color[height, width];
		}

		public ImageBuffer(Bitmap img) : this(img.Width, img.Height)
		{
			//var img = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
			var rect = new Rectangle(0, 0, Width, Height);
			var imgData = img.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			for (var y = 0; y < Height; ++y)
			{
				for (var x = 0; x < Width; ++x)
				{
					SetPixel(x, y, imgData.GetPixel(x, y));
				}
			}

			img.UnlockBits(imgData);
		}

		private Color[,] buf;

		public void Clear()
			=> buf = new Color[Height, Width];

		public bool Contains(int X, int Y)
			=> X >= 0 && X < Width && Y >= 0 && Y < Height;

		public bool Contains(Point p)
			=> Contains(p.X, p.Y);

		public Color GetPixel(Point p)
			=> GetPixel(p.X, p.Y);
		public Color GetPixel(int X, int Y)
			=> buf[Y, X];

		public void SetPixel(Point p, Color c)
			=> SetPixel(p.X, p.Y, c);
		public void SetPixel(int X, int Y, Color c)
			=> buf[Y, X] = c;

		public bool IsEmpty(Point p)
			=> GetPixel(p).IsEmpty;

		public int Width
			=> buf.GetLength(1);
		public int Height
			=> buf.GetLength(0);

		public int NumberOfPixels
			=> Width * Height;

		public Point Middle
			=> new(Width / 2, Height / 2);

		public Image GetImage()
		{
			var img = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
			var rect = new Rectangle(0, 0, Width, Height);
			var imgData = img.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			for (var y = 0; y < Height; ++y)
			{
				for (var x = 0; x < Width; ++x)
				{
					imgData.SetPixel(x, y, GetPixel(x, y));
				}
			}

			img.UnlockBits(imgData);
			return img;
		}

		public void Save(int appendix = 0)
			=> Save(GetImage(), appendix);

		private static void Save(Image i, int appendix = 0)
		{
			Console.WriteLine("Saving");
			//i.Save(@"C:\Users\Benjamin.Sutas\source\repos\all-rgb\all-rgb\content\img.png", ImageFormat.Png);
			i.Save(@$"{baseFileName}\img{appendix}_.png", ImageFormat.Png);
		}

		private static string baseFileName = @"C:\Users\bigba\source\repos\all-rgb\all-rgb\content";
	}
}
