using HotelSearch.Domain.Models;

namespace HotelSearch.Common.Helpers;

public static class GeoDistanceHelper
{
    private const double EarthRadiusKm = 6371;

    public static double CalculateDistanceKm(GeoLocation from, GeoLocation to)
    {
        var dLat = ToRad(to.Latitude - from.Latitude);
        var dLon = ToRad(to.Longitude - from.Longitude);

        var lat1 = ToRad(from.Latitude);
        var lat2 = ToRad(to.Latitude);

        var a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(lat1) * Math.Cos(lat2) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return EarthRadiusKm * c;
    }

    private static double ToRad(double d) => d * Math.PI / 180;
}
