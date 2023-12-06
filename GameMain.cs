using Godot;
using System;

public partial class GameMain : Node
{

	private PackedScene _animationsPackedScene;
	private Node2D _animationsScene;

	
	public override void _Ready()
	{
		_animationsPackedScene = GD.Load<PackedScene>("res://animations/animations.tscn");
		_animationsScene = _animationsPackedScene.Instantiate<Node2D>();
		AddChild(_animationsScene);
	}

	public override void _Process(double delta)
	{
	}
}
