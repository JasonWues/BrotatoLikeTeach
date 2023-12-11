using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BrotatoLikeTeach.playerattributes.Model;

public class PlayerAttr
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("type")] public List<AttrType> Type { get; set; }
}