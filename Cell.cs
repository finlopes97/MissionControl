namespace MissionControl;

/// <summary>
/// Represents a square in a grid-based system.
/// </summary>
public class Cell
{
    /// <summary>
    /// Gets or sets the obstacle currently occupying this square.
    /// </summary>
    private IObstacle? _currentObstacle;
    public IObstacle? CurrentObstacle
    {
        get => _currentObstacle;
        set
        {
            _currentObstacle = value;
            if (value != null) SquareCharCode = value.CharCode;
        }
    }
    
    /// <summary>
    /// Gets the coordinates of this square as an ordered pair.
    /// </summary>
    public OrderedPair ThisCoord { get; } 
    
    /// <summary>
    /// Gets the default character for this square before being occupied by an obstacle.
    /// </summary>
    public char SquareCharCode { get; private set; } 
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class with the specified obstacle and coordinates.
    /// </summary>
    /// <param name="coord">The coordinates of this square as an ordered pair.</param>
    /// <param name="obstacle">The obstacle currently occupying this square (optional).</param>
    public Cell(OrderedPair coord, IObstacle? obstacle = null)
    {
        ThisCoord = coord;
        CurrentObstacle = obstacle;
        SquareCharCode = '.';
    }
}