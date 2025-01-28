using Godot;
using System;

public partial class GenerateBoard : Node
{
	public PackedScene tile;
	public void Generate(int radius)
	{
		for (int x = -radius; x <= radius; x++)
		{
			for(int z = -radius; z <= radius; z++)
			{
				if(-radius <= x+z && x+z <= radius)
				{
					PlaceTile(x,z);
				}
			}			
		}
	}

	public void PlaceTile(int x, int z)
	{
		Node2D tileInstance = tile.Instantiate<Node2D>();
		AddChild(tileInstance);
		Vector2 position_int = Hex.Vector.Narrow(x,z).Brick();
		Texture2D texture = tileInstance.GetChild<Sprite2D>(0).Texture;
		Vector2 size = new(texture.GetWidth(),texture.GetHeight());
		tileInstance.Position += position_int * size;
	}

	public override void _Ready()
	{
		tile = GD.Load<PackedScene>("res://tiles/hex_tile.tscn");
		Generate(3);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
