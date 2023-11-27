using app.packets.request;
using Newtonsoft.Json;

namespace app.utils.io;

public class DeserializePacket
{
    public static T Deserialize<T>(string packet)
    {
        /*if (packet.IndexOf("<EOF>") > -1)
        {*/
            return JsonConvert.DeserializeObject<T>(packet);
        /*}
        return default;*/
    }
}