namespace MissionControl;

/// <summary>
/// Represents a guard obstacle in the mission control system.
/// </summary>
public class Guard : IObstacle
{
    /// <summary>
    /// Gets the position of the guard as a list of ordered pairs.
    /// </summary>
    public List<Coordinate>? Positions { get; set; }
    
    /// <summary>
    /// Guards have no movement cost as they are not traversable.
    /// </summary>
    public int MovementCost => 0;

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
    /// <param name="cell">The cell to add the guard to.</param>
    public void AddObstacle(ref Cell cell)
    {
        if (Positions != null)
        {
            cell.CurrentObstacle = this;
        }
    }
    
    /// <summary>
    /// Checks if the guard obstacle intersects with a specified cell.
    /// </summary>
    /// <param name="cellToCheck">The cell to check for intersection.</param>
    /// <returns>True if the guard intersects with the cell, otherwise false.</returns>
    public bool IntersectsWithCell(Coordinate cellToCheck)
    {
        if (Positions == null) return false;
        
        if (Positions.Contains(cellToCheck))
            return true;
        else
            return false;
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
    /// Initializes a new instance of the guard class with the specified position.
    /// </summary>
    /// <param name="guardPosition">The position of the guard as an ordered pair.</param>
    public Guard(Coordinate guardPosition)
    {
        Positions = new List<Coordinate>() { guardPosition };
        CharCode = 'g';
        Type = "Guard";
    }

    /// <summary>
    /// Initialises a new instance of the guard class with no additional parameters.
    /// Specifically for generating a dynamic list in the main menu, not for use in the actual program.
    /// </summary>
    public Guard()
    {
        CharCode = 'g';
        Type = "Guard";
    }
}