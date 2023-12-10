using Godot;
using System;

public partial class SceneUpdate : CanvasLayer
{

	private GridContainer _attrItemChose;
	private Panel _attrItemTemplate;
	private VBoxContainer _attrList;
	private HBoxContainer _attrTemplate;

	private Button _refresh;

	private Button _update;
	private Button _continue;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_attrItemChose = GetNode<GridContainer>("AttrItemChose");
		_attrItemTemplate = GetNode<Panel>("AttrItemChose/AttrItemTemplate");
		_attrList = GetNode<VBoxContainer>("AttrValuePanel/MarginContainer/ScrollContainer/AttrList");
		_attrTemplate = GetNode<HBoxContainer>("AttrValuePanel/MarginContainer/ScrollContainer/AttrList/AttrTemplate");
		_refresh = GetNode<Button>("Refresh");
		_update = GetNode<Button>("Update");
		_continue = GetNode<Button>("Continue");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
