using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace FakePathConsoleApp;

public class WriteShapefile
{
    public static void ExportShapefile(List<PolygonData> polygons, string filename)
    {
        var factory = new GeometryFactory();
        var features = FormatPolygonsToFeatures(polygons, factory);
        WriteToFile(features, factory, filename);
    }

    private static List<Feature> FormatPolygonsToFeatures(List<PolygonData> polygons, GeometryFactory factory)
    {
        var features = new List<Feature>();

        foreach (var polygon in polygons)
        {
            var attr = new AttributesTable
            {
                {"Name", polygon.Name},
                {"Value", polygon.Value},
                {"ID", polygon.ID.ToString()}
            };
            var shape = factory.CreatePolygon(polygon.Coordinates);
            features.Add(new Feature(shape, attr));
        }

        return features;
    }

    private static void WriteToFile(List<Feature> features, GeometryFactory factory, string filename)
    {
        // initialize a new data writer
        var writer = new ShapefileDataWriter(filename, factory)
        {
            Header = ShapefileDataWriter.GetHeader(features[0], features.Count)
        };
        if (File.Exists(filename + ".shp"))
        {
            writer.Write(features, true);
        }
        else
        {
            writer.Write(features);
        }
    }
}
