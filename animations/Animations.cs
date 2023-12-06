using Godot;
using BrotatoLikeTeach.animations;

public partial class Animations : Node2D
{
	public override void _Ready()
	{
		Hide();
	}
	
	public override void _Process(double delta)
	{
	}
	
	
	public void RunAnimation(Options options)
	{
		var temp = Duplicate() as Node2D;
		
		options.Node.AddChild(temp);
		temp.Show();
		temp.Scale = options.Scale ?? new Vector2(1, 1);
		temp.Position = options.Position;
		temp.GetNode<AnimatedSprite2D>("EnemyAnimation").Play(options.AnimationName);
	}
	
	private void _OnEnemyAnimationAnimationFinished()
	{
		QueueFree();
	}
}

