using System.Text;

namespace MissionControl;

/// <summary>
/// Though I walk through the valley of the shadow of death,
/// I will fear no evil: for thou art with me; thy rod and thy staff they comfort me.
/// Thou preparest a table before me in the presence of mine enemies:
/// thou anointest my head with oil; my cup runneth over.
/// </summary>
public static class God
{
    private static readonly List<IObstacle> Obstacles = new List<IObstacle>();
    
    /// <summary>
    /// Gets a user-selected option from the console input.
    /// </summary>
    /// <returns>The user-selected option character.</returns>
    public static char GetOption()
    {
        var input = Console.ReadLine();
        if (input == null || input.Length != 1)
            throw new ArgumentException("Invalid input.\n" +
                                        "Enter code:" );
        var lowerInput = input.ToLower();
        return lowerInput[0];
    }
        
    /// <summary>
    /// Checks the board for obstacles and writes a string of safe directions that the agent can move in to the program's output.
    /// </summary>
    public static void ShowSafeDirections()
    {
        var safeDirections = new HashSet<char> {'N', 'S', 'E', 'W'};
        
        Console.WriteLine( "Enter your current location (X,Y):" );
        var position = GetPosition( Console.ReadLine() );

        foreach (var obstacle in Obstacles)
        {
            if (obstacle.Positions == null) continue;

            if (obstacle.IntersectsWithCell(position))
            {
                Console.WriteLine("Agent, your location is compromised. Abort mission.");
                return;
            }

            if (obstacle.IntersectsWithCell(new OrderedPair(position.X, position.Y - 1))) // North
                safeDirections.Remove('N');
            if (obstacle.IntersectsWithCell(new OrderedPair(position.X, position.Y + 1))) // South
                safeDirections.Remove('S');
            if (obstacle.IntersectsWithCell(new OrderedPair(position.X + 1, position.Y))) // East
                safeDirections.Remove('E');
            if (obstacle.IntersectsWithCell(new OrderedPair(position.X - 1, position.Y))) // West
                safeDirections.Remove('W');
        }

        if (safeDirections.Count == 0)
        {
            Console.WriteLine( "You cannot safely move in any direction. Abort mission." );
        }
        else
        {
            Console.WriteLine( "You can safely take any of the following directions: " + string.Join("", safeDirections));
        }
    }

    /// <summary>
    /// Finds a safe path from the agent's current location to the mission objective.
    /// </summary>
    public static void FindSafePath()
    {
        Console.WriteLine( "Enter your current location (X,Y):" );
        Cell startCell = new Cell(GetPosition(Console.ReadLine()));
        Console.WriteLine( "Enter the location of the mission objective (X,Y):" );
        Cell endCell = new Cell(GetPosition(Console.ReadLine()));
        
        AStar(startCell, endCell);
    }

    private static void AStar(Cell startCell, Cell endCell)
    {
        var openSet = new HashSet<Cell>();
        var closedSet = new HashSet<Cell>();
        openSet.Add(startCell);
        
        while (openSet.Count > 0)
        {
            Cell currentCell = openSet.OrderBy(cell => cell.FCost).First();

            openSet.Remove(currentCell);
            closedSet.Add(currentCell);

            if (currentCell.CellPosition.IsEqual(endCell.CellPosition))
            {
                List<Cell> path = ReconstructPath(currentCell);
                string directions = GetDirections(path);
                Console.WriteLine($"The following path will take you to the objective: {directions}");
                return;
            }

            foreach (var neighbour in GetNeighbours(currentCell))
            {
                if (closedSet.Contains(neighbour) || IsObstacle(neighbour))
                    continue;
                
                int tentativeGCost = currentCell.GCost + 1;
                
                if (tentativeGCost < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = tentativeGCost;
                    neighbour.HCost = GetDistance(neighbour, endCell);
                    neighbour.Parent = currentCell;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
        Console.WriteLine( "There is no safe path to the objective." );
    }

    private static List<Cell> GetNeighbours(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();

        int x = cell.CellPosition.X;
        int y = cell.CellPosition.Y;
        
        neighbours.Add(new Cell(new OrderedPair(x, y - 1)));
        neighbours.Add(new Cell(new OrderedPair(x, y + 1)));
        neighbours.Add(new Cell(new OrderedPair(x + 1, y)));
        neighbours.Add(new Cell(new OrderedPair(x - 1, y)));
        
        return neighbours;
    }

    private static List<Cell> ReconstructPath(Cell endCell)
    {
        List<Cell> path = new List<Cell>();
        Cell current = endCell;

        while (current != null)
        {
            path.Add(current);
            current = current.Parent;
        }

        path.Reverse();
        return path;
    }

    private static string GetDirections(List<Cell> path)
    {
        StringBuilder directions = new StringBuilder();
        for (int i = 0; i < path.Count - 1; i++)
        {
            Cell current = path[i];
            Cell next = path[i + 1];

            if (next.CellPosition.Y < current.CellPosition.Y)
                directions.Append("N");
            else if (next.CellPosition.X > current.CellPosition.X)
                directions.Append("E");
            else if (next.CellPosition.Y > current.CellPosition.Y)
                directions.Append("S");
            else if (next.CellPosition.X < current.CellPosition.X)
                directions.Append("W");
        }

        return directions.ToString();
    }
    
    private static bool IsObstacle(Cell cell)
    {
        return cell.CurrentObstacle != null;
    }
    
    private static int GetDistance(Cell a, Cell b)
    {
        return Math.Abs(a.CellPosition.X - b.CellPosition.X) + Math.Abs(a.CellPosition.Y - b.CellPosition.Y);
    }
    
    /// <summary>
    /// Adds a guard to the list of obstacles based on user input.
    /// </summary>
    public static void AddGuard()
    {
        Console.WriteLine( "Enter the guard's location (X,Y):" );
        var guardPosition = GetPosition( Console.ReadLine() );
        Obstacles.Add(new Guard(guardPosition));
    }

    /// <summary>
    /// Adds a fence to the list of obstacles based on user input.
    /// </summary>
    public static void AddFence()
    {
        Console.WriteLine( "Enter the location where the fence starts (X,Y):" );
        var fenceStartingPosition = GetPosition( Console.ReadLine() );
        Console.WriteLine( "Enter the location where the fence ends (X,Y):" );
        var fenceEndingPosition = GetPosition( Console.ReadLine() );
        Obstacles.Add(new Fence(fenceStartingPosition, fenceEndingPosition));
    }

    /// <summary>
    /// Adds a sensor to the list of obstacles based on user input.
    /// </summary>
    public static void AddSensor()
    {
        Console.WriteLine( "Enter the sensor's location (X,Y):" );
        var sensorPosition = GetPosition( Console.ReadLine() ); 
        Console.WriteLine( "Enter the sensor's range (in klicks):" );
        var sensorRange = GetRange(Console.ReadLine());
        var sensor = new Sensor(sensorPosition, sensorRange);
        Obstacles.Add(sensor);
    }

    public static void AddCamera()
    {
        Console.WriteLine( "Enter the camera's location (X,Y):" );
        var cameraPosition = GetPosition( Console.ReadLine() );
        Console.WriteLine( "Enter the direction the camera is facing (n, s, e or w):" );
        var cameraDirection = GetDirection( Console.ReadLine() );
        Obstacles.Add(new Camera(cameraPosition, cameraDirection));
    }

    public static void AddSpotlight()
    {
        Console.WriteLine( "Enter the spotlight's location (X,Y):" );
        var spotlightPosition = GetPosition(Console.ReadLine());
        Console.WriteLine( "Enter the direction the spotlight is facing in (n, s, e or w):" );
        var spotlightDirection = GetDirection( Console.ReadLine() );
        Console.WriteLine( "Enter the spotlight's range (in klicks):" );
        var spotlightRange = GetRange( Console.ReadLine() );
        Obstacles.Add(new Spotlight(spotlightPosition, spotlightDirection, spotlightRange));
    }

    /// <summary>
    /// Creates a new board based on user input and displays it.
    /// </summary>
    public static void CreateBoard()
    {
        Console.WriteLine( "Enter the location of the top-left cell of the map (X,Y):" );
        var mapTopLeft = GetPosition( Console.ReadLine() );
        Console.WriteLine( "Enter the location of the bottom-right cell of the map (X,Y):" );
        var mapBottomRight = GetPosition( Console.ReadLine() );
        var board = new Board(mapTopLeft, mapBottomRight, Obstacles);
        Console.WriteLine( board.ToString() );
    }

    
    /// <summary>
    /// Parses a string representation of position coordinates and returns an OrderedPair.
    /// </summary>
    /// <param name="posString">A string containing two integer values separated by a comma.</param>
    /// <returns>An OrderedPair representing the parsed position.</returns>
    /// <exception cref="ArgumentException">Thrown if the input string is invalid.</exception>
    /// <exception cref="FormatException">Thrown if the input does not contain valid integer values.</exception>
    private static OrderedPair GetPosition(string? posString)
    {
        if (posString == null)
            throw new ArgumentException("Invalid input.");
        
        if (!posString.Contains(','))
            throw new ArgumentException( "Invalid input." );
        
        string[] pos = posString.Split(',');
        if (pos.Length != 2)
            throw new ArgumentException("Invalid input.");
        
        if (int.TryParse(pos[0], out int x) && int.TryParse(pos[1], out int y))
            return new OrderedPair(x,y);
        else
            throw new FormatException("Invalid input.");
    }
    
    /// <summary>
    /// Parses a string representation of a double and returns the value.
    /// </summary>
    /// <param name="doubleString">A string containing a valid double value.</param>
    /// <returns>A double value representing the range of an obstacle.</returns>
    /// <exception cref="ArgumentException">Thrown if the input string is invalid or if the value is less than or equal to zero.</exception>
    private static double GetRange(string? doubleString)
    {
        if (doubleString == null)
            throw new ArgumentException( "Invalid input." );

        if (double.TryParse(doubleString, out var result))
            if (result <= 0)
                throw new ArgumentException( "Invalid input." );
            else
                return result;
        else
            throw new ArgumentException( "Invalid input." );
    }
    
    /// <summary>
    /// Parses a direction string and returns the corresponding OrderedPair representing the direction.
    /// </summary>
    /// <param name="directionString">A string representing a cardinal direction ("n", "s", "e", or "w").</param>
    /// <returns>An OrderedPair representing the direction.</returns>
    /// <exception cref="ArgumentException">Thrown when an invalid or empty input is provided.</exception>
    private static OrderedPair GetDirection(string? directionString)
    {
        if (directionString == "")
            throw new ArgumentException("Invalid input.");
        directionString = directionString?.ToLower();

        OrderedPair direction;
        
        switch (directionString)
        {
            case "n":
                direction = new OrderedPair(0, -1);
                break;
            case "s":
                direction = new OrderedPair(0, 1);
                break;
            case "e":
                direction = new OrderedPair(1, 0);
                break;
            case "w":
                direction = new OrderedPair(-1, 0);
                break;
            default:
                throw new ArgumentException( "Invalid input." );
        }

        return direction;
    }
    
    /// <summary>
    /// Safely exits the program.
    /// </summary>
    public static void ExitProgram()
    {
        Environment.Exit(0);
    }
}