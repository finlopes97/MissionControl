namespace MissionControl;
using System;
using System.Linq;
using System.Reflection;

/// <summary>
/// The main class of the Mission Control program.
/// </summary>
public static class Program
{
    /// <summary>
    /// The entry point of the program.
    /// </summary>
    public static void Main()
    {
        bool showMenu = true;

        while(true)
        {
            if (showMenu)
            {
                PrintMenu();
            }
            char option;
            try
            {
                option = God.GetOption();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                showMenu = false;
                continue;
            }
            try
            {
                ProcessOption(option);
                showMenu = true; 
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                showMenu = false;
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
                God.AddGuard();
                break;
            case 'f':
                God.AddFence();
                break;
            case 's':
                God.AddSensor();
                break;
            case 'c':
                God.AddCamera();
                break;
            case 'l':
                God.AddSpotlight();
                break;
            case 'd':
                God.ShowSafeDirections();
                break;
            case 'm':
                God.CreateBoard();
                break;
            case 'x':
                God.ExitProgram();
                break;
            default:
                throw new ArgumentException("Invalid option.\n" +
                                            "Enter code:" );
        }
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
        var obstacleTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => typeof(IObstacle).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false })
            .ToList();
        
        obstacleTypes.Sort((type1, type2) =>
        {
            var obstacle1 = (IObstacle?)Activator.CreateInstance(type1);
            var obstacle2 = (IObstacle?)Activator.CreateInstance(type2);
            return obstacle1.Priority.CompareTo(obstacle2.Priority);
        });

        return obstacleTypes;
    }
}   