using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int grid_size;
    float world_grid_size = 1.05f;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    public Node Get_node(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
    }

    void Awake()
    {
        CreateGrid();   
    }

    public Vector2Int getcoordinatefromposition(Vector3 position)
    {
        Vector2Int coordinate = new Vector2Int();
        coordinate.x = Mathf.RoundToInt(position.x/world_grid_size);
        coordinate.y = Mathf.RoundToInt(position.z/world_grid_size);
        return coordinate;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * world_grid_size;
        position.z = coordinates.y * world_grid_size;
        return position;
    }


    public void ResetNodes() {
        foreach(KeyValuePair<Vector2Int,Node> node in grid) {
            node.Value.is_explored = false;
            node.Value.is_path = false;
            node.Value.connectedTo = null;
        }
    }


    void CreateGrid()
    {
        for(int x = 0; x < grid_size.x; x++) {
            for(int y = 0; y < grid_size.y; y++) {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }  
    }
}
