﻿using System.Diagnostics;
using NetTopologySuite.Geometries;

namespace Geofences;

internal class Program
{
    private static void Main()
    {
        // 31.88791, 54.39709 Inside
        // 31.90030, 54.07934 Outside

        var geofenceChecker = new GeofenceChecker();

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

        // Example 2: Circular geofence
        var circularGeofence = geofenceChecker.CreateCircularGeofence(31.94240, 54.26491, 500);
        var point2 = geofenceChecker.CreatePoint(31.88791, 54.39709);
        var isInsideCircle = geofenceChecker.IsPointInside(circularGeofence, point2);

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

        // Parsing
        var polygonString = "POLYGON=(48.85 2.347, 48.85 2.34, 48.34 2.324, 48.231 2.231, 48.123 2.2313)";
        var routeString = "ROUTE=(31.87 54.35, 31.87 54.34)";
        var circularString = "CIRCULAR=(31.87 54.35 200)";

        var parsedPolygonGeofence = geofenceChecker.ParseGeofence(polygonString);
        var parsedRouteGeofence = geofenceChecker.ParseGeofence(routeString);
        var parsedCircularGeofence = geofenceChecker.ParseGeofence(circularString);

        var testPoint = geofenceChecker.CreatePoint(2.345, 48.84);
        Console.WriteLine($"Point inside polygon: {geofenceChecker.IsPointInside(parsedPolygonGeofence, testPoint)}");
        Console.WriteLine($"Point inside route: {geofenceChecker.IsPointInside(parsedRouteGeofence, testPoint)}");
        Console.WriteLine($"Point inside circular geofence: {geofenceChecker.IsPointInside(parsedCircularGeofence, testPoint)}");
    }
}