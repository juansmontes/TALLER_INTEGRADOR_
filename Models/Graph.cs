
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampusNet.Models;

public sealed class Graph
{
    private readonly Dictionary<string, Vertex> vertices = new();
    private readonly Dictionary<string, HashSet<string>> adjacency = new();

    public IReadOnlyCollection<Vertex> Vertices => vertices.Values.ToList();

    public int VertexCount => vertices.Count;

    public int EdgeCount => adjacency.Values.Sum(x => x.Count);

    public bool AddVertex(Vertex vertex)
    {
        if (vertices.ContainsKey(vertex.Id))
            return false;

        vertices.Add(vertex.Id, vertex);
        adjacency.Add(vertex.Id, new HashSet<string>());
        return true;
    }

    public bool RemoveVertex(string id)
    {
        if (!vertices.Remove(id))
            return false;

        adjacency.Remove(id);

        foreach (var neighbors in adjacency.Values)
            neighbors.Remove(id);

        return true;
    }

    public bool AddEdge(string fromId, string toId)
    {
        if (!vertices.ContainsKey(fromId) ||
            !vertices.ContainsKey(toId))
            return false;

        return adjacency[fromId].Add(toId);
    }

    public bool RemoveEdge(string fromId, string toId)
    {
        if (!adjacency.ContainsKey(fromId))
            return false;

        return adjacency[fromId].Remove(toId);
    }

    public int OutDegree(string id)
    {
        return adjacency.TryGetValue(id, out var neighbors)
            ? neighbors.Count : 0;
    }

    public int InDegree(string id)
    {
        return adjacency.Values.Count(x => x.Contains(id));
    }

    public IReadOnlyCollection<string> Neighbors(string id)
    {
        return adjacency.TryGetValue(id, out var neighbors)
            ? neighbors.ToList() : Array.Empty<string>();
    }

    public IReadOnlyList<string> BFS(string startId)
    {
        var result = new List<string>();

        if (!vertices.ContainsKey(startId))
            return result;

        var visited = new HashSet<string> { startId };
        var queue = new Queue<string>();
        queue.Enqueue(startId);

        while (queue.Count > 0)
        {
            string current = queue.Dequeue();
            result.Add(current);

            foreach (var neighbor in adjacency[current].OrderBy(x => x))
            {
                if (visited.Add(neighbor))
                    queue.Enqueue(neighbor);
            }
        }

        return result;
    }

    public IReadOnlyList<string> DFS(string startId)
    {
        var result = new List<string>();
        var visited = new HashSet<string>();

        void Visit(string current)
        {
            visited.Add(current);
            result.Add(current);

            foreach (var neighbor in adjacency[current].OrderBy(x => x))
            {
                if (!visited.Contains(neighbor))
                    Visit(neighbor);
            }
        }

        if (vertices.ContainsKey(startId))
            Visit(startId);

        return result;
    }
}
