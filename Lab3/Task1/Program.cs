namespace Task1
{
    internal static class Program
    {
        public static void Main( string[] args )
        {
            using ( var window = new Window( 800, 800, "Graph" ) )
            {
                window.Run();
            }
        }
    }
}
