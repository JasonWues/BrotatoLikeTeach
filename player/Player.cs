using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private AnimatedSprite2D _playerAnim;

	private Vector2 _dir;

	private float _speed = 700;

	private bool flip = false;

	private bool _canMove = true;

	private bool _stop = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerAnim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		choosePlayer("player2");
	}

	public void choosePlayer(string type)
	{
		var playerPath = "res://player/assets/";
		
		_playerAnim.SpriteFrames.ClearAll();
		var spriteFrameCustom = new SpriteFrames();
		spriteFrameCustom.SetAnimationLoop("default",true);
		spriteFrameCustom.SetAnimationSpeed("default",7.0);
		var textureSize = new Vector2(960, 240);
		var spriteSize = new Vector2(240, 240);
		var fullTexture = GD.Load<Texture2D>(playerPath + type + "/" + type + "-sheet.png");

		var numColumns = (int)(textureSize.X / spriteSize.X);
		var numRow = (int)(textureSize.Y / spriteSize.Y);
		
		for (int x = 0; x < numColumns; x++)
		{
			for (int y = 0; y < numRow; y++)
			{
				var frmae = new AtlasTexture();
				frmae.Atlas = fullTexture;
				var regionVector2 = new Vector2(x, y);
				frmae.Region = new Rect2((regionVector2 * spriteSize), spriteSize);
				spriteFrameCustom.AddFrame("default",frmae);
			}
		}
		_playerAnim.SpriteFrames = spriteFrameCustom;
		
		_playerAnim.Play("default");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var globalMousePos = GetGlobalMousePosition();
		var selfPos = Position;

		if (globalMousePos.X >= selfPos.X)
		{
			flip = false;
		}
		else
		{
			flip = true;
		}

		_playerAnim.FlipH = flip;
		
		_dir = (globalMousePos - selfPos).Normalized();
		if (!_canMove || _stop)
		{
			return;
		}
		Velocity = _dir * _speed;
		MoveAndSlide();
		GD.Print(globalMousePos);
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

}

