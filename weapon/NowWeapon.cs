using Godot;
using System;

public partial class NowWeapon : Node2D
{
	private int _weaponRadius = 230;
	
	private int _weaponNum;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_weaponNum = GetChildCount();
		var unit = (float)(Math.Tau / _weaponNum);
		var weapons = GetChildren();

		for (int i = 0; i < weapons.Count; i++)
		{
			var weapon = (Node2D)weapons[i];
			var weaponRad = unit * i;
			var endPos = weapon.Position + new Vector2(_weaponRadius, 0).Rotated(weaponRad);
			weapon.Position = endPos;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
