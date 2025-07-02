using Godot;
using System;
public partial class GenerateBoard : TO_BE_REMOVED.TempFix.FramedNode
{
	public PackedScene tile;
	public void Generate(int radius)
	{
		for (int x = -radius; x <= radius; x++)
		{
			for(int z = -radius; z <= radius; z++)
			{
				Hex.Vector vector = Hex.Vector.MakeNarrow(x, z);
				if (vector.Taxi() < radius)
				{
					PlaceTile(vector);
				}
			}			
		}
	}

	public void PlaceTile(Hex.Vector vector)
	{
		Node2D tileInstance = tile.Instantiate<Node2D>();
		AddChild(tileInstance);
		Vector2 position_int = vector.Brick();
		Texture2D texture = tileInstance.GetChild<Sprite2D>(0).Texture;
		Vector2 size = new(texture.GetWidth(),texture.GetHeight());
		tileInstance.Position += position_int * size;
	}
	public override void _Ready()
	{
		base._Ready();
		tile = GD.Load<PackedScene>(Defs.Defs.TilePath);
	}
	public override void _FirstFrame()
	{
		Generate(4);
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
