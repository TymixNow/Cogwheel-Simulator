using Components;
using Godot;
using System;
using System.Collections.Generic;

public sealed class TileLayerExistenceState(int number) : Enum<int>(number)
{
	public static implicit operator TileLayerExistenceState(int x) => new(x);
	public static implicit operator int(TileLayerExistenceState x) => x.Value;
	public static StateGraph graph;
	public static TileLayerExistenceState
	Off = (TileLayerExistenceState)0,
	Add = (TileLayerExistenceState)1,
	On = (TileLayerExistenceState)2,
	Remove = (TileLayerExistenceState)3;

}

public partial class TileLayer : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		existenceState = TileLayerExistenceState.Off;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetTexture(Texture2D texture)
	{
		foreach (Sprite2D sprite in GetChildren())
		{
			sprite.Texture = texture;
		}
		SetExistenceState(TileLayerExistenceState.Off);
	}

	private TileLayerExistenceState existenceState;
	public TileLayerExistenceState ExistenceState
	{
		get {return existenceState;}
		set {SetExistenceState(value);}
	} 

	private void SetExistenceState(TileLayerExistenceState state)
	{
		existenceState = state;
		switch (state.Value)
		{
			case 0:
				KeepRemove();
				break;
			case 1:
				PreviewAdd();
				break;
			case 2:
				KeepAdd();
				break;
			case 3:
				PreviewRemove();
				break;
		}
	}
	private void PreviewAdd()
	{
		GetNode<Sprite2D>("./add").Visible = true;
		GetNode<Sprite2D>("./keep").Visible = false;
		GetNode<Sprite2D>("./remove").Visible = false;
	}

	private void KeepAdd()
	{
		GetNode<Sprite2D>("./add").Visible = true;
		GetNode<Sprite2D>("./keep").Visible = true;
		GetNode<Sprite2D>("./remove").Visible = false;
	}

	private void PreviewRemove()
	{
		GetNode<Sprite2D>("./add").Visible = true;
		GetNode<Sprite2D>("./keep").Visible = true;
		GetNode<Sprite2D>("./remove").Visible = true;
	}

	private void KeepRemove()
	{
		GetNode<Sprite2D>("./add").Visible = false;
		GetNode<Sprite2D>("./keep").Visible = false;
		GetNode<Sprite2D>("./remove").Visible = false;
	}
}
