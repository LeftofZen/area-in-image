using System.Collections.Generic;
using System.Drawing;

namespace AreaFinder
{
	// Why we need this?
	// https://stackoverflow.com/questions/46142734/why-is-hashsetpoint-so-much-slower-than-hashsetstring
	public class PointComparer : IEqualityComparer<Point>
	{
		public bool Equals(Point x, Point y)
			=> x.X == y.X && x.Y == y.Y;

		// Perfect hash for practical bitmaps, their width/height is never >= 65536
		public int GetHashCode(Point obj)
			=> (obj.Y << 16) ^ obj.X;
	}
}
