using System.Diagnostics;
using NetTopologySuite.Geometries;

namespace Geofences;

internal class Program
{
    private static void Main()
    {
        // 31.88791, 54.39709 Inside
        // 31.90030, 54.07934 Outside

        var pTime = new List<long>();
        var cTime = new List<long>();
        var lTime = new List<long>();
        for (int i = 0; i < 100; i++)
        {
            var geofenceChecker = new GeofenceChecker();

            var stopwatch = Stopwatch.StartNew();

            // Example 1: Polygon geofence
            var polygonCoordinates = new[]
            {
                new Coordinate(31.94240, 54.26491),
                new Coordinate(31.82040, 54.25787),
                new Coordinate(31.82040, 54.52955),
                new Coordinate(31.94240, 54.53579),
                new Coordinate(31.94240, 54.26491), // Closing the polygon
            };
            var polygonGeofence = geofenceChecker.CreatePolygon(polygonCoordinates);

            var point1 = geofenceChecker.CreatePoint(31.90030, 54.07934);
            var isInsidePolygon = geofenceChecker.IsPointInside(polygonGeofence, point1);

            // Stop stopwatch and print elapsed time
            stopwatch.Stop();
            pTime.Add(stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();

            // Example 2: Circular geofence
            var circularGeofence = geofenceChecker.CreateCircularGeofence(31.94240, 54.26491, 500);
            var point2 = geofenceChecker.CreatePoint(31.88791, 54.39709);
            var isInsideCircle = geofenceChecker.IsPointInside(circularGeofence, point2);

            stopwatch.Stop();
            cTime.Add(stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();

            // Example 2: Linear geofence
            var lineCoordinates = new[]
            {
                new Coordinate(48.8566, 2.3522), // Start point (e.g., Paris center)
                new Coordinate(48.8570, 2.3530), // Midpoint
                new Coordinate(48.8575, 2.3540)  // End point
            };

            var bufferWidthInMeters = 50.0;
            var lineGeofence = geofenceChecker.CreateBufferedLineGeofence(lineCoordinates, bufferWidthInMeters);

            var pointToCheck = geofenceChecker.CreatePoint(48.8568, 2.3525);
            var isInsideLineGeofence = geofenceChecker.IsPointInside(lineGeofence, pointToCheck);

            stopwatch.Stop();
            lTime.Add(stopwatch.ElapsedMilliseconds);
        }

        Console.WriteLine($"Point is inside polygon geofence: {pTime.Average()}");
        Console.WriteLine($"Point is inside circular geofence: {cTime.Average()}");
        Console.WriteLine($"Point is inside line geofence: {lTime.Average()}");
    }
}