namespace MissionControl;

/// <summary>
/// Represents a fence obstacle in the mission control system.
/// </summary>
public class Fence : IObstacle
{
    public List<OrderedPair>? Positions { get; set; }
    
    /// <summary>
    /// Gets the character code representing the fence.
    /// </summary>
    public char CharCode { get; }
    
    /// <summary>
    /// Gets the type of the obstacle as a string.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Determines the list position in which this obstacle appears in the main menu.
    /// </summary>
    public int Priority => 1;

    public void AddObstacle(ref Board board)
    {
        foreach (var pos in Positions)
        {
            board.Grid[pos.X, pos.Y].CurrentObstacle = this;
        }
    }
    
    /// <summary>
    /// Builds a string for use in the main menu
    /// </summary>
    /// <returns>A string to be used when printing the main menu of the program.</returns>
    public override string ToString()
    {
        return $"{CharCode}) Add '{Type}' obstacle.";
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Fence"/> class with the specified starting and ending positions.
    /// </summary>
    /// <param name="fenceStartingPosition">The starting position of the fence as an ordered pair.</param>
    /// <param name="fenceEndingPosition">The ending position of the fence as an ordered pair.</param>
    /// <exception cref="ArgumentException">Thrown when the fence is not horizontal or vertical.</exception>
    public Fence(OrderedPair fenceStartingPosition, OrderedPair fenceEndingPosition)
    {
        if (fenceStartingPosition.IsEqual(fenceEndingPosition) || fenceStartingPosition.IsDiagonalFrom(fenceEndingPosition))
        {
            throw new ArgumentException("Fences must be horizontal or vertical.");
        }
        
        int numPoints;
        var points = new List<OrderedPair>();
        
        var startX = fenceStartingPosition.X;
        var startY = fenceStartingPosition.Y;
        var endX = fenceEndingPosition.X;
        var endY = fenceEndingPosition.Y;
        
        if (startX == endX) // Vertical fence
        {
            numPoints = Math.Abs(endY - startY) + 1;

            for (int i = 0; i < numPoints; i++)
            {
                int currentY = Math.Min(startY, endY) + i;
                points.Add(new OrderedPair(startX, currentY));
            }
        }
        else if (startY == endY) // Horizontal fence
        {
            numPoints = Math.Abs(endX - startX) + 1;

            for (int i = 0; i < numPoints; i++)
            {
                int currentX = Math.Min(startX, endX) + i;
                points.Add(new OrderedPair(currentX, startY));
            }
        }
        else
        {
            throw new ArgumentException("Fences must be horizontal or vertical.");
        }
        
        Positions = points;
        CharCode = 'f';
        Type = "Fence";
    }
    
    /// <summary>
    /// Initialises a new instance of the <see cref="Fence"/> class with no additional parameters.
    /// Specifically for generating a dynamic list in the main menu, not for use in the actual program.
    /// </summary>
    public Fence()
    {
        CharCode = 'f';
        Type = "Fence";
    }
}