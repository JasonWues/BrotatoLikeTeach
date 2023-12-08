using Godot;

namespace BrotatoLikeTeach.gameData;

public partial class PlayerStatus : GodotObject
{
    public int NowHp { get; set; } = 100;

    public int MaxHp { get; set; } = 100;

    public int MaxExp { get; set; } = 5;

    public int NowExp { get; set; }

    public int Level { get; set; } = 1;

    public int Gold { get; set; }
}