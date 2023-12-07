using System.Threading.Tasks;
using BrotatoLikeTeach.gameData;
using Godot;

public partial class Enemy : CharacterBody2D
{
	private Vector2 _dir;

	private int _speed = 300;

	private Player _player;

	private AnimatedSprite2D _animatedSprite2D;

	[Export]
	public int Hp = 3;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = (Player)GetTree().GetFirstNodeInGroup("Player");
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_dir = (_player.GlobalPosition - GlobalPosition).Normalized();

		Velocity = _dir * _speed;

		MoveAndSlide();
	}

	public async void EnemyHurt(int hurt)
	{
		Hp -= hurt;
		Options options = new Options
		{
			Node = this,
			AnimationName = "enemiesHurt",
			Position = new Vector2(1,1),
			Scale = new Vector2(1,1)
		};
		await EnemyFlash();
		Main.AnimationsScene.RunAnimation(options);
		if (Hp <= 0)
		{
			EnemyDead();
		}
	}

	public void EnemyDead()
	{
		Main.AnimationsScene.RunAnimation(new Options()
		{
			AnimationName = "enemiesDead",
			Position = GlobalPosition,
			Scale = new Vector2(0.7f,0.7f)
		});
		Main.DropItemScene.GenDropItem(new Options()
		{
			AnimationName = "gold",
			Position = GlobalPosition,
			Scale = new Vector2(3,3)
		});
		QueueFree();
	}

	public async Task EnemyFlash()
	{
		((ShaderMaterial)_animatedSprite2D.Material).SetShaderParameter("flash_opacity",1);
		await ToSignal(GetTree().CreateTimer(0.1), SceneTreeTimer.SignalName.Timeout);
		((ShaderMaterial)_animatedSprite2D.Material).SetShaderParameter("flash_opacity",0);
		
	}

}
