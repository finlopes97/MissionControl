namespace MissionControl;

/// <summary>
/// Represents a grid of <see cref="Cell"/> objects used in the program.
/// </summary>
public class Board
{
    /// <summary>
    /// Gets the two-dimensional array representing the grid.
    /// </summary>
    public Cell[,] Grid { get; }
    
    public int Rows => Grid.GetLength(0);
    
    public int Cols => Grid.GetLength(1);

    /// <summary>
    /// Initializes a new instance of the <see cref="Board"/> class with the specified top-left and bottom-right coordinates.
    /// </summary>
    /// <param name="topLeft">The top-left coordinates of the grid.</param>
    /// <param name="bottomRight">The bottom-right coordinates of the grid.</param>
    /// <param name="obstaclesList">List of obstacles that exist in the program.</param>
    public Board(OrderedPair topLeft, OrderedPair bottomRight, List<IObstacle>? obstaclesList)
    {
        if(bottomRight.X <= topLeft.X || bottomRight.Y <= topLeft.Y)
            throw new ArgumentException( "Invalid map specification." );
        
        var gridWidth = bottomRight.X - topLeft.X;
        var gridHeight = bottomRight.Y - topLeft.Y;
        
        Grid = new Cell[gridWidth,gridHeight];
        
        for (var y = 0; y < gridHeight; y++)
        {
            for (var x = 0; x < gridWidth; x++)
            {
                Grid[x, y] = new Cell(new OrderedPair(x,y));
                if (obstaclesList == null) continue;
                foreach (var obstacle in obstaclesList)
                {
                    if (obstacle.Positions != null && obstacle.Positions.Contains(new OrderedPair(x, y)))
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
    public override string? ToString()
    {
        string? gridString = null;
        
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Cols; x++)
            {
                if (gridString != null) gridString += Grid[x, y].CellCharCode;
            }

            gridString += "\n";
        }

        return gridString;
    }
}