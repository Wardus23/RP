namespace GeometryLib;

public struct Point2D(double x, double y)
{
    public const int Tolerance = 5;

    public double X { get; } = x;

    public double Y { get; } = y;

    public readonly double DistanceTo(Point2D other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;
        return Math.Round(Math.Sqrt(dx * dx + dy * dy), Tolerance);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}