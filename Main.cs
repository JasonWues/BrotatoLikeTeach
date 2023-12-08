using Godot;

public partial class Main : Node
{
	public static Animations AnimationsScene;

	public static DropItems DropItemScene;

	public static Node2D DuplicateNode;

	//动效
	private PackedScene _animationsPackedScene;

	//掉落物
	private PackedScene _dropItemPackedScene;

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
		var node2D = new Node2D();
		node2D.Name = "DuplicateNode";
		GetWindow().CallDeferred(Node.MethodName.AddChild, node2D);
		DuplicateNode = node2D;
	}
}
