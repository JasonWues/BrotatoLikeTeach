using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private AnimatedSprite2D _playerAnim;

	private Vector2 _dir;

	private float _speed = 700;

	private bool flip = false;

	private bool _canMove = true;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerAnim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
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
		if (_canMove)
		{
			Velocity = _dir * _speed;
			MoveAndSlide();
			GD.Print(globalMousePos);
		}
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
		// Replace with function body.
	}
}


