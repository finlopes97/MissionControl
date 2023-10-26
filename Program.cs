namespace MissionControl;

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
                UIService.PrintMenu();
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
                UIService.ProcessOption(option);
                showMenu = true; 
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                showMenu = false;
            }
        }
    }
}   