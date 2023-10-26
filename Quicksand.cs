namespace MissionControl;

/// <summary>
/// Quicksand is an obstacle that has a randomised splatter pattern that extends from an origin point with a greater density towards the centre.
/// It is traversable but has a movement cost. The agent should prefer an unimpeded path over a path with quicksand but if it is faster
/// it will still take the path with quicksand.
/// </summary>
public class Quicksand : IObstacle
{
    /// <summary>
    /// Gets the positions that the mud occupies as a list of ordered pairs.
    /// </summary>
    public List<Coordinate>? Positions { get; set; }

    /// <summary>
    /// Quicksand has a movement cost as it is traversable.
    /// </summary>
    public int MovementCost { get; }

    /// <summary>
    /// Gets the character code representing the guard.
    /// </summary>
    public char CharCode { get; }
    
    /// <summary>
    /// Gets the type of the obstacle as a string.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// Determines the list position in which this obstacle appears in the main menu.
    /// </summary>
    public int Priority => 5;

    /// <summary>
    /// Adds quicksand to the board.
    /// </summary>
    /// <param name="cell">The origin point of the quicksand.</param>
    public void AddObstacle(ref Cell cell)
    {
        if (Positions != null)
        {
            cell.CurrentObstacle = this;
        }
    }
    
    /// <summary>
    /// Checks if the quicksand obstacle intersects with a specified cell.
    /// </summary>
    /// <param name="cellToCheck">The cell to check for intersection.</param>
    /// <returns>True if the quicksand intersects with the cell, otherwise false.</returns>
    public bool IntersectsWithCell(Coordinate cellToCheck)
    {
        return Positions != null && Positions.Contains(cellToCheck);
    }
    
    /// <summary>
    /// Fills an area with quicksand based on a splatter pattern.
    /// </summary>
    /// <param name="origin">The center of the quicksand area.</param>
    /// <param name="areaRange">The range around the origin to consider for quicksand placement.</param>
    /// <returns>A list of coordinates representing the quicksand-filled area.</returns>
    private List<Coordinate> FillArea(Coordinate origin, double areaRange)
    {
        List<Coordinate> filledPositions = new List<Coordinate>();
        Random random = new Random();
        int areaRangeInt = Convert.ToInt32(areaRange);

        for (int x = origin.X - areaRangeInt; x <= origin.X + areaRangeInt; x++)
        {
            for (int y = origin.Y - areaRangeInt; y <= origin.Y + areaRangeInt; y++)
            {
                double distance = Math.Sqrt(Math.Pow(x - origin.X, 2) + Math.Pow(y - origin.Y, 2));
                double probability = 1 - (distance / (areaRangeInt + 1)); // The probability of a cell being filled decreases as the distance from the origin increases.

                if (random.NextDouble() < probability)
                {
                    filledPositions.Add(new Coordinate(x,y));
                }
            }
        }

        return filledPositions;
    }
    
    /// <summary>
    /// Builds a string for use in the main menu
    /// </summary>
    /// <returns>A string to be used when printing the main menu of the program.</returns>
    public override string ToString()
    {
        return $"{CharCode}) Add '{Type}' obstacle.";
    }
    
    /// <summary>
    /// Initializes a new instance of the Quicksand class with the specified position, range, and depth.
    /// </summary>
    /// <param name="quicksandOrigin">The origin position of the quicksand.</param>
    /// <param name="quicksandRange">The range that the quicksand can spawn in.</param>
    /// <param name="quicksandDepth">Determines the movement cost of the quicksand.</param>
    public Quicksand(Coordinate quicksandOrigin, double quicksandRange, double quicksandDepth)
    {
        Positions = FillArea(quicksandOrigin, quicksandRange);
        if (quicksandDepth >= 0) MovementCost = Convert.ToInt32(quicksandDepth);
        else throw new ArgumentException("Quicksand depth cannot be negative.");
        CharCode = 'q';
        Type = "Quicksand";
    }

    /// <summary>
    /// Initialises a new instance of the guard class with no additional parameters.
    /// Specifically for generating a dynamic list in the main menu, not for use in the actual program.
    /// </summary>
    public Quicksand()
    {
        CharCode = 'q';
        Type = "Quicksand";
    }
}