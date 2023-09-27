namespace MissionControl;
using System;
using System.Linq;
using System.Reflection;

public static class Program
{
    public static void Main()
    {
        List<IObstacle> obstacles = new List<IObstacle>();
        
        PrintMenu();
        var input = Console.ReadKey().KeyChar;
        switch (input)
        {
            case 'g':
                Console.WriteLine( "Enter the guard's location (X,Y):" );
                OrderedPair position = GetPosition( Console.ReadLine() );
        }
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
    /// Safely exits the program.
    /// </summary>
    private static void ExitProgram()
    {
        Console.WriteLine("Exiting the program. Goodbye!");
        Environment.Exit(0); // Terminate the program with a status code of 0 (indicating a successful exit).
    }
    
    /// <summary>
    /// Prints the menu options and displays information about available obstacle types.
    /// </summary>
    private static void PrintMenu()
    {
        Console.WriteLine( "Select one of the following options" );
        var obstacleTypes = GetObstacleTypes();
        foreach (var type in obstacleTypes)
        {
            IObstacle? instance = (IObstacle?)Activator.CreateInstance(type);
            if (instance != null)
                Console.WriteLine(instance.ToString());
            else
                throw new Exception( $"Could not create instance of type {type.Name}" );
        }
        Console.WriteLine( "d) Show safe directions\n" +
                           "m) Display obstacle map\n" +
                           "p) Find safe path\n" +
                           "x) Exit\n" +
                           "Enter code:" );
    }
    
    /// <summary>
    /// Retrieves a list of obstacle types using reflection.
    /// </summary>
    /// <returns>A list of obstacle types that implement the IObstacle interface.</returns>
    private static List<Type> GetObstacleTypes()
    {
        // Use reflection to get all classes that implement IObstacle.
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => typeof(IObstacle).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .ToList();
    }  
}   