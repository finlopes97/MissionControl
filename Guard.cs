namespace MissionControl;

/// <summary>
/// Represents a guard obstacle in the mission control system.
/// </summary>
public class Guard : IObstacle
{
    /// <summary>
    /// Gets the position of the guard as an ordered pair.
    /// </summary>
    public List<OrderedPair>? Positions { get; set; }
    
    /// <summary>
    /// Gets the character code representing the guard.
    /// </summary>
    public char CharCode { get; }
    
    /// <summary>
    /// Gets the type of the obstacle as a string.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Determines the list position in which this obstacle appears in the main menu.
    /// </summary>
    public int Priority => 0;
    
    /// <summary>
    /// Adds a guard to the grid.
    /// </summary>
    /// <param name="board">The board to add the guard to.</param>
    public void AddObstacle(ref Board board)
    {
        if (Positions != null)
        {
            board.Grid[Positions.First().X, Positions.First().Y].CurrentObstacle = this;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Guard"/> class with the specified position.
    /// </summary>
    /// <param name="guardPosition">The position of the guard as an ordered pair.</param>
    public Guard(OrderedPair guardPosition)
    {
        Positions = new List<OrderedPair>() { guardPosition };
        CharCode = 'g';
        Type = "Guard";
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Guard"/> class with no additional parameters.
    /// Specifically for generating a dynamic list in the main menu, not for use in the actual program.
    /// </summary>
    public Guard()
    {
        CharCode = 'g';
        Type = "Guard";
    }
        
    /// <summary>
    /// Builds a string for use in the main menu
    /// </summary>
    /// <returns>A string to be used when printing the main menu of the program.</returns>
    public override string ToString()
    {
        return $"{CharCode}) Add '{Type}' obstacle.";
    }
}