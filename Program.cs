﻿namespace MissionControl;
using System;
using System.Linq;
using System.Reflection;

/// <summary>
/// The main class of the Mission Control program.
/// </summary>
public static class Program
{
    private static readonly List<IObstacle> Obstacles = new List<IObstacle>();
    private static Board _board = new Board();
    
    /// <summary>
    /// The entry point of the program.
    /// </summary>
    public static void Main()
    {
        PrintMenu();

        while(true)
        {
            char option;
            try
            {
                option = GetOption();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
            try
            {
                ProcessOption(option);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    /// <summary>
    /// Processes the user's selected option and performs the corresponding action.
    /// </summary>
    /// <param name="option">The option selected by the user.</param>
    private static void ProcessOption(char option)
    {
        switch (option)
        {
            case 'g':
                Console.WriteLine( "Enter the guard's location (X,Y):" );
                var position = GetPosition( Console.ReadLine() );
                Obstacles.Add(new Guard(position));
                PrintMenu();
                break;
            case 'f':
                Console.WriteLine( "Enter the location where the fence starts (X,Y):" );
                var startingPosition = GetPosition( Console.ReadLine() );
                Console.WriteLine( "Enter the location where the fence ends (X,Y):" );
                var endingPosition = GetPosition( Console.ReadLine() );
                Obstacles.Add(new Fence(startingPosition, endingPosition));
                PrintMenu();
                break;
            case 'd':
                Console.WriteLine( ShowSafeDirections() );
                PrintMenu();
                break;
            case 'x':
                ExitProgram();
                break;
            default:
                throw new ArgumentException("Invalid option.\n" +
                                            "Enter code:" );
        }
    }

    /// <summary>
    /// Gets a user-selected option from the console input.
    /// </summary>
    /// <returns>The user-selected option character.</returns>
    private static char GetOption()
    {
        var input = Console.ReadLine();
        if (input == null || input.Length != 1)
            throw new ArgumentException("Invalid input.\n" +
                                        "Enter code:" );
        var lowerInput = input.ToLower();
        return lowerInput[0];
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
        Environment.Exit(0);
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

        Console.WriteLine("d) Show safe directions\n" +
                          "m) Display obstacle map\n" +
                          "p) Find safe path\n" +
                          "x) Exit\n" + 
                          "Enter code:");
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
    
    /// <summary>
    /// Checks the board for obstacles and returns a string of safe directions that the agent can move in.
    /// </summary>
    /// <returns>A string of safe directions that the agent can move in.</returns>
    private static string ShowSafeDirections()
    {
        List<char> safeDirections = new List<char> {'N', 'S', 'E', 'W'};
        var stringOfSafeDirections = "";
        
        Console.WriteLine( "Enter your current location (X,Y):" );
        var position = GetPosition( Console.ReadLine() );
        
        if (Obstacles.Count == 0)
        {
            return "You can safely move in any of the following directions: NSEW";
        }

        foreach (var obstacle in Obstacles)
        {
            if (position.IsEqual(obstacle.Position))
            {
                return "Agent, your location is compromised. Abort mission.";
            }

            if (position.X == obstacle.Position.X)
                safeDirections.Remove(position.Y < obstacle.Position.Y ? 'N' : 'S');
            else if (position.Y == obstacle.Position.Y)
                safeDirections.Remove(position.X < obstacle.Position.X ? 'E' : 'W');
        }

        foreach (var direction in safeDirections)
        {
            stringOfSafeDirections += direction;
        }
        
        return "You can safely move in any of the following directions: " + stringOfSafeDirections;
    }
}   