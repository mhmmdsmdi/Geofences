# Geofence Checker

A C# library for creating and managing geofences using the [NetTopologySuite](https://nettopologysuite.github.io/NetTopologySuite/). This library allows for the creation of polygons, circular geofences, and buffered line geofences, as well as checking whether a given point lies inside a geofence.

---

## Features

- **Create Polygons**: Build custom polygon geofences using an array of coordinates.
- **Create Circular Geofences**: Define geofences with a circular shape around a given center point.
- **Create Buffered Line Geofences**: Generate geofences by buffering a line segment.
- **Point-in-Geofence Check**: Verify if a point is inside a given geofence.

---

## Installation

Ensure you have the `NetTopologySuite` NuGet package installed:

```bash
dotnet add package NetTopologySuite
```

---

## Usage

### Creating a Geofence
```csharp
using Geofences;
using NetTopologySuite.Geometries;

// Initialize the GeofenceChecker
var geofenceChecker = new GeofenceChecker();

// Create a polygon geofence
var coordinates = new[]
{
    new Coordinate(2.3473018, 48.8563949),
    new Coordinate(2.3455856, 48.8566877),
    new Coordinate(2.3447592, 48.8551717),
    new Coordinate(2.3469681, 48.8547325),
    new Coordinate(2.3473018, 48.8563949) // Closing the polygon
};
var polygon = geofenceChecker.CreatePolygon(coordinates);

// Create a circular geofence
var circularGeofence = geofenceChecker.CreateCircularGeofence(2.3473018, 48.8563949, 100); // 100 meters radius

// Create a buffered line geofence
var lineCoordinates = new[]
{
    new Coordinate(2.3473018, 48.8563949),
    new Coordinate(2.3455856, 48.8566877)
};
var bufferedLineGeofence = geofenceChecker.CreateBufferedLineGeofence(lineCoordinates, 50); // 50 meters buffer
```

### Checking if a Point is Inside a Geofence
```csharp
var point = geofenceChecker.CreatePoint(2.346, 48.856);

// Check if the point is inside the polygon geofence
bool isInPolygon = geofenceChecker.IsPointInside(polygon, point);

// Check if the point is inside the circular geofence
bool isInCircle = geofenceChecker.IsPointInside(circularGeofence, point);
```

---

## API Reference

### `Polygon CreatePolygon(Coordinate[] coordinates)`
- **Description**: Creates a polygon geofence from an array of coordinates.
- **Parameters**:
  - `coordinates`: An array of `Coordinate` objects. Must contain at least 4 points (including the closing coordinate).
- **Returns**: A `Polygon` object.

### `Point CreatePoint(double longitude, double latitude)`
- **Description**: Creates a point at the specified longitude and latitude.
- **Parameters**:
  - `longitude`: The longitude of the point.
  - `latitude`: The latitude of the point.
- **Returns**: A `Point` object.

### `Polygon CreateCircularGeofence(double longitude, double latitude, double radiusInMeters)`
- **Description**: Creates a circular geofence around a center point.
- **Parameters**:
  - `longitude`: The longitude of the center.
  - `latitude`: The latitude of the center.
  - `radiusInMeters`: The radius of the geofence in meters.
- **Returns**: A `Polygon` object.

### `Polygon CreateBufferedLineGeofence(Coordinate[] lineCoordinates, double bufferWidthInMeters)`
- **Description**: Creates a geofence by buffering a line segment.
- **Parameters**:
  - `lineCoordinates`: An array of `Coordinate` objects representing the line.
  - `bufferWidthInMeters`: The buffer width in meters.
- **Returns**: A `Polygon` object.

### `bool IsPointInside(Geometry geofence, Point point)`
- **Description**: Checks if a point is inside a geofence.
- **Parameters**:
  - `geofence`: The geofence as a `Geometry` object.
  - `point`: The point to check.
- **Returns**: `true` if the point is inside the geofence, `false` otherwise.

---

## Contributing

Feel free to submit issues or pull requests for improvements.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.