using Godot;
using System;

public partial class HexTile : Node2D
{
	public void OnAreaEntered()
	{
		GetNode<Sprite2D>("./shaft/preview").Visible = true;
	}

	public void OnAreaExited()
	{
		GetNode<Sprite2D>("./shaft/preview").Visible = false;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
