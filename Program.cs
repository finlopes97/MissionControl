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
                option = Helper.GetOption();
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
                Helper.AddGuard();
                break;
            case 'f':
                Helper.AddFence();
                break;
            case 's':
                Helper.AddSensor();
                break;
            case 'c':
                Helper.AddCamera();
                break;
            case 'l':
                Helper.AddSpotlight();
                break;
            case 'd':
                Helper.ShowSafeDirections();
                break;
            case 'm':
                Helper.CreateBoard();
                break;
            case 'p':
                Helper.FindSafePath();
                break;
            case 'x':
                Helper.ExitProgram();
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
        List<Type> obstacleTypes = GetObstacleTypes();
        foreach (Type type in obstacleTypes)
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
        List<Type> obstacleTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => typeof(IObstacle).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false })
            .ToList();
        
        obstacleTypes.Sort((type1, type2) =>
        {
            IObstacle? obstacle1 = (IObstacle?)Activator.CreateInstance(type1);
            IObstacle? obstacle2 = (IObstacle?)Activator.CreateInstance(type2);
            if (obstacle1 != null && obstacle2 != null)
                return obstacle1.Priority.CompareTo(obstacle2.Priority);
            else
                return 0;
        });

        return obstacleTypes;
    }
}   