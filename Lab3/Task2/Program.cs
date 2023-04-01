namespace Task2
{
    internal static class Program
    {
        public static void Main( string[] args )
        {
            using ( var Window = new Window( 800, 600, "Pin" ) )
            {
                Window.Run();
            }
        }
    }
}
