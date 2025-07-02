using System;
using Godot;

namespace Hex
{
	public class Vector
	{
		public Vector2I Narrow
		{
			get
			{
				return new Vector2I(NX, Y);
			}
			set
			{
				Y = value.Y;
				NX = value.X;
				WX = NX + Y;  //IMPORTANT FORMULA
			}
		}
		public Vector2I Wide
		{
			get
			{
				return new Vector2I(WX, Y);
			}
			set
			{
				Y = value.Y;
				WX = value.X;
				NX = WX - Y;
			}
		}
		public int WX { get; private set; }
		public int NX { get; private set; }
		public int Y { get; private set; }
		

		public static Vector MakeNarrow(int x, int y)
		{
			Vector2I input = new(x, y);
			Vector output = new();
			output.Narrow = input;
			return output;
		}

		public static Vector MakeWide(int x, int y)
		{
			Vector2I input = new(x, y);
			Vector output = new();
			output.Wide = input;
			return output;
		}

		public static implicit operator Vector2(Hex.Vector hex)
		{
			return new Vector2(1,0)*hex.NX + new Vector2(1,0).Rotated((float)(Math.PI/3))*hex.Y;
		}

		public Godot.Vector2 Brick()
		{
			return new Vector2((NX + WX)*0.5f, Y*0.75f);
		}

		public int Taxi()
		{
			int a = Math.Abs(WX);
			int b = Math.Abs(NX);
			int c = Math.Abs(Y);
			return a + b + c - MoreMath.Max(a, b, c);
		}

		public static Vector operator +(Vector vector)
		{
			return vector;
		}

		public static Vector operator -(Vector vector)
		{
			return MakeNarrow(-vector.NX, -vector.Y);
		}

		public static Vector operator +(Vector a, Vector b)
		{
			return MakeNarrow(a.NX + b.NX, a.Y + b.Y);
		}

		public static Vector operator -(Vector a, Vector b)
		{
			return a + (-b);
		}

		public static Vector operator *(Vector a, int b)
		{
			return MakeNarrow(a.NX *b,a.Y *b);
		}

		public override int GetHashCode()
		{
			return this.Narrow.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case null:
					return false;
				case Vector vector:
					return this.Narrow.Equals(vector.Narrow);
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
