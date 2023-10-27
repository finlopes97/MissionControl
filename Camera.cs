namespace MissionControl;

/// <summary>
/// Represents a camera obstacle in the mission control system.
/// Has an origin position, direction and indefinite range in a 45 degree cone of vision.
/// </summary>
public class Camera : IObstacle
{
    /// <summary>
    /// Gets the positions of the camera as a list of ordered pairs.
    /// </summary>
    public List<Coordinate>? Positions { get; set; }
    
    /// <summary>
    /// Cameras have no movement cost as they are not traversable.
    /// </summary>
    public int MovementCost => 0;
    
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
    private Coordinate Direction { get; }


    /// <summary>
    /// Gets the origin position of the camera.
    /// </summary>
    /// <returns>Returns an ordered pair representing the origin.</returns>
    /// <exception cref="Exception">Throws an exception if object has no positions.</exception>
    private Coordinate OriginPosition()
    {
        if (Positions == null) 
            throw new Exception( "Camera object has no positions." );
        else 
            return Positions.First();
    }
    
    /// <summary>
    /// Adds the camera as an obstacle to the specified cell.
    /// </summary>
    /// <param name="cell">The cell to which the camera is added.</param>
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
    public bool IntersectsWithCell(Coordinate cellToCheck)
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
    private bool InCone(Coordinate cameraPosition, Coordinate cellPosition)
    {
        // Calculate the vector from the camera to the cell.
        Coordinate vectorToCell = new Coordinate(
            cellPosition.X - cameraPosition.X, 
            cellPosition.Y - cameraPosition.Y);

        // Calculate the angle between the X-axis and the vector to the cell.
        double angleToCell = Math.Atan2(vectorToCell.Y, vectorToCell.X);
        // Calculate the angle between the X-axis and the camera's direction.
        double angleToDirection = Math.Atan2(Direction.Y, Direction.X);
        
        // Calculate the difference between the two angles.
        double angleDifference = angleToCell - angleToDirection;

        // Normalise the angle difference to be between -π and π.
        while (angleDifference > Math.PI) angleDifference -= 2 * Math.PI;
        while (angleDifference <= -Math.PI) angleDifference += 2 * Math.PI;

        // Define the half angle of the camera's cone of vision (45 degrees in radians).
        const double halfConeAngle = Math.PI / 4;
        
        // Check if the absolute difference between the angles is less than or equal to the half-angle of the cone.
        // If it is, the cell is within the camera's cone of vision.
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
    public Camera(Coordinate cameraPosition, Coordinate cameraDirection)
    {
        Positions = new List<Coordinate>() { cameraPosition };
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