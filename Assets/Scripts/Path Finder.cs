using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    GridManager gridmanager;
    [SerializeField] Vector2Int start_coordinates;
    public Vector2Int Start_coordinates { get { return start_coordinates; } }
    [SerializeField] Vector2Int destination_coordinates;
    public Vector2Int Destination_coordinates { get { return destination_coordinates; }}
    
    Node starting_node;
    Node destination_node;
    [SerializeField] Node currentNode;
    Vector2Int[] directions = {Vector2Int.left,Vector2Int.right,Vector2Int.up,Vector2Int.down};
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> Explored_grid = new Dictionary<Vector2Int, Node>();
    Queue<Node> queue = new Queue<Node>();


    void Awake()
    {
        gridmanager = FindObjectOfType<GridManager>();
        if (gridmanager != null)
        {
            grid = gridmanager.Grid;
            starting_node = grid[start_coordinates];
            destination_node = grid[destination_coordinates];
        }
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("FindPath", false, SendMessageOptions.DontRequireReceiver);
    }

    private void Start()
    {
        Get_new_path();
    }

    public List<Node> Get_new_path() {
        return Get_new_path(start_coordinates);
    }

    public List<Node> Get_new_path(Vector2Int coordinates)
    {
        gridmanager.ResetNodes();
        Apply_bfs(coordinates);
        return BuildPath();
    }



    private void ExploreNeighbours()
    {
        List<Node> neighbour_nodes = new List<Node>();
        foreach (Vector2Int direction in directions)
        {
            Vector2Int current_neighbour_node = currentNode.coordinates + direction;
            if (grid.ContainsKey(current_neighbour_node))
            {
                neighbour_nodes.Add(grid[current_neighbour_node]);
            }
        }

        foreach (Node neighbor in neighbour_nodes)
        {
            if (!Explored_grid.ContainsKey(neighbor.coordinates) && neighbor.is_walkable)
            {
                neighbor.connectedTo = currentNode;
                Explored_grid.Add(neighbor.coordinates, neighbor);
                queue.Enqueue(neighbor);
            }
        }

    }


    public void Apply_bfs(Vector2Int coordinates)
    {
        starting_node.is_walkable = true;
        destination_node.is_walkable = true;
        queue.Clear();
        Explored_grid.Clear();
        bool is_running = true;
        queue.Enqueue(grid[coordinates]);
        Explored_grid.Add(coordinates, grid[coordinates]);
        while (queue.Count != 0 && is_running == true)
        {
            currentNode = queue.Dequeue();
            currentNode.is_explored = true;
            ExploreNeighbours();
            if(currentNode == destination_node)
            {
                is_running = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destination_node;
        path.Add(currentNode);
        currentNode.is_path = true;

        while (currentNode != starting_node) {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            if (currentNode == null) { return path;}
            currentNode.is_path = true;
            
        }
        path.Reverse();
        return path;
    }

    public bool Will_block_path(Vector2Int coordinates)
    {

        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].is_walkable;
            grid[coordinates].is_walkable = false;
            List<Node> newPath = Get_new_path();
            grid[coordinates].is_walkable = previousState;
            if (newPath.Count <= 2)
            {
                Get_new_path();
                return true;
            }
        }
        return false;
    }

}
