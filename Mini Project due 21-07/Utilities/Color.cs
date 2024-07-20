namespace Mini_Project_due_21_07.Utilities
{
    public static class Color
    {
        public static void WriteLine(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor; 
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
