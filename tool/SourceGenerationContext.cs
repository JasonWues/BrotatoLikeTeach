using System.Collections.Generic;
using System.Text.Json.Serialization;
using BrotatoLikeTeach.playerattributes.Model;

namespace BrotatoLikeTeach.tool;

[JsonSerializable(typeof(PlayerAttr))]
[JsonSerializable(typeof(Dictionary<string, PlayerAttr>))]
[JsonSerializable(typeof(AttrData))]
[JsonSerializable(typeof(Dictionary<string, AttrData>))]
public partial class SourceGenerationContext : JsonSerializerContext
{
}