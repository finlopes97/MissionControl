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
    private OrderedPair Direction { get; }


    /// <summary>
    /// Gets the origin position of the <see cref="Camera"/> as an <see cref="OrderedPair"/>.
    /// </summary>
    /// <returns>Returns an <see cref="OrderedPair"/> representing the origin.</returns>
    /// <exception cref="Exception">Throws an exception if object has no positions.</exception>
    public OrderedPair OriginPosition()
    {
        if (Positions == null) 
            throw new Exception( "Camera object has no positions." );
        else 
            return Positions.First();
    }
    
    /// <summary>
    /// Adds the camera as an obstacle to the specified cell. Looks in the direction specified by <see cref="Direction"/>.
    /// Extends indefinitely in a cone shape.
    /// </summary>
    /// <param name="cell">The <see cref="Cell"/> to which the camera is added.</param>
    public void AddObstacle(ref Cell cell)
    {
        if(Positions == null) return;

        if (Positions.Contains(cell.CellPosition))
            cell.CurrentObstacle = this;
        else if (InCone(OriginPosition(), cell.CellPosition))
            cell.CurrentObstacle = this;
    }
    
    /// <summary>
    /// Checks if a cell intersects with the camera or if it is in the camera's cone of vision.
    /// </summary>
    /// <param name="cellToCheck">The cell to check for intersection.</param>
    /// <returns>True if the camera intersects with the cell, otherwise false.</returns>
    public bool IntersectsWithCell(OrderedPair cellToCheck)
    {
        if (Positions == null) return false;
        
        if (Positions.Contains(cellToCheck))
            return true;
        else if (InCone(OriginPosition(), cellToCheck))
            return true;
        else
            return false;
    }

    /// <summary>
    /// Checks the cells in front of the camera in an infinite range, in a 45 degree cone, based on the direction of the camera.
    /// </summary>
    /// <param name="cameraPosition">The camera's initial position.</param>
    /// <param name="cellPosition">The position of the cell to compare to.</param>
    /// <returns>Returns true if the cell is within the camera's cone of vision, false if the cell is outside of it.</returns>
    public bool InCone(OrderedPair cameraPosition, OrderedPair cellPosition)
    {
        OrderedPair vectorToCell = new OrderedPair(
            cellPosition.X - cameraPosition.X, 
            cellPosition.Y - cameraPosition.Y);

        double angleToCell = Math.Atan2(vectorToCell.Y, vectorToCell.X);
        double angleToDirection = Math.Atan2(Direction.Y, Direction.X);
        
        double angleDifference = angleToCell - angleToDirection;

        while (angleDifference > Math.PI) angleDifference -= 2 * Math.PI;
        while (angleDifference <= -Math.PI) angleDifference += 2 * Math.PI;

        const double halfConeAngle = Math.PI / 4;

        return Math.Abs(angleDifference) <= halfConeAngle;
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
    /// Initializes a new instance of the camera class with the specified position and direction.
    /// </summary>
    /// <param name="cameraPosition">The initial position of the camera.</param>
    /// <param name="cameraDirection">The direction that the camera is facing in.</param>
    public Camera(OrderedPair cameraPosition, OrderedPair cameraDirection)
    {
        Positions = new List<OrderedPair>() { cameraPosition };
        CharCode = 'c';
        Type = "Camera";
        Direction = cameraDirection;
    }
    
    /// <summary>
    /// Initialises a new instance of the camera class with no additional parameters.
    /// Specifically for generating a dynamic list in the main menu, not for use in the actual program.
    /// </summary>
    public Camera()
    {
        CharCode = 'c';
        Type = "Camera";
    }
}