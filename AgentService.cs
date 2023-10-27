namespace MissionControl;

/// <summary>
/// Contains methods related to agent movement and pathfinding.
/// </summary>
public static class AgentService
{ 
    /// <summary>
    /// Checks the board for obstacles and writes a string of safe directions that the agent can move in to the program's output.
    /// </summary>
    public static void ShowSafeDirections()
    {
        HashSet<char> safeDirections = new HashSet<char> {'N', 'S', 'E', 'W'};
        
        Console.WriteLine( "Enter your current location (X,Y):" );
        Coordinate position = Helper.GetPosition( Console.ReadLine() );

        foreach (IObstacle obstacle in ObstacleService.Obstacles)
        {
            if (obstacle.Positions == null) continue;

            if (obstacle.IntersectsWithCell(position))
            {
                Console.WriteLine("Agent, your location is compromised. Abort mission.");
                return;
            }

            if (obstacle.IntersectsWithCell(new Coordinate(position.X, position.Y - 1))) // North
                safeDirections.Remove('N');
            if (obstacle.IntersectsWithCell(new Coordinate(position.X, position.Y + 1))) // South
                safeDirections.Remove('S');
            if (obstacle.IntersectsWithCell(new Coordinate(position.X + 1, position.Y))) // East
                safeDirections.Remove('E');
            if (obstacle.IntersectsWithCell(new Coordinate(position.X - 1, position.Y))) // West
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
        Cell startCell = new Cell(Helper.GetPosition(Console.ReadLine()));
        Console.WriteLine( "Enter the location of the mission objective (X,Y):" );
        Cell endCell = new Cell(Helper.GetPosition(Console.ReadLine()));
        
        PathfindingService.AStar(startCell, endCell);
    }
}