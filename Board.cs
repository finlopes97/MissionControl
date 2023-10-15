namespace MissionControl;

/// <summary>
/// Represents a grid of <see cref="Cell"/> objects used in the program.
/// </summary>
public class Board
{
    /// <summary>
    /// Gets the two-dimensional array representing the grid.
    /// </summary>
    private Cell[,] Grid { get; }
    
    private int Rows => Grid.GetLength(0);
    
    private int Cols => Grid.GetLength(1);

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
        
        var gridWidth = Math.Abs(bottomRight.X - topLeft.X) + 1;
        var gridHeight = Math.Abs(bottomRight.Y - topLeft.Y) + 1;
        
        Grid = new Cell[gridHeight, gridHeight];
        
        for (var y = 0; y < gridHeight; y++)
        {
            for (var x = 0; x < gridWidth; x++)
            {
                var realX = x + topLeft.X;
                var realY = y + topLeft.Y;
                
                Grid[x, y] = new Cell(new OrderedPair(realX, realY));
                
                if (obstaclesList == null) continue;
                foreach (var obstacle in obstaclesList)
                {
                    if (obstacle.Positions != null && obstacle.Positions.Contains(new OrderedPair(realX, realY)))
                    {
                        obstacle.AddObstacle(ref Grid[x,y]);
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