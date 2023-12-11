using Godot;

public partial class Bullet : CharacterBody2D
{

    public Vector2 Dir;

    public int Hurt;

    public int Speed;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Velocity = Dir * Speed;
        MoveAndSlide();
    }

    private void _OnArea2dBodyShapeEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex)
    {
        if (body.IsInGroup("Enemy"))
        {
            if (body is Enemy enemy)
            {
                enemy.EnemyHurt(Hurt);
                QueueFree();
            }
        }


        if (body is not TileMap tileMap)
        {
            return;
        }
        var coords = tileMap.GetCoordsForBodyRid(bodyRid);
        var tileData = tileMap.GetCellTileData(2, coords);
        var isWall = (bool)tileData.GetCustomData("isWall");
        if (isWall)
        {
            QueueFree();
        }

    }
}