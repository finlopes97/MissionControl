namespace MissionControl;

/// <summary>
/// Represents a grid of cells used in the program.
/// </summary>
public class Board
{
    /// <summary>
    /// Gets the two-dimensional array of cells representing the grid.
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
    private Coordinate FirstCell { get; }
    
    /// <summary>
    /// Initializes a new board with the specified top-left and bottom-right coordinates.
    /// </summary>
    /// <param name="firstCell">The top-left coordinates of the grid.</param>
    /// <param name="bottomRightCell">The bottom-right coordinates of the grid</param>
    public Board(Coordinate firstCell, Coordinate bottomRightCell)
    {
        // Ensures the grid's bottom-right cell is not above or to the left of the top-left cell.
        if (bottomRightCell.X >= firstCell.X && bottomRightCell.Y >= firstCell.Y)
        { 
            Width = Math.Abs(bottomRightCell.X - firstCell.X) + 1;
            Height = Math.Abs(bottomRightCell.Y - firstCell.Y) + 1;
            FirstCell = firstCell;
        } else
        {
            throw new ArgumentException( "Invalid map specification." );
        }

        Grid = CreateBoard();
    }

    /// <summary>
    /// Creates a board represented as a 2D grid of cells.
    /// </summary>
    /// <returns>A 2D grid of cells representing the board.</returns>
    private Cell[,] CreateBoard()
    {
        Cell[,] grid = new Cell[Width, Height];
        
        // Loop through the grid and create a new cell for each position.
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                int realX = x + FirstCell.X;
                int realY = y + FirstCell.Y;
                
                Coordinate cellPosition = new Coordinate(realX, realY);
                grid[x, y] = new Cell(cellPosition);
                
                if (ObstacleService.Obstacles == null) continue;
                foreach (IObstacle obstacle in ObstacleService.Obstacles)
                {
                    // If the cell intersects with an obstacle, add it to the cell.
                    if (obstacle.IntersectsWithCell(cellPosition))
                    {
                        obstacle.AddObstacle(ref grid[x, y]);
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