namespace MissionControl;

/// <summary>
/// The helper class contains methods that are used by the program to get user input and perform operations.
/// </summary>
public static class Helper
{
    /// <summary>
    /// Gets a user-selected option from the console input.
    /// </summary>
    /// <returns>The user-selected option character.</returns>
    public static char GetOption()
    {
        string? input = Console.ReadLine();
        if (input == null || input.Length != 1)
            throw new ArgumentException("Invalid input.\n" +
                                        "Enter code:" );
        string lowerInput = input.ToLower();
        return lowerInput[0];
    }
        
    /// <summary>
    /// Creates a new board based on user input and displays it.
    /// </summary>
    public static void CreateBoard()
    {
        Console.WriteLine( "Enter the location of the top-left cell of the map (X,Y):" );
        Coordinate mapTopLeft = GetPosition( Console.ReadLine() );
        Console.WriteLine( "Enter the location of the bottom-right cell of the map (X,Y):" );
        Coordinate mapBottomRight = GetPosition( Console.ReadLine() );
        Board board = new Board(mapTopLeft, mapBottomRight);
        Console.WriteLine( board.ToString() );
    }

    
    /// <summary>
    /// Parses a string representation of position coordinates and returns an Coordinate.
    /// </summary>
    /// <param name="posString">A string containing two integer values separated by a comma.</param>
    /// <returns>An Coordinate representing the parsed position.</returns>
    /// <exception cref="ArgumentException">Thrown if the input string is invalid.</exception>
    /// <exception cref="FormatException">Thrown if the input does not contain valid integer values.</exception>
    public static Coordinate GetPosition(string? posString)
    {
        if (posString == null)
            throw new ArgumentException("Invalid input.");
        
        if (!posString.Contains(','))
            throw new ArgumentException( "Invalid input." );
        
        string[] pos = posString.Split(',');
        if (pos.Length != 2)
            throw new ArgumentException("Invalid input.");
        
        if (int.TryParse(pos[0], out int x) && int.TryParse(pos[1], out int y))
            return new Coordinate(x,y);
        else
            throw new FormatException("Invalid input.");
    }
    
    /// <summary>
    /// Parses a string representation of a double and returns the value.
    /// </summary>
    /// <param name="doubleString">A string containing a valid double value.</param>
    /// <returns>A double value representing the range of an obstacle.</returns>
    /// <exception cref="ArgumentException">Thrown if the input string is invalid or if the value is less than or equal to zero.</exception>
    public static double GetDistance(string? doubleString)
    {
        if (doubleString == null)
            throw new ArgumentException( "Invalid input." );

        if (double.TryParse(doubleString, out double result))
            if (result <= 0)
                throw new ArgumentException( "Invalid input." );
            else
                return result;
        else
            throw new ArgumentException( "Invalid input." );
    }
    
    /// <summary>
    /// Parses a direction string and returns the corresponding Coordinate representing the direction.
    /// </summary>
    /// <param name="directionString">A string representing a cardinal direction ("n", "s", "e", or "w").</param>
    /// <returns>An Coordinate representing the direction.</returns>
    /// <exception cref="ArgumentException">Thrown when an invalid or empty input is provided.</exception>
    public static Coordinate GetDirection(string? directionString)
    {
        if (directionString == "")
            throw new ArgumentException("Invalid input.");
        directionString = directionString?.ToLower();

        Coordinate direction;
        
        switch (directionString)
        {
            case "n":
                direction = new Coordinate(0, -1);
                break;
            case "s":
                direction = new Coordinate(0, 1);
                break;
            case "e":
                direction = new Coordinate(1, 0);
                break;
            case "w":
                direction = new Coordinate(-1, 0);
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