using WebApi_csharp;
using WebApi_csharp.Services;

public class Program
{
    public static void Main(string[] args)
    {
        // Si se ejecuta con "notification", arranca el consumidor
        if (args.Length > 0 && args[0].ToLower() == "notification")
        {
            new NotificationService().Start();
        }
        else
        {
            StartupApp.Run(args);
        }
    }
}
