﻿namespace SoftverskaRealizacijaBackend.Helpers
{
    public class GeoUtils
    {
        private const double EarthRadiusKm = 6371;

        // Haversine formula to calculate distance between two lat/lon points
        public static double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);

            double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                       Math.Pow(Math.Sin(dLon / 2), 2) * Math.Cos(lat1) * Math.Cos(lat2);
            double c = 2 * Math.Asin(Math.Sqrt(a));
            return EarthRadiusKm * c;
        }

        private static double DegreesToRadians(double deg) => deg * (Math.PI / 180);
    }
}
