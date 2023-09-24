namespace MissionControl;

public static class Program
{
    public static void Main()
    {
        OrderedPair topLeft = new OrderedPair(0, 0);
        OrderedPair bottomRight = new OrderedPair(10, 7);
        Board board = new Board(topLeft, bottomRight);

        Console.WriteLine(board.ToString());

        OrderedPair guardPos = new OrderedPair(2, 2);
        IObstacle guard = new Guard(guardPos);
        board.AddObstacle(ref guard);

        OrderedPair fenceStart = new OrderedPair(3, 3);
        OrderedPair fenceEnd = new OrderedPair(0, 3);
        IObstacle fence = new Fence(fenceStart, fenceEnd);
        board.AddObstacle(ref fence);
        
        Console.WriteLine(board.ToString());
    }
}   