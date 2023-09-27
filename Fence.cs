namespace MissionControl;

/// <summary>
/// Represents a fence obstacle in the mission control system.
/// </summary>
public class Fence : IObstacle
{
    /// <summary>
    /// Gets the starting position of the fence as an ordered pair.
    /// </summary>
    public OrderedPair Position { get; }
    
    /// <summary>
    /// Gets the character code representing the fence.
    /// </summary>
    public char CharCode { get; }
    
    /// <summary>
    /// Gets the type of the obstacle as a string.
    /// </summary>
    public string Type { get; }
    
    /// <summary>
    /// Gets the ending position of the fence as an ordered pair.
    /// </summary>
    public OrderedPair EndingPosition { get; }

    /// <summary>
    /// Adds a fence to the grid.
    /// </summary>
    /// <param name="board">The board to add the fence to.</param>
    public void AddObstacle(ref Board board)
    {
        int startX = Position.X;
        int startY = Position.Y;
        int endX = EndingPosition.X;
        int endY = EndingPosition.Y;
        
        if (startX == endX)
        {
            // Vertical fence
            for (int y = Math.Min(startY, endY); y <= Math.Max(startY, endY); y++)
            {
                board.Grid[startX, y].CurrentObstacle = this;
            }
        }
        else if (startY == endY)
        {
            // Horizontal fence
            for (int x = Math.Min(startX, endX); x <= Math.Max(startX, endX); x++)
            {
                board.Grid[x, startY].CurrentObstacle = this;
            }
        }
        else
        {
            throw new ArgumentException("Fences must be horizontal or vertical.");
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
    /// <param name="startingPos">The starting position of the fence as an ordered pair.</param>
    /// <param name="endingPosition">The ending position of the fence as an ordered pair.</param>
    /// <exception cref="ArgumentException">Thrown when the fence is not horizontal or vertical.</exception>
    public Fence(OrderedPair startingPos, OrderedPair endingPosition)
    {
        if (startingPos.IsEqual(endingPosition) || startingPos.IsDiagonalFrom(endingPosition))
        {
            throw new ArgumentException("Fences must be horizontal or vertical.");
        }
        Position = startingPos;
        CharCode = 'f';
        Type = "Fence";

        EndingPosition = endingPosition;
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