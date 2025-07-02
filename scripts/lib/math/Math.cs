using Godot;
using System;
using System.Linq;

public class MoreMath
{
    public static int Max(params int[] values)
    {
        return Enumerable.Max(values);
    }
}
