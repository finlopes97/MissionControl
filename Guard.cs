namespace MissionControl;

/// <summary>
/// Represents a guard obstacle in the mission control system.
/// </summary>
public class Guard : IObstacle
{
    /// <summary>
    /// Gets the position of the guard as an ordered pair.
    /// </summary>
    public OrderedPair Position { get; }
    
    /// <summary>
    /// Gets the character code representing the guard.
    /// </summary>
    public char CharCode { get; }
    
    /// <summary>
    /// Gets the type of the obstacle as a string.
    /// </summary>
    public string Type { get; }
    
    /// <summary>
    /// Gets the name of the guard.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Guard"/> class with the specified position.
    /// </summary>
    /// <param name="pos">The position of the guard as an ordered pair.</param>
    public Guard(OrderedPair pos)
    {
        Position = pos;
        CharCode = 'g';
        Type = "Guard";
        Name = "Shannanthony";
    }
}