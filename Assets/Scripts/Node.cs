using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool is_walkable;
    public bool is_path;
    public bool is_explored;
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool is_walkable)
    {
        this.coordinates = coordinates;
        this.is_walkable = is_walkable;
    }
}
