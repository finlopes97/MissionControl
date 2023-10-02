using System.Runtime.CompilerServices;

namespace MissionControl;

public class Sensor : IObstacle
{
    public List<OrderedPair>? Positions { get; set; }
    public char CharCode { get; }
    public string Type { get; }
    public static double SensorRange { get; set; }
    
    public override string ToString()
    {
        return $"{CharCode}) Add '{Type}' obstacle.";
    }

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
    
    public Sensor(OrderedPair sensorPosition, double sensorRange)
    {
        Positions = new List<OrderedPair> { sensorPosition };
        CharCode = 's';
        Type = "Sensor";
        SensorRange = sensorRange;
        
        // Fill in an area around the sensor using IsCellInRange
        for (var x = sensorPosition.X - SensorRange; x <= sensorPosition.X + SensorRange; x++)
        {
            for (var y = sensorPosition.Y - SensorRange; y <= sensorPosition.Y + SensorRange; y++)
            {
                var cellPosition = new OrderedPair(Convert.ToInt32(x),Convert.ToInt32(y));
                if (IsCellInRange(sensorPosition, cellPosition))
                {
                    Positions.Add(cellPosition);
                }
            }
        }
    }

    private static bool IsCellInRange(OrderedPair sensorPosition, OrderedPair cellPosition)
    {
        var distanceBetweenCells = Math.Sqrt(
            Math.Pow(cellPosition.X - sensorPosition.X, 2) +
            Math.Pow(cellPosition.Y - sensorPosition.Y, 2));
        
        return distanceBetweenCells <= SensorRange;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Sensor"/> class with no additional parameters.
    /// Specifically for generating a dynamic list in the main menu, not for use in the actual program.
    /// </summary>
    public Sensor()
    {
        CharCode = 's';
        Type = "Sensor";
    }
    
    public void AddObstacle(ref Board board)
    {
        board.Grid[Positions.First().X, Positions.First().Y].CurrentObstacle = this;
    }
}