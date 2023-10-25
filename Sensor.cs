namespace MissionControl;

/// <summary>
/// Represents a sensor used in a mission control system.
/// Sensors can detect obstacles within a specified range.
/// </summary>
public class Sensor : IObstacle
{
    /// <summary>
    /// Gets or sets the list of positions associated with this sensor.
    /// </summary>
    public List<OrderedPair>? Positions { get; set; }
    
    /// <summary>
    /// Gets the character code representing this sensor.
    /// </summary>
    public char CharCode { get; }
    
    /// <summary>
    /// Gets the type (name) of this obstacle.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Determines the list position in which this obstacle appears in the main menu.
    /// </summary>
    public int Priority => 2;

    /// <summary>
    /// Gets or sets the static sensor range for all instances of the sensor class.
    /// </summary>
    private static double SensorRange { get; set; }
    
    /// <summary>
    /// Adds the sensor as an obstacle to the specified cell.
    /// </summary>
    /// <param name="cell">The <see cref="Cell"/> to which the sensor is added.</param>
    public void AddObstacle(ref Cell cell)
    {
        if(Positions != null && Positions.Contains(cell.CellPosition))
        {
            cell.CurrentObstacle = this;
        }
    }
    
    /// <summary>
    /// Checks if a cell intersects with the sensor or if it is in the sensor's range.
    /// </summary>
    /// <param name="cellToCheck">The cell to check for intersection.</param>
    /// <returns>True if the sensor intersects with the cell, otherwise false.</returns>
    public bool IntersectsWithCell(OrderedPair cellToCheck)
    {
        return Positions != null && Positions.Contains(cellToCheck);
    }
    
    /// <summary>
    /// Gets the origin position of the sensor.
    /// </summary>
    /// <returns>Returns the origin position of the sensor.</returns>
    /// <exception cref="Exception">Throws an exception if object has no positions.</exception>
    public OrderedPair OriginPosition()
    {
        return (Positions ?? throw new InvalidOperationException( "Sensor has no positions." )).First();
    }
    
    /// <summary>
    /// Determines if a given cell is within the sensor's range.
    /// </summary>
    /// <param name="sensorPosition">The sensor's position.</param>
    /// <param name="cellPosition">The position of the cell to check.</param>
    /// <returns>True if the cell is within range; otherwise, false.</returns>
    private static bool CellInRange(OrderedPair sensorPosition, OrderedPair cellPosition)
    {
        double distanceBetweenCells = Math.Sqrt(
            Math.Pow(cellPosition.X - sensorPosition.X, 2) +
            Math.Pow(cellPosition.Y - sensorPosition.Y, 2));
        
        return distanceBetweenCells <= SensorRange;
    }
        
    /// <summary>
    /// Returns a string representation of the sensor.
    /// </summary>
    /// <returns>A string in the format "{CharCode}) Add '{Type}' obstacle."</returns>
    public override string ToString()
    {
        return $"{CharCode}) Add '{Type}' obstacle.";
    }
    
    /// <summary>
    /// Initializes a new instance of the sensor class with specified position and range.
    /// </summary>
    /// <param name="sensorPosition">The initial position of the sensor.</param>
    /// <param name="sensorRange">The range within which the sensor can detect obstacles.</param>
    public Sensor(OrderedPair sensorPosition, double sensorRange)
    {
        Positions = new List<OrderedPair> { sensorPosition };
        CharCode = 's';
        Type = "Sensor";
        SensorRange = sensorRange;
        
        for (int x = sensorPosition.X - (int)SensorRange; x <= sensorPosition.X + (int)SensorRange; x++)
        {
            for (int y = sensorPosition.Y - (int)SensorRange; y <= sensorPosition.Y + (int)SensorRange; y++)
            {
                OrderedPair cellPosition = new OrderedPair(x,y);
                if (CellInRange(sensorPosition, cellPosition))
                {
                    Positions.Add(cellPosition);
                }
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the sensor class with default values.
    /// </summary>
    public Sensor()
    {
        CharCode = 's';
        Type = "Sensor";
    }
}