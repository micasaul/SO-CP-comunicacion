using WebApi_csharp;
using WebApi_csharp.Services;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            var mode = args[0].ToLower();

            if (mode == "notification")
            {
                new NotificationService().Start();
            }
            else if (mode == "analytics")
            {
                new AnalyticsService().Start();
            }
            else
            {
                StartupApp.Run(args);
            }
        }
        else
        {
            StartupApp.Run(args);
        }
    }
}
