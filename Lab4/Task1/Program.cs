namespace Task1
{
    internal class Program
    {
        static void Main( string[] args )
        {
            using ( var window = new Window( 1200, 800, "My window" ) )
            {
                window.Run();
            }
        }
    }
}
