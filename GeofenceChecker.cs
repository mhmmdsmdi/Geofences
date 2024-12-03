using NetTopologySuite.Geometries;

namespace Geofences;

public class GeofenceChecker
{
    private readonly GeometryFactory _geometryFactory = new();
    private const double MetersPerDegree = 111_000;

    public Polygon CreatePolygon(Coordinate[] coordinates)
    {
        if (coordinates == null || coordinates.Length < 4)
        {
            throw new ArgumentException("A polygon requires at least 4 coordinates (including the closing coordinate).");
        }
        return _geometryFactory.CreatePolygon(coordinates);
    }

    public Point CreatePoint(double longitude, double latitude)
    {
        return _geometryFactory.CreatePoint(new Coordinate(longitude, latitude));
    }

    public Polygon CreateCircularGeofence(double longitude, double latitude, double radiusInMeters)
    {
        var radiusInDegrees = radiusInMeters / MetersPerDegree;

        var centerPoint = CreatePoint(longitude, latitude);
        return (Polygon)centerPoint.Buffer(radiusInDegrees);
    }

    public Polygon CreateBufferedLineGeofence(Coordinate[] lineCoordinates, double bufferWidthInMeters)
    {
        if (lineCoordinates == null || lineCoordinates.Length < 2)
        {
            throw new ArgumentException("A line requires at least two coordinates.");
        }

        var bufferWidthInDegrees = bufferWidthInMeters / MetersPerDegree;

        var line = _geometryFactory.CreateLineString(lineCoordinates);
        return (Polygon)line.Buffer(bufferWidthInDegrees);
    }

    public bool IsPointInside(Geometry geofence, Point point)
    {
        return geofence.Contains(point);
    }
}