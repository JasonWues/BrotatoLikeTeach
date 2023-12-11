using BrotatoLikeTeach.gameData;
using Godot;

public partial class DropItems : CharacterBody2D
{
    private Player _player;

    private float _speed = 1500;

    public bool CanMoving;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Hide();
        SetCollisionLayerValue(5, false);
        _player = (Player)GetTree().GetFirstNodeInGroup("Player");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (CanMoving)
        {
            var dir = (_player.Position - Position).Normalized();

            Velocity = dir * _speed;

            MoveAndSlide();
        }
    }

    public void GenDropItem(Options options)
    {
        var temp = Duplicate() as CharacterBody2D;

        if (options.Node == null)
        {
            options.Node = Main.DuplicateNode;
        }
        options.Node.CallDeferred(Node.MethodName.AddChild, temp);
        temp.CallDeferred(CanvasItem.MethodName.Show);
        temp.CallDeferred(CollisionObject2D.MethodName.SetCollisionLayerValue, 5, true);
        temp.Scale = options.Scale ?? new Vector2(1, 1);
        temp.Position = options.Position;
        temp.GetNode<AnimatedSprite2D>("GoldAnimated").Play(options.AnimationName);
    }
}