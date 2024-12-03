# Geofence Checker

A powerful C# library for creating, parsing, and managing geofences using the [NetTopologySuite](https://nettopologysuite.github.io/NetTopologySuite/). This library supports various geofence types such as polygons, routes, and circular geofences and includes utilities to check point containment and parse geofence definitions from strings.

---

## Features

- **Create Geofences**:
  - Polygons with specified coordinates
  - Circular geofences with a center point and radius
  - Buffered line geofences
- **Parse Geofences**:
  - Interpret geofence definitions from string inputs (POLYGON, ROUTE, CIRCULAR formats)
- **Point-in-Geofence Check**:
  - Verify if a point lies within a specified geofence.

---

## Installation

Ensure the `NetTopologySuite` NuGet package is installed:

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

// Create a point
var point = geofenceChecker.CreatePoint(2.3473018, 48.8563949);

// Create a polygon geofence
var polygonCoordinates = new[]
{
    new Coordinate(2.3473018, 48.8563949),
    new Coordinate(2.3455856, 48.8566877),
    new Coordinate(2.3447592, 48.8551717),
    new Coordinate(2.3473018, 48.8563949) // Closing the polygon
};
var polygon = geofenceChecker.CreatePolygon(polygonCoordinates);

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

### Parsing a Geofence from a String
```csharp
string geofenceString = "POLYGON=(48.8564 2.3473, 48.8567 2.3456, 48.8552 2.3448, 48.8564 2.3473)";
var polygonGeofence = geofenceChecker.ParseGeofence(geofenceString);

string circularString = "CIRCULAR=(48.8564 2.3473 100)"; // Center at (48.8564, 2.3473) with 100 meters radius
var circularGeofence = geofenceChecker.ParseGeofence(circularString);

string routeString = "ROUTE=(48.8564 2.3473, 48.8570 2.3480)";
var routeGeofence = geofenceChecker.ParseGeofence(routeString);
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

### Geofence Creation
#### `Point CreatePoint(double longitude, double latitude)`
- **Description**: Creates a point with the given longitude and latitude.
- **Parameters**:
  - `longitude`: Longitude of the point.
  - `latitude`: Latitude of the point.
- **Returns**: A `Point` object.

#### `Geometry CreatePolygon(Coordinate[] coordinates)`
- **Description**: Creates a polygon geofence.
- **Parameters**:
  - `coordinates`: An array of `Coordinate` objects (at least 4, including the closing coordinate).
- **Returns**: A `Geometry` object representing the polygon.

#### `Geometry CreateCircularGeofence(double longitude, double latitude, double radiusInMeters)`
- **Description**: Creates a circular geofence.
- **Parameters**:
  - `longitude`: Longitude of the center point.
  - `latitude`: Latitude of the center point.
  - `radiusInMeters`: Radius in meters.
- **Returns**: A `Geometry` object representing the circle.

#### `Geometry CreateBufferedLineGeofence(Coordinate[] lineCoordinates, double bufferWidthInMeters)`
- **Description**: Creates a geofence by buffering a line.
- **Parameters**:
  - `lineCoordinates`: An array of `Coordinate` objects (at least 2).
  - `bufferWidthInMeters`: Buffer width in meters.
- **Returns**: A `Geometry` object representing the buffered line.

### Geofence Parsing
#### `Geometry ParseGeofence(string geofenceString)`
- **Description**: Parses a geofence string into a `Geometry` object.
- **Supported Formats**:
  - **POLYGON**: `"POLYGON=(lat lon, lat lon, ...)"` (e.g., `"POLYGON=(48.85 2.35, 48.85 2.34, 48.34 2.32)"`)
  - **ROUTE**: `"ROUTE=(lat lon, lat lon, ...)"` (e.g., `"ROUTE=(48.85 2.35, 48.86 2.36)"`)
  - **CIRCULAR**: `"CIRCULAR=(lat lon radius)"` (e.g., `"CIRCULAR=(48.85 2.35 200)"`)
- **Returns**: A `Geometry` object.

### Point Containment Check
#### `bool IsPointInside(Geometry geofence, Point point)`
- **Description**: Checks if a point is inside a geofence.
- **Parameters**:
  - `geofence`: The geofence as a `Geometry` object.
  - `point`: The point to check.
- **Returns**: `true` if the point is inside, `false` otherwise.

---

## Contributing

Contributions are welcome! Submit an issue or a pull request for suggestions or enhancements.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.