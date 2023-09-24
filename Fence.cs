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
}