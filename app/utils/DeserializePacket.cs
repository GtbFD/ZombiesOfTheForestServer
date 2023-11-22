using Newtonsoft.Json;

namespace app.interfaces;

public class DeserializePacket
{
    public T Deserialize<T>(string packet)
    {
        return JsonConvert.DeserializeObject<T>(packet);
    }
}