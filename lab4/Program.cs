using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Dictionary<int, HashSet<int>> graph = new Dictionary<int, HashSet<int>>();
    static int nextVertexId = 1000; 

    static void Main()
    {
        AddEdge(1, 2);
        AddEdge(2, 3);
        AddEdge(3, 1);

        AddEdge(3, 4);

        AddEdge(4, 5);
        AddEdge(5, 6);
        AddEdge(6, 4);

        Console.WriteLine("Исходный граф:");
        PrintGraph();

        while (TryFindAndGlueTriangle())
        {
            Console.WriteLine("\nГраф после склеивания треугольника:");
            PrintGraph();
        }

        Console.WriteLine("\nГраф без треугольников:");
        PrintGraph();
    }
    static void AddEdge(int u, int v)
    {
        if (!graph.ContainsKey(u))
            graph[u] = new HashSet<int>();
        if (!graph.ContainsKey(v))
            graph[v] = new HashSet<int>();

        graph[u].Add(v);
        graph[v].Add(u);
    }
    static bool TryFindAndGlueTriangle()
    {
        var vertices = graph.Keys.ToList();

        for (int i = 0; i < vertices.Count; i++)
        {
            for (int j = i + 1; j < vertices.Count; j++)
            {
                for (int k = j + 1; k < vertices.Count; k++)
                {
                    int a = vertices[i], b = vertices[j], c = vertices[k];

                    if (IsTriangle(a, b, c))
                    {
                        GlueTriangle(a, b, c);
                        return true;
                    }
                }
            }
        }

        return false; 
    }
    static bool IsTriangle(int a, int b, int c)
    {
        return graph[a].Contains(b) && graph[b].Contains(c) &&
               graph[c].Contains(a) && graph[a].Contains(c) && graph[b].Contains(a) && graph[c].Contains(b);
    }
    static void GlueTriangle(int a, int b, int c)
    {
        var triangle = new HashSet<int> { a, b, c };
        var newVertex = nextVertexId++;
        var neighbors = new HashSet<int>();

        foreach (var v in triangle)
        {
            foreach (var neighbor in graph[v])
            {
                if (!triangle.Contains(neighbor))
                    neighbors.Add(neighbor);
            }
        }
        graph[newVertex] = new HashSet<int>();
        foreach (var n in neighbors)
        {
            graph[newVertex].Add(n);
            graph[n].Add(newVertex);
        }
        foreach (var v in triangle)
        {
            foreach (var neighbor in graph[v])
            {
                graph[neighbor].Remove(v);
            }
            graph.Remove(v);
        }

        Console.WriteLine($"\nСклеен треугольник: {a}, {b}, {c} -> {newVertex}");
    }
    static void PrintGraph()
    {
        foreach (var vertex in graph)
        {
            Console.Write($"{vertex.Key}: ");
            Console.WriteLine(string.Join(", ", vertex.Value));
        }
    }
}