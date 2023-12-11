using System.Text.Json.Serialization;

namespace BrotatoLikeTeach.playerattributes.Model;

public class AttrData
{
    [JsonPropertyName("group")] public string Group { get; set; }

    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("range")] public string Range { get; set; }
}