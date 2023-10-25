namespace MissionControl;

/// <summary>
/// Represents a grid of <see cref="Cell"/> objects used in the program.
/// </summary>
public class Board
{
    /// <summary>
    /// Gets the two-dimensional array of <see cref="Cell"/> objects representing the grid.
    /// </summary>
    private Cell[,] Grid { get; }

    /// <summary>
    /// Gets the width of the grid.
    /// </summary>
    private int Width { get; }

    /// <summary>
    /// Gets the height of the grid.
    /// </summary>
    private int Height { get; }

    /// <summary>
    /// Gets the first cell in the grid, which is assigned as the top-left cell.
    /// </summary>
    private OrderedPair FirstCell { get; }

    /// <summary>
    /// Gets a list of objects that inherit from the interface <see cref="IObstacle"/>.
    /// </summary>
    private List<IObstacle>? Obstacles { get; }
    
    /// <summary>
    /// Initializes a new instance of a <see cref="Board"/> class with the specified top-left and bottom-right coordinates.
    /// </summary>
    /// <param name="firstCell">The top-left coordinates of the grid represented as an <see cref="OrderedPair"/>.</param>
    /// <param name="bottomRightCell">The bottom-right coordinates of the grid represented as an <see cref="OrderedPair"/>.</param>
    /// <param name="obstaclesList">List of obstacles that inherit from the interface <see cref="IObstacle"/>.</param>
    public Board(OrderedPair firstCell, OrderedPair bottomRightCell, List<IObstacle>? obstaclesList)
    {
        if (bottomRightCell.X >= firstCell.X && bottomRightCell.Y >= firstCell.Y)
        { 
            Width = Math.Abs(bottomRightCell.X - firstCell.X) + 1;
            Height = Math.Abs(bottomRightCell.Y - firstCell.Y) + 1;
            FirstCell = firstCell;
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
        
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                int realX = x + FirstCell.X;
                int realY = y + FirstCell.Y;
                
                OrderedPair cellPosition = new OrderedPair(realX, realY);
                grid[x, y] = new Cell(cellPosition);
                
                if (Obstacles == null) continue;
                foreach (IObstacle obstacle in Obstacles)
                {
                    if (obstacle is Camera camera && camera.InCone(camera.OriginPosition(), cellPosition))
                    {
                        camera.AddObstacle(ref grid[x,y]);
                    }
                    
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
        string? gridString = null;
        
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                gridString += Grid[x, y].CellCharCode;
            }

            gridString += "\n";
        }

        return gridString;
    }
}