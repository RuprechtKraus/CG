namespace Maze
{
    internal class Program
    {
        static void Main( string[] args )
        {
            using ( var window = new Window( 1200, 900, "Maze" ) )
            {
                window.Run();
            }
        }
    }
}
