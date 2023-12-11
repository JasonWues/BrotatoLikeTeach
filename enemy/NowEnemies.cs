using Godot;

public partial class NowEnemies : Node2D
{
    private PackedScene _enemy;

    private TileMap _tileMap;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _enemy = GD.Load<PackedScene>("res://enemy/enemy.tscn");
        _tileMap = (TileMap)GetTree().GetFirstNodeInGroup("Map");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void _OnTimerTimeout()
    {
        var num = GD.RandRange(0, _tileMap.GetUsedCells(0).Count - 1);
        var localPos = _tileMap.MapToLocal(_tileMap.GetUsedCells(0)[num]);
        var enemy = _enemy.Instantiate<CharacterBody2D>();
        enemy.Position = localPos * new Vector2(6, 6);
        AddChild(enemy);
    }
}