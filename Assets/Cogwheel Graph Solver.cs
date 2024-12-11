using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System.Linq;

public struct WeightedDirectedGraph<T>
{
    public List<int> verts;
    public List<(int start, int end, T weight)> edges;
    public List<(int neighbour, bool outward, T weight)> GetNeighbours(int vert)
    {
        List<(int neighbour, bool outward, T weight)> output = new();
        for (int i = 0; i < edges.Count; i++)
        {
            if (edges[i].start == vert)
            {
                output.Add((edges[i].end,true,edges[i].weight));
            }
            else if (edges[i].end == vert)
            {
                output.Add((edges[i].start, false, edges[i].weight));
            }
        }
        return output;
    }
}

public class SignedPowTwo
{
    public SignedPowTwo(int exp, bool sign)
    {
        this.exp = exp;
        this.sign = sign;
    }
    public int exp;
    public bool sign;
    public float Value()
    {
        return sign ? -Mathf.Pow(2, exp) : Mathf.Pow(2, exp);
    }
    public static SignedPowTwo operator *(SignedPowTwo num, SignedPowTwo mult)
    {
        return new SignedPowTwo(num.exp + mult.exp, num.sign ^ num.sign);
    }
    public static SignedPowTwo operator /(SignedPowTwo num, SignedPowTwo mult)
    {
        return new SignedPowTwo(num.exp - mult.exp, num.sign ^ num.sign);
    }
}

public class CogwheelGraphSolver
{
    public bool TryGetSpeeds(WeightedDirectedGraph<SignedPowTwo> graph, int power, out List<SignedPowTwo> speeds)
    {
        speeds = new(graph.verts.Count);
        List<bool> tested = new(graph.verts.Count);
        for (int i = 0; i < graph.verts.Count; i++)
        {
            tested[i] = false;
        }
        tested[power] = true;
        speeds[power] = new SignedPowTwo(0,false);
        for (int i = 0; i <= graph.verts.Count; i++)
        {
            for (int vertind = 0; vertind < speeds.Count; vertind++)
            {
                if (tested[vertind])
                {
                    foreach (var neigh in graph.GetNeighbours(vertind))
                    {
                        if (tested[neigh.neighbour])
                        {
                            if (neigh.outward)
                            {
                                if ((speeds[vertind] * neigh.weight) != (speeds[neigh.neighbour]))
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (speeds[neigh.neighbour] * neigh.weight != speeds[vertind])
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (neigh.outward)
                            {
                                speeds[neigh.neighbour] = speeds[vertind] * neigh.weight;
                            }
                            else
                            {
                                speeds[neigh.neighbour] = speeds[vertind] / neigh.weight;
                            }
                        }
                    }
                }
            }
        }
        return true;
    }
}
