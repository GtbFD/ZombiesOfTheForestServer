using app.interfaces;
using Newtonsoft.Json;

namespace app.packets.response;

public class PlayerLocalizationResponsePacket : IPacket
{
    [JsonProperty("opcode")]
    public int opcode { get; set; }
    [JsonProperty("x")]
    public float x { get; set; }
    [JsonProperty("y")]
    public float y { get; set; }
    [JsonProperty("z")]
    public float z { get; set; }
}