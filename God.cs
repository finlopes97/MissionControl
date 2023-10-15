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
        var safeDirections = new List<char> {'N', 'S', 'E', 'W'};
        string? stringOfSafeDirections = null;
        
        Console.WriteLine( "Enter your current location (X,Y):" );
        var position = GetPosition( Console.ReadLine() );
        
        if (Obstacles.Count != 0)
        {
            foreach (var obstacle in Obstacles)
            {
                if (obstacle.Positions == null) continue;
                
                foreach (var obstaclePosition in obstacle.Positions)
                {
                    if (position.IsEqual(obstaclePosition))
                    {
                        Console.WriteLine("Agent, your location is compromised. Abort mission.");
                        return;
                    }
    
                    if (position.X == obstaclePosition.X)
                        safeDirections.Remove(position.Y > obstaclePosition.Y ? 'N' : 'S');
                    else if (position.Y == obstaclePosition.Y)
                        safeDirections.Remove(position.X < obstaclePosition.X ? 'E' : 'W');
                }
            }
        }

        foreach (var direction in safeDirections)
        {
            stringOfSafeDirections += direction;
        }
        
        if (stringOfSafeDirections == null) { Console.WriteLine( "You cannot safely move in any direction. Abort mission." ); }
        else { Console.WriteLine( "You can safely take any of the following directions: " + stringOfSafeDirections ); }
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