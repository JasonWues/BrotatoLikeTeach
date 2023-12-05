using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BgMap : Node2D
{
	private TileMap _tileMap;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_tileMap = GetNode<TileMap>("TileMap");
		Random_tile();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void Random_tile()  
	{
		var random = new Random();
		var usedCells = new HashSet<Vector2I>();
		var grassCell = new Vector2I(18, 1);
		var flowerCell = new Vector2I(9, 3);
		var stoneCell = new Vector2I(6, 2);
		var availableCells = _tileMap.GetUsedCells(0);

		SetCellIfRandom(5, grassCell);
		SetCellIfRandom(1, flowerCell);
		SetCellIfRandom(2, stoneCell);
		return;

		void SetCellIfRandom(int chance, Vector2I cellType)
		{
			foreach (var cell in availableCells)
			{
				if (random.Next(0, 100) <= chance && usedCells.Add(cell))
				{
					_tileMap.SetCell(1, cell, 0, cellType);
				}
			}
		}
	}
}
