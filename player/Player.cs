using BrotatoLikeTeach.gameData;
using Godot;

public partial class Player : CharacterBody2D
{

    [Signal]
    public delegate void StatusChangeEventHandler(PlayerStatus playerStatus);

    private bool _canMove = true;

    private Vector2 _dir;

    private bool _flip;

    private AnimatedSprite2D _playerAnim;

    private bool _stop;

    public int LevelAddNum = 0;

    public PlayerStatus PlayerStatus;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _playerAnim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        PlayerStatus = new PlayerStatus();
        ChoosePlayer("player1");
    }

    private void ChoosePlayer(string type)
    {
        var playerPath = "res://player/assets/";
        var playerSheetPath = $"{playerPath}{type}/{type}-sheet.png";

        _playerAnim.SpriteFrames.ClearAll();
        var spriteFrameCustom = new SpriteFrames();
        spriteFrameCustom.SetAnimationSpeed("default", 7.0);
        var textureSize = new Vector2(960, 240);
        var spriteSize = new Vector2(240, 240);
        var fullTexture = GD.Load<Texture2D>(playerSheetPath);

        var numColumns = (int)(textureSize.X / spriteSize.X);
        var numRow = (int)(textureSize.Y / spriteSize.Y);

        for (var x = 0; x < numColumns; x++)
        for (var y = 0; y < numRow; y++)
        {
            var frame = new AtlasTexture();
            frame.Atlas = fullTexture;
            frame.Region = new Rect2(x * spriteSize.X, y * spriteSize.Y, spriteSize);
            spriteFrameCustom.AddFrame("default", frame);
        }
        _playerAnim.SpriteFrames = spriteFrameCustom;

        _playerAnim.Play("default");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var globalMousePos = GetGlobalMousePosition();
        var selfPos = Position;

        _flip = !(globalMousePos.X >= selfPos.X);

        _playerAnim.FlipH = _flip;

        _dir = (globalMousePos - selfPos).Normalized();
        if (!_canMove || _stop)
        {
            return;
        }
        Velocity = _dir * PlayerStatus.Speed;
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Left } && @event.IsPressed())
        {
            _canMove = false;
        }
        else if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Left } && !@event.IsPressed())
        {
            _canMove = true;
        }
        base._Input(@event);
    }

    private void _OnStopMouseEntered()
    {
        _stop = true;
        // Replace with function body.
    }

    private void _OnStopMouseExited()
    {
        _stop = false;
    }

    private void _OnDropItemAreaBodyEntered(Node2D body)
    {
        if (body.IsInGroup("DropItem") && body is DropItems dropItems)
        {
            PlayerStatus.NowExp += 1;
            PlayerStatus.Gold += 1;
            if (PlayerStatus.NowExp >= PlayerStatus.MaxExp)
            {
                PlayerStatus.Level++;
                PlayerStatus.NowExp = 0;
            }
            EmitSignal(SignalName.StatusChange, PlayerStatus);
            dropItems.CanMoving = true;
        }
    }

    private void _OnStopBodyEntered(Node2D body)
    {
        if (body.IsInGroup("Enemy") && body is Enemy enemy)
        {
            PlayerStatus.NowHp -= 1;
            EmitSignal(SignalName.StatusChange, PlayerStatus);
        }

        if (body.IsInGroup("DropItem") && body is DropItems dropItems)
        {
            dropItems.QueueFree();
        }
    }
}