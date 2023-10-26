using System.Reflection;
namespace MissionControl;

public static class UIService
{
    /// <summary>
    /// Prints the menu options and displays information about available obstacle types.
    /// </summary>
    public static void PrintMenu()
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
    /// Processes the user's selected option and performs the corresponding action.
    /// </summary>
    /// <param name="option">The option selected by the user.</param>
    public static void ProcessOption(char option)
    {
        switch (option)
        {
            case 'g':
                ObstacleService.AddGuard();
                break;
            case 'f':
                ObstacleService.AddFence();
                break;
            case 's':
                ObstacleService.AddSensor();
                break;
            case 'c':
                ObstacleService.AddCamera();
                break;
            case 'l':
                ObstacleService.AddSpotlight();
                break;
            case 'q':
                ObstacleService.AddQuicksand();
                break;
            case 'd':
                AgentService.ShowSafeDirections();
                break;
            case 'm':
                Helper.CreateBoard();
                break;
            case 'p':
                AgentService.FindSafePath();
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