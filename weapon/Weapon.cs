using Godot;
using System;
using Godot.Collections;

public partial class Weapon : Node2D
{
	private AnimatedSprite2D _weaponAnimated;

	private Marker2D _shootPos;

	private Timer _timer;

	private Random _random = new Random();

	private float _bulletShootTime = 0.5f;

	private int _bulletSpeed = 2000;

	private int _bulletHurt = 1;

	private PackedScene _bullet;
	
	readonly private static Dictionary<string, Color> WeaponLevel = new Dictionary<string, Color>()
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
		_shootPos = GetNode<Marker2D>("ShootPos");
		_timer = GetNode<Timer>("Timer");
		_bullet = GD.Load<PackedScene>("res://bullet/bullet.tscn");
		((ShaderMaterial)_weaponAnimated.Material).SetShaderParameter("color",WeaponLevel[$"level{_random.Next(1,5)}"]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _OnTimerTimeOut()
	{
		if (_bullet.Instantiate() is not Bullet nowBullet)
		{
			return;
		}
		nowBullet.Speed = _bulletSpeed;
		nowBullet.Hurt = _bulletHurt;
		nowBullet.Position = _shootPos.GlobalPosition;
		nowBullet.Dir = new Vector2(1, 0);
		GetTree().Root.AddChild(nowBullet);
	}
}
