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
    /// Determines the list position in which this obstacle appears in the main menu.
    /// </summary>
    public int Priority => 3;

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
    /// Adds the camera as an obstacle to the specified cell. Looks in the direction specified by <see cref="Direction"/>.
    /// Extends indefinitely in a cone shape.
    /// </summary>
    /// <param name="cell">The <see cref="Cell"/> to which the camera is added.</param>
    public void AddObstacle(ref Cell cell)
    {
        if (Positions != null && (Positions.Contains(cell.CellPosition) || InCone(Positions.First(), cell.CellPosition)))
        {
            cell.CurrentObstacle = this;
        }
    }

    /// <summary>
    /// Checks the cells in front of the camera in an infinite range, in a 45 degree cone, based on the <see cref="Direction"/> of the camera.
    /// </summary>
    /// <param name="cameraPosition">The <see cref="OrderedPair"/> that represents the camera's origin position.</param>
    /// <param name="cellPosition">The <see cref="OrderedPair"/> that represents the cell to compare to.</param>
    /// <returns>Returns true if the cell is within the camera's cone of vision, false if the cell is outside of it.</returns>
    private static bool InCone(OrderedPair cameraPosition, OrderedPair cellPosition)
    {
        var vectorToCell = new OrderedPair(
            cellPosition.X - cameraPosition.X, 
            cellPosition.Y - cellPosition.Y);

        var dotProduct = (vectorToCell.X * Direction.X + vectorToCell.Y * Direction.Y) /
                         (vectorToCell.Length() * Direction.Length());
        
        double halfConeAngleCosine = Math.Cos(Math.PI / 8);
        
        return dotProduct >= halfConeAngleCosine;
    }
}