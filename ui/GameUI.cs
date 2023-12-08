using BrotatoLikeTeach.gameData;
using Godot;

// ReSharper disable once InconsistentNaming
public partial class GameUI : CanvasLayer
{
	[Signal]
	public delegate void RoundEndEventHandler();
	
	private Label _nowRound;

	private Label _timeShow;

	private Timer _timer;

	private ProgressBar _expProgressBar;

	private Label _gold;

	private ProgressBar _hpProgressBar;

	private Player _player;

	private Tween _tween;

	private int _nowRoundNum = 0;
	
	private int _roundTime = 0;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = (Player)GetTree().GetFirstNodeInGroup("Player");
		_hpProgressBar = GetNode<ProgressBar>("Status/VBoxContainer/HpValue/HBoxContainer/HpValueBar");
		_expProgressBar = GetNode<ProgressBar>("Status/VBoxContainer/ExpValue/HBoxContainer/ExpValueBar");
		_gold = GetNode<Label>("Gold");
		_nowRound = GetNode<Label>("CountDown/NowRound");
		_timeShow = GetNode<Label>("CountDown/TimeShow");
		_timer = GetNode<Timer>("CountDown/Timer");
		_hpProgressBar.Value = _player.PlayerStatus.NowHp;
		_expProgressBar.Value = _player.PlayerStatus.NowExp;
		InitRound();
	}

	public override void _Process(double delta)
	{
		_hpProgressBar.MaxValue = _player.PlayerStatus.MaxHp;
		_hpProgressBar.GetChild<Label>(0).Text = $"{_player.PlayerStatus.NowHp}/{_player.PlayerStatus.MaxHp}";
		_expProgressBar.MaxValue = _player.PlayerStatus.MaxExp;
		_expProgressBar.GetChild<Label>(0).Text = $"LV.{_player.PlayerStatus.Level}";

		_gold.Text = _player.PlayerStatus.Gold.ToString();
	}

	private void InitRound()
	{
		_nowRoundNum += 1;
		_nowRound.Text = $"第{_nowRoundNum}波";
		_roundTime = 10;
		_timeShow.Text = _roundTime.ToString();
		_timer.Start();
	}

	private void _OnPlayerStatusChange(GodotObject playerStatus)
	{
		_tween = GetTree().CreateTween().SetParallel();
		var status = playerStatus as PlayerStatus;
		_tween.TweenProperty(_hpProgressBar, "value", status.NowHp, 1);
		_tween.TweenProperty(_expProgressBar, "value", status.NowExp, 1);
	}
	
	private void _OnTimerTimeout()
	{
		_roundTime --;
		_timeShow.Text = _roundTime.ToString();
		if (_roundTime <= 0)
		{
			_timer.Stop();
			EmitSignal(SignalName.RoundEnd);
		}
	}
}
