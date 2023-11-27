using System.Text.RegularExpressions;

namespace app.utils.identifier;

public class PacketIdentifier
{
    public static int Opcode(string packetReceived)
    {
        if (packetReceived.Length <= 1) return 0;
        var pattern = @".*""opcode"":(.*?[0-9]*)";
        var matchResult = Regex.Match(packetReceived, @pattern).Groups[1].Value;
        var opcode = int.Parse(matchResult);
        return opcode;
    }
}