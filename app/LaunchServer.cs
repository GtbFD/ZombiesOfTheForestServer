class LaunchServer
{
    public static void Main(String[] args)
    {
        var serverApp = new ServerConfiguration();
        serverApp.Start();
    }
}