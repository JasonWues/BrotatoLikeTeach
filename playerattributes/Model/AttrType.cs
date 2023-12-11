using System.Text.Json.Serialization;

namespace BrotatoLikeTeach.playerattributes.Model;

public class AttrType
{
    [JsonPropertyName("id")] public string Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("img")] public string Img { get; set; }
}