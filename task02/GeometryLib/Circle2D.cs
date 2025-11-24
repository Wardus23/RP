namespace GeometryLib;

public class Circle2D
{
    public Circle2D(Point2D center, double radius)
    {
        if (radius <= 0)
        {
            throw new ArgumentException("Radius must be positive", nameof(radius));
        }

        Center = center;
        Radius = radius;
    }

    public Point2D Center { get; }

    public double Radius { get; }

    public double Diameter => 2 * Radius;

    public double Circumference => 2 * Math.PI * Radius;

    public double Area => Math.PI * Radius * Radius;

    public double DistanceTo(Point2D p)
    {
        double distanceToCenter = Center.DistanceTo(p);
        return Math.Abs(distanceToCenter - Radius);
    }

    public double DistanceTo(Circle2D other)
    {
        double centerDistance = Center.DistanceTo(other.Center);
        double distanceBetweenCircles = centerDistance - (Radius + other.Radius);

        return Math.Max(0, distanceBetweenCircles);
    }

    public bool Contains(Point2D p)
    {
        double distanceToCenter = Center.DistanceTo(p);
        return distanceToCenter <= Radius;
    }

    public bool IntersectsWith(Circle2D other)
    {
        double centerDistance = Center.DistanceTo(other.Center);
        double sumRadii = Radius + other.Radius;
        double diffRadii = Math.Abs(Radius - other.Radius);

        return centerDistance <= sumRadii && centerDistance >= diffRadii;
    }

    public bool Contains(Circle2D other)
    {
        double centerDistance = Center.DistanceTo(other.Center);
        return centerDistance + other.Radius <= Radius;
    }

    public override string ToString() => $"Circle(Center: {Center}, Radius: {Radius})";
}