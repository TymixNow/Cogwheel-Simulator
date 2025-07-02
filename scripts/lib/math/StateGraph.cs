using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;
public class Enum<T>(T number)
{
    public T Value { get; private set; } = number;

    public static implicit operator Enum<T>(T x) => new(x);
    public static implicit operator T(Enum<T> x) => x.Value;
}
public abstract class GraphState(int number) : Enum<int>(number)
{
    public abstract StateGraph Graph { get; }
    public GraphState Apply(GraphAction action)
    {
        return Graph.Apply(this, action);
    }
    public static GraphState operator+ (GraphState state, GraphAction action)
        => state.Apply(action);
}
public abstract class GraphAction(int number) : Enum<int>(number){}
public class StateGraph(Dictionary<(GraphState, GraphAction), GraphState> matrix)
{
    public Dictionary<(GraphState, GraphAction), GraphState> matrix = matrix;
    public GraphState Apply(GraphState state, GraphAction action)
    {
        return matrix.GetValueOrDefault((state, action));
    }
    protected void AddData(GraphState input, GraphAction action, GraphState output)
    {
        matrix.Add((input, action), output);
    }
    public static StateGraph operator+ (StateGraph data, (GraphState input, GraphAction action, GraphState output) change)
    {
        StateGraph data2 = data;
        data2.AddData(change.input, change.action, change.output);
        return data2;
    }
}
