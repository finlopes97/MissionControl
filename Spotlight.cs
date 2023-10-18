namespace MissionControl;

public class Spotlight : IObstacle
{
    /// <summary>
    /// A list of positions that the obstacle occupies on the grid.
    /// </summary>
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

    /// <summary>
    /// The direction that the spotlight is facing in, represented as an ordered pair vector.
    /// </summary>
    private OrderedPair SpotlightDirection { get; }
    
    /// <summary>
    /// The offset between the spotlight's endPoint point and the end of its range.
    /// </summary>
    private double SpotlightRange { get; }

    /// <summary>
    /// The diameter of the spotlight.
    /// </summary>
    private const int SpotlightDiameter = 2;

    /// <summary>
    /// Adds an obstacle to the grid.
    /// </summary>
    /// <param name="cell">The <see cref="Cell"/> to add the obstacle to.</param>
    public void AddObstacle(ref Cell cell)
    {
        if (Positions != null && Positions.Contains(cell.CellPosition))
        {
            cell.CurrentObstacle = this;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Spotlight"/> class with a specified position, direction and range.
    /// </summary>
    /// <param name="spotlightPosition">An <see cref="OrderedPair"/> endPoint position of the spotlight.</param>
    /// <param name="spotlightDirection">An <see cref="OrderedPair"/> that represents the direction that the spotlight is facing in.</param>
    /// <param name="spotlightRange">A <see cref="System.Double"/> the offset between the spotlight's endPoint point and the end of its range.</param>
    public Spotlight(OrderedPair spotlightPosition, OrderedPair spotlightDirection, double spotlightRange)
    {
        Positions = new List<OrderedPair>() { spotlightPosition };
        CharCode = 'l';
        Type = "Spotlight";

        SpotlightDirection = spotlightDirection;
        SpotlightRange = spotlightRange;
        
        GenerateSpotlightArea();
    }
    
    /// <summary>
    /// Gets the origin position of the <see cref="Spotlight"/> as an <see cref="OrderedPair"/>.
    /// </summary>
    /// <returns>Returns an <see cref="OrderedPair"/> representing the origin.</returns>
    /// <exception cref="Exception">Throws an exception if object has no positions.</exception>
    public OrderedPair OriginPosition()
    {
        if (Positions == null) 
            throw new Exception( "Spotlight object has no positions." );
        else 
            return Positions.First();
    }


    /// <summary>
    /// Generates the area that the spotlight covers and adds it to the list of positions that the obstacle occupies.
    /// </summary>
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

    /// <summary>
    /// Checks if a cell is within the spotlight's range.
    /// </summary>
    /// <param name="endPoint">An <see cref="OrderedPair"/> that represents the end point of the spotlight's range.</param>
    /// <param name="cellPosition">An <see cref="OrderedPair"/> that represents the cell to compare against the endPoint</param>
    /// <returns></returns>
    private static bool CellInRange(OrderedPair endPoint, OrderedPair cellPosition)
    {
        var distanceBetweenCells = Math.Sqrt(
            Math.Pow(cellPosition.X - endPoint.X, 2) +
            Math.Pow(cellPosition.Y - endPoint.Y, 2));

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