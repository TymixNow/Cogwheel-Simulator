using Godot;
using System;
using Components;
using System.Collections.Generic;
using System.Text.Json;

public class BuildingRecipeData
{
	public List<string> Tools { get; set; }
	public Dictionary<string, List<string>> Components { get; set; }
	public List<List<string>> Recipes { get; set; }
	public string Root { get; set; }
}

public partial class ComponentTreeData : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BuildingRecipeData data = JSONReader.Read<BuildingRecipeData>("res://data/recipes.json");

		Dictionary<string, TileTool> tools = [];

		foreach (string tool in data.Tools)
		{
			tools.Add(tool,new TileTool(tool));
		}

		Dictionary<string,Component> components = [];

		foreach (var pair in data.Components)
		{
			components.Add(pair.Key, new Component(pair.Key, pair.Value));
		}

		ComponentTree mainTree = new(components[data.Root]);

		foreach (List<string> recipe in data.Recipes)
		{
			mainTree += (components[recipe[0]], tools[recipe[1]], components[recipe[2]]);
		}
		
		Component.graph = mainTree;
		Defs.Global.toolbar.OnToolsListed();
		Defs.Global.toolbar.Index = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
