using System.Drawing;

namespace GeometryLib.Tests;

public class Point2DTests
{
    [Theory]
    [MemberData(nameof(DistanceToPointsParams))]
    public void Can_calculate_distance_to_different_points(Point2D p1, Point2D p2, double expectedDistance)
    {
        double actualDistance = p1.DistanceTo(p2);
        Assert.Equal(expectedDistance, actualDistance, 5);
    }

    public static TheoryData<Point2D, Point2D, double> DistanceToPointsParams()
    {
        return new TheoryData<Point2D, Point2D, double>
        {
            { new Point2D(0, 0), new Point2D(3, 4), 5 },
            { new Point2D(1, 1), new Point2D(1, 1), 0 },
            { new Point2D(0, 0), new Point2D(0, 5), 5 },
            { new Point2D(-1, -1), new Point2D(2, 3), 5 },
            { new Point2D(-1000, -1000), new Point2D(1000, 1000), 2828.42712 },
        };
    }
}