namespace MissionControl;

/// <summary>
/// Represents a fence obstacle in the mission control system.
/// </summary>
public class Fence : IObstacle
{
    /// <summary>
    /// Gets the position of the fence as a list of ordered pairs.
    /// </summary>
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
    
    private OrderedPair FenceStartingPosition { get; }
    
    private OrderedPair FenceEndingPosition { get; }

    /// <summary>
    /// Adds a fence to the grid.
    /// </summary>
    /// <param name="cell">The cell to add the fence to.</param>
    public void AddObstacle(ref Cell cell)
    {
        if (Positions != null && Positions.Contains(cell.CellPosition))
        {
            cell.CurrentObstacle = this;
        }
    }
    
    /// <summary>
    /// Checks if the fence obstacle intersects with a specified cell.
    /// </summary>
    /// <param name="cellToCheck">The cell to check for intersection.</param>
    /// <returns>True if the fence intersects with the cell, otherwise false.</returns>
    public bool IntersectsWithCell(OrderedPair cellToCheck)
    {
        return Positions != null && Positions.Contains(cellToCheck);
    }

    private List<OrderedPair> Points()
    {
        int numPoints;
        List<OrderedPair> points = new List<OrderedPair>();
        
        int startX = FenceStartingPosition.X;
        int startY = FenceStartingPosition.Y;
        int endX = FenceEndingPosition.X;
        int endY = FenceEndingPosition.Y;
        
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

        return points;
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
    /// Initializes a new instance of the fence class with the specified starting and ending positions.
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

        FenceStartingPosition = fenceStartingPosition;
        FenceEndingPosition = fenceEndingPosition;
        Positions = Points();
        CharCode = 'f';
        Type = "Fence";
    }
    
    /// <summary>
    /// Initialises a new instance of the fence class with no additional parameters.
    /// Specifically for generating a dynamic list in the main menu, not for use in the actual program.
    /// </summary>
    public Fence()
    {
        CharCode = 'f';
        Type = "Fence";
    }
}