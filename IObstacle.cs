namespace MissionControl;

/// <summary>
/// Represents an obstacle in the simulation.
/// </summary>
public interface IObstacle
{
    /// <summary>
    /// Gets the position of the obstacle as an OrderedPair.
    /// </summary>
    OrderedPair Position { get; }

    /// <summary>
    /// Gets the character code that represents the obstacle.
    /// </summary>
    char CharCode { get; }

    /// <summary>
    /// Gets the type of the obstacle as a string.
    /// </summary>
    string Type { get; }
}