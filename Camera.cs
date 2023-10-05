namespace MissionControl;

public class Camera : IObstacle
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
    /// Gets or sets the direction of the camera. Represented as an ordered pair (e.g. N=(0,1)).
    /// </summary>
    public static OrderedPair Direction { get; set; }

    public Camera(OrderedPair cameraPosition, OrderedPair cameraDirection)
    {
        Positions = new List<OrderedPair>() { cameraPosition };
        CharCode = 'c';
        Type = "Camera";
        Direction = cameraDirection;
    }
    
    /// <summary>
    /// Initialises a new instance of the <see cref="Camera"/> class with no additional parameters.
    /// Specifically for generating a dynamic list in the main menu, not for use in the actual program.
    /// </summary>
    public Camera()
    {
        CharCode = 'c';
        Type = "Camera";
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
    /// Adds the camera as an obstacle to the specified board.
    /// </summary>
    /// <param name="board">The board to which the camera is added.</param>
    public void AddObstacle(ref Board board)
    {
        throw new NotImplementedException("Fuck");
    }
}