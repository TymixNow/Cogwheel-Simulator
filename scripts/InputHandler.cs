using Godot;
using System;

public partial class InputHandler : Node
{
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton && @event.IsPressed())
		{
			MouseButton button = mouseButton.ButtonIndex;
			switch (button)
			{
				case MouseButton.WheelUp:
					Defs.Global.toolbar.Index++;
					break;
				case MouseButton.WheelDown:
					Defs.Global.toolbar.Index--;
					break;
			}
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
