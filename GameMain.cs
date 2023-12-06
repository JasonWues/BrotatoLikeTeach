using Godot;
using System;

public partial class GameMain : Node
{

	private PackedScene _animationsPackedScene;
	public static Animations AnimationsScene;

	public static Node2D DuplicateNode;
	
	public override void _Ready()
	{
		_animationsPackedScene = GD.Load<PackedScene>("res://animations/animations.tscn");
		AnimationsScene = _animationsPackedScene.Instantiate<Animations>();
		AddChild(AnimationsScene);

		Node2D node2D = new Node2D();
		node2D.Name = "DuplicateNode";
		GetWindow().CallDeferred("add_child", node2D);
		DuplicateNode = node2D;
	}

	public override void _Process(double delta)
	{
	}
}
