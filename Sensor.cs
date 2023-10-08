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
    /// Gets or sets the static sensor range for all instances of the sensor class.
    /// </summary>
    private static double SensorRange { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Sensor"/> class with specified position and range.
    /// </summary>
    /// <param name="sensorPosition">The initial position of the sensor.</param>
    /// <param name="sensorRange">The range within which the sensor can detect obstacles.</param>
    public Sensor(OrderedPair sensorPosition, double sensorRange)
    {
        Positions = new List<OrderedPair> { sensorPosition };
        CharCode = 's';
        Type = "Sensor";
        SensorRange = sensorRange;
        
        for (var x = sensorPosition.X - SensorRange; x <= sensorPosition.X + SensorRange; x++)
        {
            for (var y = sensorPosition.Y - SensorRange; y <= sensorPosition.Y + SensorRange; y++)
            {
                throw new NotImplementedException( "Rounding seems to break this, fix later." );
                var cellPosition = new OrderedPair(Convert.ToInt32(x),Convert.ToInt32(y));
                if (CellInRange(sensorPosition, cellPosition))
                {
                    Positions.Add(cellPosition);
                }
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sensor"/> class with default values.
    /// </summary>
    public Sensor()
    {
        CharCode = 's';
        Type = "Sensor";
    }
    
    /// <summary>
    /// Adds the sensor as an obstacle to the specified board.
    /// </summary>
    /// <param name="board">The board to which the sensor is added.</param>
    public void AddObstacle(ref Board board)
    {
        if (Positions != null) board.Grid[Positions.First().X, Positions.First().Y].CurrentObstacle = this;
    }
    
    /// <summary>
    /// Determines if a given cell is within the sensor's range.
    /// </summary>
    /// <param name="sensorPosition">The sensor's position.</param>
    /// <param name="cellPosition">The position of the cell to check.</param>
    /// <returns>True if the cell is within range; otherwise, false.</returns>
    private static bool CellInRange(OrderedPair sensorPosition, OrderedPair cellPosition)
    {
        var distanceBetweenCells = Math.Sqrt(
            Math.Pow(cellPosition.X - sensorPosition.X, 2) +
            Math.Pow(cellPosition.Y - sensorPosition.Y, 2));
        
        return distanceBetweenCells <= SensorRange;
    }
    
    /// <summary>
    /// Generates a list of positions within the sensor's range.
    /// </summary>
    /// <returns>A string containing a list of positions, one per line.</returns>
    public string ListOfPositions()
    {
        var output = "";
        if (Positions == null) return "Positions is null";
        foreach (var position in Positions)
        {
            output += position.ToString() + '\n';
        }
        return output;
    }
    
    /// <summary>
    /// Returns a string representation of the sensor.
    /// </summary>
    /// <returns>A string in the format "{CharCode}) Add '{Type}' obstacle."</returns>
    public override string ToString()
    {
        return $"{CharCode}) Add '{Type}' obstacle.";
    }
}