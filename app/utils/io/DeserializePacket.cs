using Newtonsoft.Json;

namespace app.utils.io;

public class DeserializePacket
{
    public static T Deserialize<T>(string packet)
    {
        return JsonConvert.DeserializeObject<T>(packet);
    }
}