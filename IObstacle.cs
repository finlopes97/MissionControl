namespace MissionControl;

/// <summary>
/// Represents an obstacle in the simulation.
/// </summary>
public interface IObstacle
{
    /// <summary>
    /// Gets the position of the obstacle as a list of ordered pairs.
    /// </summary>
    public List<Coordinate>? Positions { get; }
    
    /// <summary>
    /// The cost of traversing the obstacle.
    /// </summary>
    public int MovementCost { get; }
    
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
    /// <param name="cell">The <see cref="Cell"/>(s) to add the obstacle to.</param>
    public void AddObstacle(ref Cell cell);

    /// <summary>
    /// Determines whether the specified cell is within the obstacle.
    /// </summary>
    /// <param name="cellToCheck">The <see cref="Cell"/> to compare against the obstacle's position and range of detection.</param>
    public bool IntersectsWithCell(Coordinate cellToCheck);
}