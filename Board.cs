namespace MissionControl;

/// <summary>
/// Represents a grid of <see cref="Cell"/> objects used in the program.
/// </summary>
public class Board
{
    private Cell[,] Grid { get; }
    private int Width { get; }
    private int Height { get; }
    private OrderedPair TopLeftCell { get; }
    private OrderedPair BottomRightCell { get; }
    private List<IObstacle>? Obstacles { get; }
    
    /// <summary>
    /// Initializes a new instance of a <see cref="Board"/> class with the specified top-left and bottom-right coordinates.
    /// </summary>
    /// <param name="topLeftCell">The top-left coordinates of the grid represented as an <see cref="OrderedPair"/>.</param>
    /// <param name="bottomRightCell">The bottom-right coordinates of the grid represented as an <see cref="OrderedPair"/>.</param>
    /// <param name="obstaclesList">List of objects that inherit from the interface <see cref="IObstacle"/>.</param>
    public Board(OrderedPair topLeftCell, OrderedPair bottomRightCell, List<IObstacle>? obstaclesList)
    {
        if (bottomRightCell.X >= topLeftCell.X && bottomRightCell.Y >= topLeftCell.Y)
        { 
            Width = Math.Abs(bottomRightCell.X - topLeftCell.X) + 1;
            Height = Math.Abs(bottomRightCell.Y - topLeftCell.Y) + 1;
            TopLeftCell = topLeftCell;
            BottomRightCell = bottomRightCell;
            Obstacles = obstaclesList;
        } else
        {
            throw new ArgumentException( "Invalid map specification." );
        }

        Grid = CreateBoard();
    }

    /// <summary>
    /// Creates the grid by initializing the cells and adding obstacles, if provided.
    /// </summary>
    /// <returns>The two-dimensional array of <see cref="Cell"/> objects representing the grid.</returns>
    private Cell[,] CreateBoard()
    {
        Cell[,] grid = new Cell[Width, Height];
        
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var realX = x + TopLeftCell.X;
                var realY = y + TopLeftCell.Y;
                
                grid[x, y] = new Cell(new OrderedPair(realX, realY));
                
                if (Obstacles == null) continue;
                foreach (var obstacle in Obstacles)
                {
                    if (obstacle.Positions != null && obstacle.Positions.Contains(new OrderedPair(realX, realY)))
                    {
                        obstacle.AddObstacle(ref grid[x,y]);
                    }
                }
            }
        }

        return grid;
    }
    
    /// <summary>
    /// Returns a string representation of the grid, including its width and height.
    /// </summary>
    /// <returns>A string representation of the grid.</returns>
    public override string? ToString()
    {
        var gridString = "";
        
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                gridString += Grid[x, y].CellCharCode;
            }

            gridString += "\n";
        }

        return gridString;
    }
}