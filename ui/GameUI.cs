using Godot;
using System;
using BrotatoLikeTeach.gameData;

// ReSharper disable once InconsistentNaming
public partial class GameUI : CanvasLayer
{
	private ProgressBar _hpProgressBar;
	private ProgressBar _expProgressBar;
	private Label _gold;
	private Player _player;

	private Tween _tween;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = (Player)GetTree().GetFirstNodeInGroup("Player");
		_hpProgressBar = GetNode<ProgressBar>("Status/VBoxContainer/HpValue/HBoxContainer/HpValueBar");
		_expProgressBar = GetNode<ProgressBar>("Status/VBoxContainer/ExpValue/HBoxContainer/ExpValueBar");
		_gold = GetNode<Label>("Gold");
		_hpProgressBar.Value = _player.PlayerStatus.NowHp;
		_expProgressBar.Value = _player.PlayerStatus.NowExp;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_hpProgressBar.MaxValue = _player.PlayerStatus.MaxHp;
		_hpProgressBar.GetChild<Label>(0).Text = $"{_player.PlayerStatus.NowHp}/{_player.PlayerStatus.MaxHp}";
		_expProgressBar.MaxValue = _player.PlayerStatus.MaxExp;
		_expProgressBar.GetChild<Label>(0).Text = $"LV.{_player.PlayerStatus.Level}";

		_gold.Text = _player.PlayerStatus.Gold.ToString();
	}
	
	private void _OnPlayerStatusChange(GodotObject playerStatus)
	{
		_tween = GetTree().CreateTween().SetParallel();
		var status = playerStatus as PlayerStatus;
		_tween.TweenProperty(_hpProgressBar, "value",  status.NowHp, 1);
		_tween.TweenProperty(_expProgressBar, "value",  status.NowExp, 1);
	}

}

