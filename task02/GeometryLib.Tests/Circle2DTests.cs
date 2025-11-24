namespace GeometryLib.Tests;

public class Circle2DConstructorTests
{
    [Theory]
    [MemberData(nameof(ValidConstructorParams))]
    public void Can_create_circle_with_valid_radius(Point2D center, double radius)
    {
        Circle2D circle = new Circle2D(center, radius);
        Assert.Equal(center, circle.Center);
        Assert.Equal(radius, circle.Radius);
    }

    [Theory]
    [MemberData(nameof(InvalidConstructorParams))]
    public void Cannot_create_circle_with_invalid_radius(Point2D center, double radius)
    {
        Assert.Throws<ArgumentException>(() => new Circle2D(center, radius));
    }

    public static TheoryData<Point2D, double> ValidConstructorParams()
    {
        return new TheoryData<Point2D, double>
        {
            { new Point2D(0, 0), 1 },
            { new Point2D(1, 2), 0.5 },
            { new Point2D(-1, -1), 10 },
        };
    }

    public static TheoryData<Point2D, double> InvalidConstructorParams()
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
    [MemberData(nameof(PropertiesParams))]
    public void Can_check_circle_properties_with_different_radius(double radius, double expectedDiameter, double expectedCircumference, double expectedArea)
    {
        Circle2D circle = new Circle2D(new Point2D(0, 0), radius);
        double actualDiameter = circle.Diameter;
        double actualCircumference = circle.Circumference;
        double actualArea = circle.Area;
        Assert.Equal(expectedDiameter, actualDiameter, 5);
        Assert.Equal(expectedCircumference, actualCircumference, 5);
        Assert.Equal(expectedArea, actualArea, 5);
    }

    public static TheoryData<double, double, double, double> PropertiesParams()
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
    [MemberData(nameof(DistanceToPointParams))]
    public void Can_calculate_distance_to_point(Point2D center, double radius, Point2D point, double expectedDistance)
    {
        Circle2D circle = new Circle2D(center, radius);
        double actualDistance = circle.DistanceTo(point);
        Assert.Equal(expectedDistance, actualDistance, 5);
    }

    public static TheoryData<Point2D, double, Point2D, double> DistanceToPointParams()
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
    [MemberData(nameof(DistanceToCircleParams))]
    public void Can_calculate_distance_to_circle(Circle2D circle1, Circle2D circle2, double expectedDistance)
    {
        double actualDistance = circle1.DistanceTo(circle2);
        Assert.Equal(expectedDistance, actualDistance, 5);
    }

    public static TheoryData<Circle2D, Circle2D, double> DistanceToCircleParams()
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
    [MemberData(nameof(ContainsPointParams))]
    public void Can_circle_contain_points(Circle2D circle, Point2D point, bool expected)
    {
        bool actual = circle.Contains(point);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<Circle2D, Point2D, bool> ContainsPointParams()
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
    [MemberData(nameof(IntersectsWithParams))]
    public void Can_intersect_with_different_circles(Circle2D circle1, Circle2D circle2, bool expected)
    {
        bool actual = circle1.IntersectsWith(circle2);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<Circle2D, Circle2D, bool> IntersectsWithParams()
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
    [MemberData(nameof(ContainsCircleParams))]
    public void Can_circle_contain_other_circle(Circle2D circle1, Circle2D circle2, bool expected)
    {
        bool actual = circle1.Contains(circle2);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<Circle2D, Circle2D, bool> ContainsCircleParams()
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