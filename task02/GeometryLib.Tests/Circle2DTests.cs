namespace GeometryLib.Tests;

public class Circle2DConstructorTests
{
    [Theory]
    [MemberData(nameof(ValidConstructorTestData))]
    public void Constructor_WithValidRadius(Point2D center, double radius)
    {
        Circle2D circle = new Circle2D(center, radius);
        Assert.Equal(center, circle.Center);
        Assert.Equal(radius, circle.Radius);
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorTestData))]
    public void Constructor_WithInvalidRadius(Point2D center, double radius)
    {
        Assert.Throws<ArgumentException>(() => new Circle2D(center, radius));
    }

    public static TheoryData<Point2D, double> ValidConstructorTestData()
    {
        return new TheoryData<Point2D, double>
        {
            { new Point2D(0, 0), 1 },
            { new Point2D(1, 2), 0.5 },
            { new Point2D(-1, -1), 10 },
        };
    }

    public static TheoryData<Point2D, double> InvalidConstructorTestData()
    {
        return new TheoryData<Point2D, double>
        {
            { new Point2D(0, 0), 0 },
            { new Point2D(1, 2), -1 },
            { new Point2D(-1, -1), -0.1 },
        };
    }
}

public class Circle2DPropertiesTests
{
    [Theory]
    [MemberData(nameof(PropertiesTestData))]
    public void Properties_WithDifferentRadius(double radius, double expectedDiameter, double expectedCircumference, double expectedArea)
    {
        Circle2D circle = new Circle2D(new Point2D(0, 0), radius);
        double actualDiameter = circle.Diameter;
        double actualCircumference = circle.Circumference;
        double actualArea = circle.Area;
        Assert.Equal(expectedDiameter, actualDiameter, 5);
        Assert.Equal(expectedCircumference, actualCircumference, 5);
        Assert.Equal(expectedArea, actualArea, 5);
    }

    public static TheoryData<double, double, double, double> PropertiesTestData()
    {
        return new TheoryData<double, double, double, double>
        {
            { 1, 2, 2 * Math.PI, Math.PI },
            { 2, 4, 4 * Math.PI, 4 * Math.PI },
            { 5, 10, 10 * Math.PI, 25 * Math.PI },
        };
    }
}

public class Circle2DDistanceToPointTests
{
    [Theory]
    [MemberData(nameof(DistanceToPointTestData))]
    public void DistanceToPoint_WithDifferentPoints(Point2D center, double radius, Point2D point, double expectedDistance)
    {
        Circle2D circle = new Circle2D(center, radius);
        double actualDistance = circle.DistanceTo(point);
        Assert.Equal(expectedDistance, actualDistance, 5);
    }

    public static TheoryData<Point2D, double, Point2D, double> DistanceToPointTestData()
    {
        return new TheoryData<Point2D, double, Point2D, double>
        {
            { new Point2D(0, 0), 5, new Point2D(0, 0), 5 },
            { new Point2D(0, 0), 5, new Point2D(5, 0), 0 },
            { new Point2D(0, 0), 5, new Point2D(10, 0), 5 },
            { new Point2D(1, 1), 3, new Point2D(5, 1), 1 },
        };
    }
}

public class Circle2DDistanceToCircleTests
{
    [Theory]
    [MemberData(nameof(DistanceToCircleTestData))]
    public void DistanceToCircle_WithDifferentCircles(Circle2D circle1, Circle2D circle2, double expectedDistance)
    {
        double actualDistance = circle1.DistanceTo(circle2);
        Assert.Equal(expectedDistance, actualDistance, 5);
    }

    public static TheoryData<Circle2D, Circle2D, double> DistanceToCircleTestData()
    {
        return new TheoryData<Circle2D, Circle2D, double>
        {
            {
                new Circle2D(new Point2D(0, 0), 5),
                new Circle2D(new Point2D(15, 0), 5),
                5
            },
            {
                new Circle2D(new Point2D(0, 0), 5),
                new Circle2D(new Point2D(0, 0), 5),
                0
            },
            {
                new Circle2D(new Point2D(0, 0), 5),
                new Circle2D(new Point2D(8, 0), 3),
                0
            },
        };
    }
}

public class Circle2DContainsPointTests
{
    [Theory]
    [MemberData(nameof(ContainsPointTestData))]
    public void ContainsPoint_WithDifferentPoints(Circle2D circle, Point2D point, bool expected)
    {
        bool actual = circle.Contains(point);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<Circle2D, Point2D, bool> ContainsPointTestData()
    {
        return new TheoryData<Circle2D, Point2D, bool>
        {
            { new Circle2D(new Point2D(0, 0), 5), new Point2D(0, 0), true },
            { new Circle2D(new Point2D(0, 0), 5), new Point2D(3, 4), true },
            { new Circle2D(new Point2D(0, 0), 5), new Point2D(5, 0), true },
            { new Circle2D(new Point2D(0, 0), 5), new Point2D(6, 0), false },
        };
    }
}

public class Circle2DIntersectsWithTests
{
    [Theory]
    [MemberData(nameof(IntersectsWithTestData))]
    public void IntersectsWith_WithDifferentCircles(Circle2D circle1, Circle2D circle2, bool expected)
    {
        bool actual = circle1.IntersectsWith(circle2);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<Circle2D, Circle2D, bool> IntersectsWithTestData()
    {
        return new TheoryData<Circle2D, Circle2D, bool>
        {
            {
                new Circle2D(new Point2D(0, 0), 5),
                new Circle2D(new Point2D(0, 0), 3),
                false
            },
            {
                new Circle2D(new Point2D(0, 0), 5),
                new Circle2D(new Point2D(2, 0), 2),
                false
            },
            {
                new Circle2D(new Point2D(0, 0), 5),
                new Circle2D(new Point2D(8, 0), 3),
                true
            },
            {
                new Circle2D(new Point2D(0, 0), 5),
                new Circle2D(new Point2D(15, 0), 3),
                false
            },
        };
    }
}

public class Circle2DContainsCircleTests
{
    [Theory]
    [MemberData(nameof(ContainsCircleTestData))]
    public void ContainsCircle_WithDifferentCircles(Circle2D circle1, Circle2D circle2, bool expected)
    {
        bool actual = circle1.Contains(circle2);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<Circle2D, Circle2D, bool> ContainsCircleTestData()
    {
        return new TheoryData<Circle2D, Circle2D, bool>
        {
            {
                new Circle2D(new Point2D(0, 0), 10),
                new Circle2D(new Point2D(0, 0), 5),
                true
            },
            {
                new Circle2D(new Point2D(0, 0), 5),
                new Circle2D(new Point2D(0, 0), 5),
                true
            },
            {
                new Circle2D(new Point2D(0, 0), 5),
                new Circle2D(new Point2D(3, 0), 3),
                false
            },
            {
                new Circle2D(new Point2D(0, 0), 5),
                new Circle2D(new Point2D(8, 0), 3),
                false
            },
        };
    }
}