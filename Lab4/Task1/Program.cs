namespace Task1
{
    internal class Program
    {
        static void Main( string[] args )
        {
            using ( var window = new Window( 800, 600, "My window" ) )
            {
                window.Run();
            }
        }
    }
}
