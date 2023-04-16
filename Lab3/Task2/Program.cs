namespace Task2
{
    internal static class Program
    {
        public static void Main( string[] args )
        {
            using ( var Window = new Window( 600, 600, "Pin" ) )
            {
                Window.Run();
            }
        }
    }
}
