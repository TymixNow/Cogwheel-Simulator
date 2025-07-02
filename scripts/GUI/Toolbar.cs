using Godot;
using System;
using Components;
using System.Collections.Generic;
using System.Linq;

public partial class Toolbar : HBoxContainer
{
	private int index;
	public int Index
	{
		get { return index; }
		set
		{
			index = value;
			index %= tools.Count;
			index += tools.Count;
			index %= tools.Count;
			Select(index);
		}
	}
	public List<TileTool> tools;
	public TileTool Tool()
	{
		return tools[index];
	}
	private void Select(int ind)
	{
		foreach (ToolbarElement child in GetChildren().Cast<ToolbarElement>())
		{
			child.GetNode<TextureRect>("./ToolbarSelector").Visible = child.index == ind;
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Defs.Global.toolbar = this;
		tools ??= [];
	}
	public void OnToolsListed()
	{
		PackedScene element = GD.Load<PackedScene>(Defs.Defs.ToolbarElementPath);
		for (int i = 0; i <= 8; i++)
		{
			ToolbarElement elementInstance = element.Instantiate<ToolbarElement>();
			AddChild(elementInstance);
			elementInstance.index = i;
			if (i < tools.Count)
			{
				elementInstance.GetNode<TextureRect>("./ToolTexture").Texture = GD.Load<Texture2D>(Defs.Textures.GetTexture(tools[i].name));
			}
		}
		index = 0;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
