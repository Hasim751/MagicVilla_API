namespace MagicVilla_VillaAAPI.Logging
{
    public class LoggingV2 : ILogging
    {
        public void Log(string message, string type)
        {
            if(type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("ERROR - {0}",message);
                Console.BackgroundColor= ConsoleColor.Black;

            }
            else if(type == "warning")
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.Error.WriteLine("WARNING - {0}", message);
                Console.BackgroundColor = ConsoleColor.Black;

            }
            else
            {
                Console.Error.WriteLine(message);

            }
        }
    }
}
