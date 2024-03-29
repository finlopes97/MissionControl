namespace MissionControl;

/// <summary>
/// Represents an ordered pair of integers.
/// </summary>
public readonly struct Coordinate
{
    /// <summary>
    /// Gets the X-coordinate of the ordered pair.
    /// </summary>
    public int X { get; }
    
    /// <summary>
    /// Gets the Y-coordinate of the ordered pair.
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Coordinate"/> struct with the specified X and Y coordinates.
    /// </summary>
    /// <param name="x">The X-coordinate of the ordered pair.</param>
    /// <param name="y">The Y-coordinate of the ordered pair.</param>
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Returns a string representation of the ordered pair in the format "(X,Y)".
    /// </summary>
    /// <returns>A string representation of the ordered pair.</returns>
    public override string ToString()
    {
        return $"({X},{Y})";
    }

    /// <summary>
    /// Converts the ordered pair to an array of integers.
    /// </summary>
    /// <returns>An array containing the X and Y coordinates of the ordered pair.</returns>
    public int[] ToIntArray()
    {
        return new [] { X, Y };
    }
    
    /// <summary>
    /// Checks if this Coordinate is equal to another Coordinate.
    /// </summary>
    /// <param name="other">The other Coordinate to compare with.</param>
    /// <returns>True if the OrderedPairs are equal; otherwise, false.</returns>
    public bool IsEqual(Coordinate other)
    {
        return X == other.X && Y == other.Y;
    }
    
    /// <summary>
    /// Checks if this Coordinate is diagonal from another Coordinate.
    /// </summary>
    /// <param name="other">The other Coordinate to compare with.</param>
    /// <returns>True if the OrderedPairs are diagonal; otherwise, false.</returns>
    public bool IsDiagonalFrom(Coordinate other)
    {
        int xDiff = Math.Abs(X - other.X);
        int yDiff = Math.Abs(Y - other.Y);

        return xDiff == yDiff && xDiff != 0 && yDiff != 0;
    }
    
    /// <summary>
    /// Calculates the length (magnitude) of the vector represented by the X and Y coordinates.
    /// </summary>
    /// <returns>The length of the vector.</returns>
    public double Length()
    {
        return Math.Sqrt(X * X + Y * Y);
    }
}