using NetTopologySuite.Geometries;

namespace FakePathConsoleApp;

public class PolygonData
{
    public string Name { get; set; }

    public Coordinate[] Coordinates { get; set; }

    public Guid ID { get; set; }

    public double Value { get; set; }
}
