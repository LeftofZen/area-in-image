using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaFinder
{
	public static class PointExtensions
	{
		private static double Distance(this System.Drawing.Point p1, System.Drawing.Point p2)
			=> Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));

		//private static PointComparer Sub()

	}
}
