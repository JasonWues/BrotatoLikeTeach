#nullable enable
using Godot;

namespace BrotatoLikeTeach.animations;

public class Options
{
    public Node? Node { get; set; }
    
    public string? AnimationName { get; set; }
    
    public Vector2 Position { get; set; }
    
    public Vector2? Scale { get; set; }
}