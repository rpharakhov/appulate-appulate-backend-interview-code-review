using Sample.Api.Model;
using System;

namespace Sample.Api.App
{
	public sealed class DistanceCalculator
	{
		const double PIx = 3.141592653589793;
		const double RADIUS = 6378.16;

		private double Radians(double x)
		{
			return x * PIx / 180;
		}

		public double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
		{
			double dlon = Radians(lon2 - lon1);
			double dlat = Radians(lat2 - lat1);

			double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(lat1)) * Math.Cos(Radians(lat2)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
			double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			return angle * RADIUS;
		}

		public double DistanceBetweenPlaces(Airport first, Airport second)
		{
			return DistanceBetweenPlaces(first.Longitude, first.Latitude, second.Longitude, second.Latitude);
		}
	}
}