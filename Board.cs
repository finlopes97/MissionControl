namespace MissionControl;

/// <summary>
/// Represents a grid used in the program.
/// </summary>
public class Board
{
    /// <summary>
    /// Gets the two-dimensional array representing the grid.
    /// </summary>
    private Square[,] Grid { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Board"/> class with the specified top-left and bottom-right coordinates.
    /// </summary>
    /// <param name="topLeft">The top-left coordinates of the grid.</param>
    /// <param name="bottomRight">The bottom-right coordinates of the grid.</param>
    public Board(OrderedPair topLeft, OrderedPair bottomRight)
    {
        int gridWidth = bottomRight.X - topLeft.X;
        int gridHeight = bottomRight.Y - topLeft.Y;
        
        Grid = new Square[gridWidth,gridHeight];
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Grid[x, y] = new Square(new OrderedPair(x,y));
            }
        }
    }

    /// <summary>
    /// Adds an obstacle to the grid.
    /// </summary>
    /// <param name="obstacle">The obstacle to be added to the grid.</param>
    public void AddObstacle(ref IObstacle obstacle)
    {
        int x = obstacle.Position.X;
        int y = obstacle.Position.Y;
        
        Grid[x, y].CurrentObstacle = obstacle;
    }

    /// <summary>
    /// Returns a string representation of the grid, including its width and height.
    /// </summary>
    /// <returns>A string representation of the grid.</returns>
    public override string ToString()
    {
        int rows = Grid.GetLength(0);
        int cols = Grid.GetLength(1);
        string gridString = $"Grid width = {cols}, grid height = {rows}\n";
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                gridString += Grid[i, j].SquareCharCode;
            }

            gridString += "\n";
        }

        return gridString;
    }
}