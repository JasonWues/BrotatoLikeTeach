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
		var useCells = new List<Vector2I>();

		var bg1Cells = _tileMap.GetUsedCells(0);
		
		foreach (var cell in bg1Cells)
		{
			var num = random.Next(0, 100);
			if (num <= 5)
			{
				_tileMap.SetCell(1,cell,0,new Vector2I(18,1));
				useCells.Add(cell);
			}
		}

		var notUseCells = bg1Cells.Except(useCells).ToList();
		useCells.Clear();
		
		foreach (var cell in from cell in notUseCells let num = random.Next(0, 100) where num <= 1 select cell)
		{
			_tileMap.SetCell(1,cell,0,new Vector2I(9,3));
			useCells.Add(cell);
		}

		var notUseCells2 = notUseCells.Except(useCells);

		foreach (var cell in from cell in notUseCells2 let num = random.Next(0,100) where num <= 2 select cell)
		{
			_tileMap.SetCell(2,cell,0,new Vector2I(6,2));
		}
		
		
	}
}
