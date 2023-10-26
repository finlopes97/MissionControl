namespace MissionControl;

/// <summary>
/// Contains information about a cell in the grid including its position, obstacle and cost in the pathfinding algorithm.
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
            // If the obstacle is not null, set the cell's character code to the obstacle's character code.
            if (value != null) CellCharCode = value.CharCode;
        }
    }
    
    /// <summary>
    /// Gets the coordinates of this square as an ordered pair.
    /// </summary>
    public Coordinate CellPosition { get; } 
    
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
    
    /// <summary>
    /// Determines whether the specified object is equal to the current Cell object.
    /// </summary>
    /// <param name="obj">The object to compare with the current Cell object.</param>
    /// <returns>true if the specified object is equal to the current Cell object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is Cell otherCell)
        {
            return CellPosition.Equals(otherCell.CellPosition);
        }
        return false;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current Cell object.</returns>
    public override int GetHashCode()
    {
        return this.CellPosition.GetHashCode();
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class with the specified obstacle and coordinates.
    /// </summary>
    /// <param name="coord">The coordinates of this square as an ordered pair.</param>
    /// <param name="obstacle">The obstacle currently occupying this square (optional).</param>
    public Cell(Coordinate coord, IObstacle? obstacle = null)
    {
        CellPosition = coord;
        CurrentObstacle = obstacle;
        Parent = null;
        CellCharCode = '.';
    }
}