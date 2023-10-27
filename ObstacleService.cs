namespace MissionControl;

/// <summary>
/// A static class that contains methods for adding obstacles to the list of obstacles.
/// </summary>
public static class ObstacleService
{
    /// <summary>
    /// A list of obstacles that have been added to the program.
    /// </summary>
    public static readonly List<IObstacle> Obstacles = new List<IObstacle>();

    /// <summary>
    /// Adds a guard to the list of obstacles based on user input.
    /// </summary>
    public static void AddGuard()
    {
        Console.WriteLine( "Enter the guard's location (X,Y):" );
        Coordinate guardPosition = Helper.GetPosition( Console.ReadLine() );
        Obstacles.Add(new Guard(guardPosition));
    }

    /// <summary>
    /// Adds a fence to the list of obstacles based on user input.
    /// </summary>
    public static void AddFence()
    {
        Console.WriteLine( "Enter the location where the fence starts (X,Y):" );
        Coordinate fenceStartingPosition = Helper.GetPosition( Console.ReadLine() );
        Console.WriteLine( "Enter the location where the fence ends (X,Y):" );
        Coordinate fenceEndingPosition = Helper.GetPosition( Console.ReadLine() );
        Obstacles.Add(new Fence(fenceStartingPosition, fenceEndingPosition));
    }

    /// <summary>
    /// Adds a sensor to the list of obstacles based on user input.
    /// </summary>
    public static void AddSensor()
    {
        Console.WriteLine( "Enter the sensor's location (X,Y):" );
        Coordinate sensorPosition = Helper.GetPosition( Console.ReadLine() ); 
        Console.WriteLine( "Enter the sensor's range (in klicks):" );
        double sensorRange = Helper.GetDistance(Console.ReadLine());
        IObstacle sensor = new Sensor(sensorPosition, sensorRange);
        Obstacles.Add(sensor);
    }

    public static void AddCamera()
    {
        Console.WriteLine( "Enter the camera's location (X,Y):" );
        Coordinate cameraPosition = Helper.GetPosition( Console.ReadLine() );
        Console.WriteLine( "Enter the direction the camera is facing (n, s, e or w):" );
        Coordinate cameraDirection = Helper.GetDirection( Console.ReadLine() );
        Obstacles.Add(new Camera(cameraPosition, cameraDirection));
    }

    public static void AddSpotlight()
    {
        Console.WriteLine( "Enter the spotlight's location (X,Y):" );
        Coordinate spotlightPosition = Helper.GetPosition(Console.ReadLine());
        Console.WriteLine( "Enter the direction the spotlight is facing in (n, s, e or w):" );
        Coordinate spotlightDirection = Helper.GetDirection( Console.ReadLine() );
        Console.WriteLine( "Enter the spotlight's range (in klicks):" );
        double spotlightRange = Helper.GetDistance( Console.ReadLine() );
        Obstacles.Add(new Spotlight(spotlightPosition, spotlightDirection, spotlightRange));
    }

    public static void AddQuicksand()
    {
        Console.WriteLine( "Enter the position at the center of the quicksand (X,Y)." );
        Coordinate quicksandOriginPosition = Helper.GetPosition(Console.ReadLine());
        Console.WriteLine( "Enter the range of the quicksand (in klicks)." );
        double quicksandRange = Helper.GetDistance(Console.ReadLine());
        Console.WriteLine( "Please enter the depth of the quicksand (in metres) to determine the difficulty to cross." );
        double quicksandDepth = Helper.GetDistance(Console.ReadLine());
        Obstacles.Add(new Quicksand(quicksandOriginPosition, quicksandRange, quicksandDepth));
    }
}