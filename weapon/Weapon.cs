using Godot;
using System;
using Godot.Collections;

public partial class Weapon : Node2D
{
	private AnimatedSprite2D _weaponAnimated;

	private Random _random = new Random();

	readonly private static Dictionary<string, Color> _weaponLevel = new Dictionary<string, Color>()
	{
		{"level1",new Color("#b0c3d9")},
		{"level2",new Color("#4b69ff")},
		{"level3",new Color("#d32ce6")},
		{"level4",new Color("#8847ff")},
		{"level5",new Color("#eb4b4b")},
	};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_weaponAnimated = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		((ShaderMaterial)_weaponAnimated.Material).SetShaderParameter("color",_weaponLevel[$"level{_random.Next(1,5)}"]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
