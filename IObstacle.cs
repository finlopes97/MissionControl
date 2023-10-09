namespace MissionControl;

/// <summary>
/// Represents an obstacle in the simulation.
/// </summary>
public interface IObstacle
{
    public List<OrderedPair>? Positions { get; set; }
    
    /// <summary>
    /// Gets the character code that represents the obstacle.
    /// </summary>
    char CharCode { get; }

    /// <summary>
    /// Gets the type of the obstacle as a string.
    /// </summary>
    string Type { get; }
    
    /// <summary>
    /// Determines the list position of the obstacle on the menu.
    /// </summary>
    int Priority { get; }
    
    /// <summary>
    /// Adds an obstacle to the grid.
    /// </summary>
    /// <param name="board">The board to add the obstacle to.</param>
    public void AddObstacle(ref Board board);
}