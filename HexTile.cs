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

	public void OnEvent(Node viewport, InputEvent @event, int shape_idx)
	{
		if (@event is InputEventMouseButton mouseButton && @event.IsPressed())
		{
			if (mouseButton.ButtonIndex == MouseButton.Left)
			{
				OnClick();
			}
		}
	}

	public void OnClick()
	{
		bool selected = GetNode<Sprite2D>("./shaft/preview").Visible;
		if (selected)
		{
			GetNode<Sprite2D>("./shaft/real").Visible ^= true;
		}
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
