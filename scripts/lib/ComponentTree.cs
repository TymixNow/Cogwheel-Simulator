using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Components
{
	public class TileTool : GraphAction
	{
		public TileTool(string s, bool rem = false, int b = 0) : base(b)
		{
			name = s;
			isRemover = rem;
			Defs.Global.toolbar.tools.Add(this);
		}
		public string name;
		public bool isRemover;
	}
	public class Component(string s, List<string> p, int b = 0) : GraphState(b)
	{
		public override StateGraph Graph => graph;
		public static ComponentTree graph;
		public string name = s;
		public List<string> parts = p;
	}

	public partial class ComponentTree : StateGraph
	{
		public List<Texture2D> textures;
		public List<string> textureNames;
		public Component root;
		public ComponentTree(Component component, Dictionary<(GraphState, GraphAction), GraphState> input = null) : base(input)
		{
			if (input == null)
				matrix = [];
			root = component;
			textureNames = [];
			textures = [];
		}
		public Component Apply(Component component, TileTool material)
		{
			return (Component)matrix.GetValueOrDefault((component,material), component);
		}
		public static ComponentTree operator +(ComponentTree data, (Component input, TileTool addition, Component output) change)
		{
			ComponentTree data2 = data;
			data2.AddData(change.input, change.addition, change.output);
			Component newComponent = change.output;
			foreach (string part in newComponent.parts)
			{
				if (!data2.textureNames.Contains(part))
				{
					data2.textures.Add(GD.Load<Texture2D>(Defs.Textures.GetTexture(part)));
					data2.textureNames.Add(part);
				}
			}
			return data2;
		}
	}
}
