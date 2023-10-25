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
            if (value != null) CellCharCode = value.CharCode;
        }
    }
    
    /// <summary>
    /// Gets the coordinates of this square as an ordered pair.
    /// </summary>
    public OrderedPair CellPosition { get; } 
    
    /// <summary>
    /// Gets the default character for this square before being occupied by an obstacle.
    /// </summary>
    public char CellCharCode { get; private set; } 
    
    /// <summary>
    /// The cost of moving to this cell.
    /// </summary>
    public int GCost { get; set; }
    
    /// <summary>
    /// The estimated cost of moving from this cell to the goal.
    /// </summary>
    public int HCost { get; set; }
    /// <summary>
    /// The total cost of moving to this cell and then to the goal.
    /// </summary>
    public int FCost => GCost + HCost;
    
    /// <summary>
    /// The parent cell of this cell.
    /// </summary>
    public Cell? Parent { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is Cell otherCell)
        {
            return this.CellPosition.Equals(otherCell.CellPosition);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return this.CellPosition.GetHashCode();
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class with the specified obstacle and coordinates.
    /// </summary>
    /// <param name="coord">The coordinates of this square as an ordered pair.</param>
    /// <param name="obstacle">The obstacle currently occupying this square (optional).</param>
    public Cell(OrderedPair coord, IObstacle? obstacle = null)
    {
        CellPosition = coord;
        CurrentObstacle = obstacle;
        Parent = null;
        CellCharCode = '.';
    }
}