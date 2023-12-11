using System.Linq;
using System.Text.Json;
using BrotatoLikeTeach.tool;
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
        Init();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    private void Init()
    {
        GenAttrChoose();
    }

    private void GenAttrChoose()
    {
        using var dataFileAccess = FileAccess.Open("res://playerattributes/AttrData.json", FileAccess.ModeFlags.Read);
        using var attrFileAccess = FileAccess.Open("res://playerattributes/PlayerAttr.json", FileAccess.ModeFlags.Read);
        var dataJson = dataFileAccess.GetAsText();
        var attrJson = attrFileAccess.GetAsText();
        var attrDataDic = JsonSerializer.Deserialize(dataJson, SourceGenerationContext.Default.DictionaryStringAttrData);
        var attrDic = JsonSerializer.Deserialize(attrJson, SourceGenerationContext.Default.DictionaryStringPlayerAttr);
        foreach (var node in _attrItemChose.GetChildren())
            if (node is Panel { Visible: true })
            {
                _attrItemChose.RemoveChild(node);
                node.QueueFree();
            }

        for (var i = 0; i < 4; i++)
        {
            var attrItem = (Panel)_attrItemTemplate.Duplicate();
            attrItem.Show();

            var dataKeyList = attrDataDic.Keys.ToList();
            var dataKey = GD.RandRange(0, dataKeyList.Count - 1);
            var attrData = attrDataDic[dataKeyList[dataKey]];
            var attr = attrDic[attrData.Group];
            var type = attr.Type.FirstOrDefault(x => x.Id == attrData.Type);

            attrItem!.GetNode<TextureRect>("MarginContainer/HBoxContainer/TextureRect").Texture =
                GD.Load<Texture2D>($"res://scenes/UppateScene/assets/{type!.Img}.png");
            attrItem!.GetNode<Label>("MarginContainer/HBoxContainer/VBoxContainer/Label").Text = attr.Name;

            var range = attrData.Range.Split('-');
            var attrRandomValue = GD.RandRange(int.Parse(range[0]), int.Parse(range[1]));
            attrItem.GetNode<RichTextLabel>("RichTextLabel").Text = $"[color=green]+{attrRandomValue}[/color] {type.Name}";

            attrItem.GetNode<Button>("Button").Pressed += () => { GenAttrChoose(); };

            _attrItemChose.AddChild(attrItem);
        }
    }
}