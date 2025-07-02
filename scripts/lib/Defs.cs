using Godot;
using System;
using System.Collections.Generic;

namespace Defs
{
    public class Defs
    {
        public const string TileLayerContainerRelPath = "./Layers";
        public const string TileLayerPath = "res://tile_layer.tscn";
        public const string TilePath = "res://tile.tscn";
        public const string ToolbarElementPath = "res://toolbar_element.tscn";
    }
    public class Textures
    {
        public static string GetTexture(string name)
        {
            return "res://textures/" + name + ".svg";
        }
    }
}
