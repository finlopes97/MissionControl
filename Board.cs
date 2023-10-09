namespace MissionControl;

/// <summary>
/// Represents a grid used in the program.
/// </summary>
public class Board
{
    /// <summary>
    /// Gets the two-dimensional array representing the grid.
    /// </summary>
    public Square[,] Grid { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Board"/> class with the specified top-left and bottom-right coordinates.
    /// </summary>
    /// <param name="topLeft">The top-left coordinates of the grid.</param>
    /// <param name="bottomRight">The bottom-right coordinates of the grid.</param>
    /// <param name="obstaclesList">List of obstacles that exist in the program.</param>
    public Board(OrderedPair topLeft, OrderedPair bottomRight, List<IObstacle> obstaclesList)
    {
        if(bottomRight.X <= topLeft.X || bottomRight.Y <= topLeft.Y)
            throw new ArgumentException( "Invalid map specification." );
        
        var gridWidth = bottomRight.X - topLeft.X;
        var gridHeight = bottomRight.Y - topLeft.Y;
        
        Grid = new Square[gridWidth,gridHeight];
        
        for (var y = 0; y < gridHeight; y++)
        {
            for (var x = 0; x < gridWidth; x++)
            {
                Grid[x, y] = new Square(new OrderedPair(x,y));
                foreach (var obstacle in obstaclesList)
                {
                    if (obstacle.Positions != null && obstacle.Positions.Contains(new OrderedPair(x,y)))
                    {
                        Grid[x, y].CurrentObstacle = obstacle;
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Returns a string representation of the grid, including its width and height.
    /// </summary>
    /// <returns>A string representation of the grid.</returns>
    public override string ToString()
    {
        var rows = Grid.GetLength(0);
        var cols = Grid.GetLength(1);
        string gridString = $"Grid width = {cols}, grid height = {rows}\n";
        
        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < cols; x++)
            {
                gridString += Grid[x, y].SquareCharCode;
            }

            gridString += "\n";
        }

        return gridString;
    }
}