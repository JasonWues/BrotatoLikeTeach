using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BrotatoLikeTeach.gameData;
using BrotatoLikeTeach.playerattributes.Model;
using Godot;
using FileAccess = Godot.FileAccess;

public partial class SceneUpdate : CanvasLayer
{

	private GridContainer _attrItemChose;

	private Panel _attrItemTemplate;

	private VBoxContainer _attrList;

	private HBoxContainer _attrTemplate;

	private Button _continue;

	private Button _refresh;

	private Label _update;

	private Player _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_attrItemChose = GetNode<GridContainer>("AttrItemChose");
		_attrItemTemplate = GetNode<Panel>("AttrItemChose/AttrItemTemplate");
		_attrList = GetNode<VBoxContainer>("AttrValuePanel/MarginContainer/ScrollContainer/AttrList");
		_attrTemplate = GetNode<HBoxContainer>("AttrValuePanel/MarginContainer/ScrollContainer/AttrList/AttrTemplate");
		_refresh = GetNode<Button>("Refresh");
		_update = GetNode<Label>("Update");
		_continue = GetNode<Button>("Continue");
		_player = (Player)GetTree().GetFirstNodeInGroup("Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Init()
	{
		Visible = true;
		GenAttrChoose();
		LoadPlayerAttr();
	}

	private void GenAttrChoose()
	{
		var attrDataDic = LoadJsonFile<Dictionary<string, AttrData>>("res://playerattributes/AttrData.json");
		var attrDic = LoadJsonFile<Dictionary<string, PlayerAttr>>("res://playerattributes/PlayerAttr.json");

		ClearVisibleNodes(false);

		for (var i = 0; i < 4; i++)
		{
			var attrItem = (Panel)_attrItemTemplate.Duplicate();
			attrItem.Show();

			var dataKeyList = attrDataDic.Keys.ToList();
			var dataKey = GD.RandRange(0, dataKeyList.Count - 1);
			
			var attrData = attrDataDic[dataKeyList[dataKey]];
			var attr = attrDic[attrData.Group];
			var type = attr.Type.FirstOrDefault(x => x.Id == attrData.Type);

			SetAttrItemProperties(attrItem, type, attr, attrData);

			attrItem.GetNode<Button>("Button").Pressed += GenAttrChoose;

			_attrItemChose.AddChild(attrItem);
		}
	}

	private void LoadPlayerAttr()
	{
		ClearVisibleNodes(true);
		var properties = typeof(PlayerStatus).GetProperties();
		foreach (var propertyInfo in properties)
		{
			var attrItem = (HBoxContainer)_attrTemplate.Duplicate();
			attrItem.Show();
			
			attrItem.GetNode<Label>("AttrName").Text = Tr(propertyInfo.Name); 
			attrItem.GetNode<Label>("AttrValue").Text = propertyInfo.GetValue(_player.PlayerStatus).ToString();
			
			_attrList.AddChild(attrItem);
		}
	}

	private T LoadJsonFile<T>(string filePath)
	{
		using var fileAccess = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
		var json = fileAccess.GetAsText();
		return JsonSerializer.Deserialize<T>(json);
	}

	private void ClearVisibleNodes(bool isAttrList)
	{
		if (isAttrList)
		{
			foreach (var node in _attrList.GetChildren())
			{
				if (node is HBoxContainer { Visible: true })
				{
					_attrList.RemoveChild(node);
					node.QueueFree();
				}
			}
		}
		else
		{
			foreach (var node in _attrItemChose.GetChildren())
			{
				if (node is Panel { Visible: true })
				{
					_attrItemChose.RemoveChild(node);
					node.QueueFree();
				}
			}
		}
	}

	private void SetAttrItemProperties(Panel attrItem, AttrType type, PlayerAttr attr, AttrData attrData)
	{
		attrItem.GetNode<TextureRect>("MarginContainer/HBoxContainer/TextureRect").Texture =
			GD.Load<Texture2D>($"res://scenes/UppateScene/assets/{type.Img}.png");
		attrItem.GetNode<Label>("MarginContainer/HBoxContainer/VBoxContainer/Label").Text = attr.Name;

		var range = attrData.Range.Split('-');
		var attrRandomValue = GD.RandRange(int.Parse(range[0]), int.Parse(range[1]));
		attrItem.GetNode<RichTextLabel>("RichTextLabel").Text = $"[color=green]+{attrRandomValue}[/color] {type.Name}";
	}

}
