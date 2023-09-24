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

    public void AddObstacle(ref IObstacle obstacle)
    {
        // Add an obstacle to the board using polymorphism, this needs to be extensible for obstacles
        // that take up one square (e.g. guards), can take up multiple squares (e.g. fence), and have a 
        // field of view (e.g. cameras)
    }

    /// <summary>
    /// Returns a string representation of the grid, including its width and height.
    /// </summary>
    /// <returns>A string representation of the grid.</returns>
    public override string ToString()
    {
        int rows = Grid.GetLength(0);
        int cols = Grid.GetLength(1);
        string gridString = $"Welcome to Outer Heaven, Solid Snake\nGrid width = {cols}, grid height = {rows}\n";
        
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