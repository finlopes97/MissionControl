namespace MissionControl;

public class Sensor : IObstacle
{
    public OrderedPair Position { get; }
    public char CharCode { get; }
    public string Type { get; }
    public double Range { get; set; }
    
    public override string ToString()
    {
        return $"{CharCode}) Add '{Type}' obstacle.";
    }
    
    public Sensor(OrderedPair pos, double range)
    {
        Position = pos;
        CharCode = 's';
        Type = "Sensor";
        Range = range;
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
        board.Grid[Position.X, Position.Y].CurrentObstacle = this;
    }
}