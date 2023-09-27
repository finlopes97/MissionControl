namespace MissionControl;

public static class Program
{
    public static void Main()
    {
        OrderedPair topLeft = new OrderedPair(0, 0);
        OrderedPair bottomRight = new OrderedPair(10, 7);
        Board board = new Board(topLeft, bottomRight);

        OrderedPair guardPos = new OrderedPair(2, 2);
        IObstacle guard = new Guard(guardPos);
        guard.AddObstacle(ref board);

        OrderedPair fenceStart = new OrderedPair(6, 2);
        OrderedPair fenceEnd = new OrderedPair(6, 5);
        IObstacle fence = new Fence(fenceStart, fenceEnd);
        fence.AddObstacle(ref board);
        
        Console.WriteLine(board.ToString());
    }
}   