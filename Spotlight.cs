namespace MissionControl;

public class Spotlight : IObstacle
{
    public List<OrderedPair>? Positions { get; set; }
    
    /// <summary>
    /// Gets the character code that represents the obstacle.
    /// </summary>
    public char CharCode { get; }

    /// <summary>
    /// Gets the type of the obstacle as a string.
    /// </summary>
    public string Type { get; }
    
    /// <summary>
    /// Initialises a new instance of the <see cref="Spotlight"/> class with no additional parameters.
    /// Specifically for generating a dynamic list in the main menu, not for use in the actual program.
    /// </summary>
    public Spotlight()
    {
        CharCode = 's';
        Type = "Spotlight";
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
    /// Adds an obstacle to the grid.
    /// </summary>
    /// <param name="board">The board to add the obstacle to.</param>
    public void AddObstacle(ref Board board)
    {
        throw new NotImplementedException("Fuck");
    }
}