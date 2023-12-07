using Godot;
using System;

public partial class Main : Node
{
	//动效
	private PackedScene _animationsPackedScene;
	public static Animations AnimationsScene;
	
	//掉落物
	private PackedScene _dropItemPackedScene;
	public static DropItems DropItemScene;

	public static Node2D DuplicateNode;
	
	public override void _Ready()
	{
		InitDuplicateNode();
		_animationsPackedScene = GD.Load<PackedScene>("res://animations/animations.tscn");
		AnimationsScene = _animationsPackedScene.Instantiate<Animations>();
		AddChild(AnimationsScene);
		_dropItemPackedScene = GD.Load<PackedScene>("res://dropitems/drop_items.tscn");
		DropItemScene = _dropItemPackedScene.Instantiate<DropItems>();
		AddChild(DropItemScene);
	}

	public override void _Process(double delta)
	{
	}
	
	private void InitDuplicateNode()
	{
		if (DuplicateNode != null)
		{
			DuplicateNode.QueueFree();
		}
		Node2D node2D = new Node2D();
		node2D.Name = "DuplicateNode";
		GetWindow().CallDeferred("add_child", node2D);
		DuplicateNode = node2D;
	}
}
