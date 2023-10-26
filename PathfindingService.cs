namespace MissionControl;
using System.Text;

/// <summary>
/// Provides methods for pathfinding within the mission control system.
/// </summary>
public static class PathfindingService
{
    /// <summary>
    /// Implements the A* pathfinding algorithm to find the shortest path between two cells.
    /// </summary>
    /// <param name="startCell">The starting cell.</param>
    /// <param name="endCell">The destination cell.</param>
    public static void AStar(Cell startCell, Cell endCell)
    {
        // Using a priority queue to store and iterate over cells based on their F cost for better performance
        PriorityQueue<Cell, int> openQueue = new PriorityQueue<Cell, int>();
        HashSet<Cell> openSet  = new HashSet<Cell>();
        HashSet<Cell> closedSet = new HashSet<Cell>();
        
        openQueue.Enqueue(startCell, startCell.FCost);
        openSet.Add(startCell);
        
        while (openQueue.Count > 0)
        {
            Cell currentCell = openQueue.Dequeue();
            openSet.Remove(currentCell);
            closedSet.Add(currentCell);
            
            // Check if the current cell is the destination
            if (currentCell.Equals(endCell))
            {
                List<Cell> path = ReconstructPath(currentCell);
                string directions = GetDirections(path);
                Console.WriteLine($"The following path will take you to the objective:\n{directions}");
                return;
            }

            foreach (Cell neighbour in GetNeighbours(currentCell))
            {
                // Skip the neighbour if it's an obstacle or already processed
                if (closedSet.Contains(neighbour) || !IsTraversable(neighbour))
                {
                    closedSet.Add(neighbour);
                    continue;
                }
                
                int tentativeGCost = currentCell.GCost + GetMovementCost(currentCell);
                
                // Update the neighbour's cost if a shorter path is found
                if (tentativeGCost < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = tentativeGCost;
                    neighbour.HCost = GetDistance(neighbour, endCell);
                    neighbour.Parent = currentCell;

                    if (!openSet.Contains(neighbour))
                    {
                        openQueue.Enqueue(neighbour, neighbour.FCost);
                        openSet.Add(neighbour);
                    }
                }
            }
        }
        Console.WriteLine( "There is no safe path to the objective." );
    }

    /// <summary>
    /// Gets the neighbouring cells of the given cell.
    /// </summary>
    /// <param name="cell">The cell to get neighbours for.</param>
    /// <returns>A list of neighbouring cells.</returns>
    private static List<Cell> GetNeighbours(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();

        int x = cell.CellPosition.X;
        int y = cell.CellPosition.Y;
        
        // Add the cells to the north, south, east, and west of the current cell
        neighbours.Add(new Cell(new Coordinate(x, y - 1)));
        neighbours.Add(new Cell(new Coordinate(x, y + 1)));
        neighbours.Add(new Cell(new Coordinate(x + 1, y)));
        neighbours.Add(new Cell(new Coordinate(x - 1, y)));
        
        return neighbours;
    }

    /// <summary>
    /// Reconstructs the path from the start cell to the given end cell.
    /// </summary>
    /// <param name="endCell">The end cell of the path.</param>
    /// <returns>A list of cells representing the path.</returns>
    private static List<Cell> ReconstructPath(Cell endCell)
    {
        List<Cell> path = new List<Cell>();
        Cell? current = endCell;

        // Trace back from the end cell to the start cell using the Parent property
        while (current != null)
        {
            path.Add(current);
            current = current.Parent;
        }

        path.Reverse();
        return path;
    }

    /// <summary>
    /// Converts a list of cells into a string of directions.
    /// </summary>
    /// <param name="path">The list of cells representing the path.</param>
    /// <returns>A string of directions (N, S, E, W).</returns>
    private static string GetDirections(List<Cell> path)
    {
        StringBuilder directions = new StringBuilder();
        for (int i = 0; i < path.Count - 1; i++)
        {
            Cell current = path[i];
            Cell next = path[i + 1];

            // Determine the direction from the current cell to the next cell
            if (next.CellPosition.Y < current.CellPosition.Y)
                directions.Append('N');
            else if (next.CellPosition.X > current.CellPosition.X)
                directions.Append('E');
            else if (next.CellPosition.Y > current.CellPosition.Y)
                directions.Append('S');
            else if (next.CellPosition.X < current.CellPosition.X)
                directions.Append('W');
        }

        return directions.ToString();
    }
    
    /// <summary>
    /// Checks if the cell is traversable
    /// </summary>
    /// <param name="cell">The cell to check</param>
    /// <returns>True if the cell is traversable, false if not.</returns>
    private static bool IsTraversable(Cell cell)
    {
        foreach (IObstacle obstacle in ObstacleService.Obstacles)
        {
            if (obstacle.IntersectsWithCell(cell.CellPosition) && obstacle.MovementCost == 0)
                return false;
        }

        return true;
    }
    
    /// <summary>
    /// Gets the movement cost of the cell.
    /// </summary>
    /// <param name="cell">The cell to check.</param>
    /// <returns>An integer to add to a cell's FCost.</returns>
    private static int GetMovementCost(Cell cell)
    {
        foreach (IObstacle obstacle in ObstacleService.Obstacles)
        {
            if (obstacle.IntersectsWithCell(cell.CellPosition))
            {
                return obstacle.MovementCost;
            }
        }
        return 1; // Default movement cost
    }
    
    /// <summary>
    /// Calculates the Manhattan distance between two cells.
    /// </summary>
    /// <param name="a">The first cell.</param>
    /// <param name="b">The second cell.</param>
    /// <returns>The Manhattan distance between the two cells.</returns>
    private static int GetDistance(Cell a, Cell b)
    {
        return Math.Abs(a.CellPosition.X - b.CellPosition.X) + Math.Abs(a.CellPosition.Y - b.CellPosition.Y);
    }
}