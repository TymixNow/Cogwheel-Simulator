using Godot;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TO_BE_REMOVED.TempFix //TODO: 1. Add Content; 2. FIXME; 3. remove;
{
	public abstract partial class FramedNode : Node
	{
		private bool FirstFrame;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			FirstFrame = true;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (FirstFrame)
			{
				FirstFrame = false;
				_FirstFrame();
			}
		}
		public abstract void _FirstFrame();
	}
}