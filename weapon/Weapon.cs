using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Weapon : Node2D
{

	readonly private static Godot.Collections.Dictionary<string, Color> WeaponLevel =
		new Godot.Collections.Dictionary<string, Color>
		{
			{ "level1", new Color("#b0c3d9") },
			{ "level2", new Color("#4b69ff") },
			{ "level3", new Color("#d32ce6") },
			{ "level4", new Color("#8847ff") },
			{ "level5", new Color("#eb4b4b") }
		};

	private PackedScene _bullet;

	private int _bulletHurt = 1;

	private float _bulletShootTime = 0.5f;

	private int _bulletSpeed = 2000;

	private HashSet<Enemy> _enemies;

	private Marker2D _shootPos;

	private Timer _timer;

	private AnimatedSprite2D _weaponAnimated;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_weaponAnimated = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_shootPos = GetNode<Marker2D>("ShootPos");
		_timer = GetNode<Timer>("Timer");
		_bullet = GD.Load<PackedScene>("res://bullet/bullet.tscn");
		_enemies = [];
		((ShaderMaterial)_weaponAnimated.Material).SetShaderParameter("color", WeaponLevel[$"level{GD.RandRange(1, 5)}"]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_enemies.Count > 0)
		{
			LookAt(_enemies.First().Position);
		}
		else
		{
			RotationDegrees = 0;
		}
	}

	private void _OnTimerTimeOut()
	{
		if (_bullet.Instantiate() is not Bullet nowBullet)
		{
			return;
		}
		if (_enemies.Count > 0)
		{
			nowBullet.Speed = _bulletSpeed;
			nowBullet.Hurt = _bulletHurt;
			nowBullet.Position = _shootPos.GlobalPosition;
			nowBullet.Dir = (_enemies.First().GlobalPosition - nowBullet.Position).Normalized();
			GetTree().Root.AddChild(nowBullet);
		}
	}

	private void _OnArea2dBodyEntered(Enemy body)
	{
		if (body.IsInGroup("Enemy") && _enemies.Add(body))
		{
			_enemies = _enemies.OrderBy(x => x.GlobalPosition.DistanceTo(GlobalPosition)).ToHashSet();
		}
	}

	private void _OnArea2dBodyExited(Enemy body)
	{
		if (body.IsInGroup("Enemy") && _enemies.Contains(body))
		{
			_enemies.Remove(body);
			_enemies = _enemies.OrderBy(x => x.GlobalPosition.DistanceTo(GlobalPosition)).ToHashSet();
		}
	}
}
