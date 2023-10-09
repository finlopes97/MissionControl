namespace MissionControl;

public class Spotlight : IObstacle
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
    public int Priority => 4;

    private OrderedPair SpotlightDirection { get; }
    
    private double SpotlightRange { get; }

    private const int SpotlightDiameter = 2;

    /// <summary>
    /// Adds an obstacle to the grid.
    /// </summary>
    /// <param name="board">The board to add the obstacle to.</param>
    public void AddObstacle(ref Board board)
    {
        if (Positions != null) board.Grid[Positions.First().X, Positions.First().Y].CurrentObstacle = this;
    }

    public Spotlight(OrderedPair spotlightPosition, OrderedPair spotlightDirection, double spotlightRange)
    {
        Positions = new List<OrderedPair>() { spotlightPosition };
        CharCode = 'l';
        Type = "Spotlight";

        SpotlightDirection = spotlightDirection;
        SpotlightRange = spotlightRange;
        
        GenerateSpotlightArea();
    }

    private void GenerateSpotlightArea()
    {
        var spotlightRangeRounded = Convert.ToInt32(SpotlightRange);

        if (Positions != null)
        {
            var endPoint = new OrderedPair(
                Positions[0].X + SpotlightDirection.X * spotlightRangeRounded,
                Positions[0].Y + SpotlightDirection.Y * spotlightRangeRounded);
            
            Console.WriteLine( $"The direction the spotlight is facing in is: {SpotlightDirection.ToString()}." );
            Console.WriteLine( $"Origin of the spotlight is: {Positions[0].ToString()}, and the end point of the spotlight's range is {endPoint.ToString()}." );

            for (var x = endPoint.X - SpotlightDiameter; x <= endPoint.X + SpotlightDiameter; x++)
            {
                for (var y = endPoint.Y - SpotlightDiameter; y <= endPoint.Y + SpotlightDiameter; y++)
                {
                    var cellPosition = new OrderedPair(x, y);
                    if (CellInRange(endPoint, cellPosition))
                    {
                        Positions.Add(cellPosition);
                    }
                }
            }
        }
    }

    private static bool CellInRange(OrderedPair origin, OrderedPair cellPosition)
    {
        var distanceBetweenCells = Math.Sqrt(
            Math.Pow(cellPosition.X - origin.X, 2) +
            Math.Pow(cellPosition.Y - origin.Y, 2));

        return distanceBetweenCells <= SpotlightDiameter;
    }
        
    /// <summary>
    /// Initialises a new instance of the <see cref="Spotlight"/> class with no additional parameters.
    /// Specifically for generating a dynamic list in the main menu, not for use in the actual program.
    /// </summary>
    public Spotlight()
    {
        CharCode = 'l';
        Type = "Spotlight";
    }
    
    /// <summary>
    /// Builds a string for use in the main menu
    /// </summary>
    /// <returns>A string to be used when printing the main menu of the program.</returns>
    public override string ToString()
    {
        return $"{CharCode}) Add '{Type}' obstacle.";
    }
}