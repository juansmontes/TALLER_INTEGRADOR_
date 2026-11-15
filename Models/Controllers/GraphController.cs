
using System;
using System.Collections.Generic;
using CampusNet.Models;

namespace CampusNet.Controllers;

public sealed class GraphController
{
    private readonly Graph graph;

    public GraphController(Graph graph)
    {
        this.graph = graph;
    }

    public bool CreateUser(string id, string name, string role)
    {
        try
        {
            return graph.AddVertex(new Vertex(id, name, role));
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    public bool DeleteUser(string id)
    {
        return graph.RemoveVertex(id);
    }

    public bool Follow(string fromId, string toId)
    {
        return graph.AddEdge(fromId, toId);
    }

    public bool Unfollow(string fromId, string toId)
    {
        return graph.RemoveEdge(fromId, toId);
    }

    public IReadOnlyList<string> BreadthFirstSearch(string startId)
    {
        return graph.BFS(startId);
    }

    public IReadOnlyList<string> DepthFirstSearch(string startId)
    {
        return graph.DFS(startId);
    }

    public int GetFollowersCount(string id)
    {
        return graph.InDegree(id);
    }

    public int GetFollowingCount(string id)
    {
        return graph.OutDegree(id);
    }

    public int GetUserCount()
    {
        return graph.VertexCount;
    }

    public int GetRelationshipCount()
    {
        return graph.EdgeCount;
    }
}
