using System;
using Godot;

namespace Hex
{
	class Vector
	{
		public Vector2I narrow;
		public Vector2I wide;

		public static Vector Narrow(int x, int y)
		{
			Vector output = new();
			output.narrow.X = x;
			output.narrow.Y = y;
			output.wide.X = x + y;
			output.wide.Y = y;
			return output;
		}

		public static Vector Wide(int x, int y)
		{
			Vector output = new();
			output.narrow.X = x - y;
			output.narrow.Y = y;
			output.wide.X = x;
			output.wide.Y = y;
			return output;
		}

		public static implicit operator Vector2(Hex.Vector hex)
		{
			return new Vector2(1,0)*hex.narrow.X + new Vector2(1,0).Rotated((float)(Math.PI/3))*hex.narrow.Y;
		}

		public Vector2 Brick()
		{
			return new Vector2((narrow.X + wide.X)*0.5f, (narrow.Y + wide.Y)*0.375f);
		}

		public static Vector operator +(Vector vector)
		{
			return vector;
		}

		public static Vector operator -(Vector vector)
		{
			return Narrow(-vector.narrow.X, -vector.narrow.Y);
		}

		public static Vector operator +(Vector a, Vector b)
		{
			return Narrow(a.narrow.X + b.narrow.X, a.narrow.Y + b.narrow.Y);
		}

		public static Vector operator -(Vector a, Vector b)
		{
			return a + (-b);
		}

		public static Vector operator *(Vector a, int b)
		{
			return Narrow(a.narrow.X *b,a.narrow.Y *b);
		}

		public override int GetHashCode()
		{
			return this.narrow.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case null:
					return false;
				case Vector vector:
					return this.narrow.Equals(vector.narrow);
				default:
					return false;
			}
		}

		public static bool operator ==(Vector a, Vector b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Vector a, Vector b)
		{
			return !(a==b);
		}
	}
}
