namespace MagicVilla_VillaAAPI.Logging
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.WriteLine("ERROR - {0}", message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
