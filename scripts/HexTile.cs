using Components;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class HexTile : Node2D
{
	private ComponentTree tree;
	public Component currentComponent;
	private PackedScene layerPacked;
	private Node2D layerContainer;
	private bool selected;
	private Component prediction;
	private List<string> OffTextures, AddTextures, OnTextures, RemoveTextures;
	private void SetExistenceState(string component, TileLayerExistenceState state)
	{
		GetNode<TileLayer>(Defs.Defs.TileLayerContainerRelPath + "/" + component).ExistenceState = state;
	}
	public void PopulateTextureLists()
	{
		prediction = Component.graph.Apply(currentComponent, Defs.Global.CurrentTileTool);
		OffTextures = [];
		AddTextures = [];
		OnTextures = [];
		RemoveTextures = [];
		foreach (string textureName in Component.graph.textureNames)
		{
			int curr = currentComponent.parts.Contains(textureName)? 1:0;
			int pred = prediction.parts.Contains(textureName)? 1:0;
			switch (pred + (2 * curr))
			{
				case 0:
					OffTextures.Add(textureName);
					break;
				case 1:
					AddTextures.Add(textureName);
					break;
				case 2:
					RemoveTextures.Add(textureName);
					break;
				case 3:
					OnTextures.Add(textureName);
					break;
			}
		}
	}
	public void OnChanged(MouseButton mouseButton = MouseButton.None)
	{
		PopulateTextureLists();
		foreach (string texture in OnTextures)
			SetExistenceState(texture, TileLayerExistenceState.On);
		foreach (string texture in OffTextures)
			SetExistenceState(texture, TileLayerExistenceState.Off);
		if (selected)
		{
			foreach (string texture in AddTextures)
				SetExistenceState(texture, TileLayerExistenceState.Add);
			foreach (string texture in RemoveTextures)
				SetExistenceState(texture, TileLayerExistenceState.Remove);
			if (mouseButton == MouseButton.Left)
			{
				currentComponent = prediction;
				OnChanged();
			}
		}
		else
		{
			foreach (string texture in AddTextures)
				SetExistenceState(texture, TileLayerExistenceState.Off);
			foreach (string texture in RemoveTextures)
				SetExistenceState(texture, TileLayerExistenceState.On);
		}
	}
	public void OnAreaEntered()
	{
		selected = true;
		OnChanged();
	}

	public void OnAreaExited()
	{
		selected = false;
		OnChanged();
	}

	public void OnEvent(Node viewport, InputEvent @event, int shape_idx)
	{
		if (@event is InputEventMouseButton mouseButton && @event.IsPressed())
		{
			OnChanged(mouseButton.ButtonIndex);
		}
	}
	public void SetTextures()
	{
		foreach (string textureName in Component.graph.textureNames)
		{
			TileLayer instance = layerPacked.Instantiate<TileLayer>();
			instance.Name = textureName;
			instance.SetTexture(Component.graph.textures[Component.graph.textureNames.IndexOf(textureName)]);
			layerContainer.AddChild(instance);
		}
		PopulateTextureLists();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		tree = Component.graph;
		currentComponent = tree.root;
		layerPacked = GD.Load<PackedScene>(Defs.Defs.TileLayerPath);
		layerContainer = GetNode<Node2D>(Defs.Defs.TileLayerContainerRelPath);
		selected = false;
		SetTextures();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
