using System.Collections.Generic;
using System.Drawing;

namespace AreaFinder
{
	public struct Property
	{
		public Point Centroid;
		public int PixelCount;
		public string Name;

		public List<Point> Hull;

		public Property(Point middle, int pixelCount, string name = "")
		{
			Centroid = middle;
			PixelCount = pixelCount;
			Hull = new List<Point>();
			Name = name;
		}

		public string StringBinding
			=> ToString();

		public override string ToString()
			=> $"middle={Centroid} pixels={PixelCount}";
	}
}
