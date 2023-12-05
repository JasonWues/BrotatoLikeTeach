using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	private Vector2 _dir;

	private int _speed = 300;

	private Player _player;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = (Player)GetTree().GetFirstNodeInGroup("Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_dir = (_player.GlobalPosition - GlobalPosition).Normalized();

		Velocity = _dir * _speed;

		MoveAndSlide();
	}
}
