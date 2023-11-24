using System.Net;

namespace app.server;

public class EndPointServer
{

    
    public IPEndPoint Config()
    {
        var endPointInfo = new ServerInfo();

        var ip = endPointInfo.IPAdress();
        
        var endPoint = new IPEndPoint(ip, endPointInfo.GetPort());

        return endPoint;
    }
}