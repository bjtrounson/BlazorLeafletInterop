using System.Text.Json;

namespace BlazorLeafletInterop.Models.Basics;

public class Point : ICloneable
{
    public double X { get; set; }
    public double Y { get; set; }
    
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
    
    public Point()
    {
        X = 0;
        Y = 0;
    }

    public Point(string json)
    {
        var point = JsonSerializer.Deserialize<Point>(json);
        if (point is null) throw new NullReferenceException();
        X = point.X;
        Y = point.Y;
    }
    
    public Point Add(Point point)
    {
        return new Point(X + point.X, Y + point.Y);
    }
    
    public Point Subtract(Point point)
    {
        return new Point(X - point.X, Y - point.Y);
    }
    
    public Point MultiplyBy(double number)
    {
        return new Point(X * number, Y * number);
    }
    
    public Point DivideBy(double number)
    {
        return new Point(X / number, Y / number);
    }
    
    public Point ScaleBy(Point point)
    {
        return new Point(X * point.X, Y * point.Y);
    }
    
    public Point UnScaleBy(Point point)
    {
        return new Point(X / point.X, Y / point.Y);
    }
    
    public bool Equals(Point point)
    {
        return X == point.X && Y == point.Y;
    }
    
    public double DistanceTo(Point point)
    {
        var x = point.X - X;
        var y = point.Y - Y;
        return Math.Sqrt(x * x + y * y);
    }
    
    public Point Round()
    {
        return new Point(Math.Round(X), Math.Round(Y));
    }
    
    public Point Floor()
    {
        return new Point(Math.Floor(X), Math.Floor(Y));
    }
    
    public Point Ceil()
    {
        return new Point(Math.Ceiling(X), Math.Ceiling(Y));
    }
    
    public Point Truncate()
    {
        return new Point((int) X, (int) Y);
    }
    
    public bool Contains(Point point)
    {
        return Math.Abs(point.X) <= Math.Abs(X) && Math.Abs(point.Y) <= Math.Abs(Y);
    }
    
    public object Clone()
    {
        return new Point(X, Y);
    }
    
    public override string ToString()
    {
        return $"Point({X}, {Y})";
    }
}