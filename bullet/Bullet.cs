using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	
	public Vector2 Dir;
	
	public int Speed;

	public int Hurt;
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
}
