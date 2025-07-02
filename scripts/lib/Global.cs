using Components;
using Godot;
using System;

namespace Defs
{
	public partial class Global : GodotObject
	{
		public static Toolbar toolbar;
		public static TileTool CurrentTileTool
		{
			get => toolbar.Tool();
		}
	}
}
