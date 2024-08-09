using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavepoints : MonoBehaviour
{
    [SerializeField] GameObject shooting_tower;
    [SerializeField] bool is_placable;
    public bool Is_placable { get { return is_placable; } }
    GameObject parent;
    Bank bank;
    int cost = 75;
    GridManager gridManager;
    PathFinder pathfinder;
    Vector2Int coordinates;

    private void Awake()
    {
        parent = GameObject.FindWithTag("tower_keeper");
        bank = FindObjectOfType<Bank>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();
        coordinates = gridManager.getcoordinatefromposition(transform.position);
    }

    private void OnMouseDown()
    {
        if (bank.Current_balance >= cost)
            {
            if (coordinates == pathfinder.Start_coordinates) return;
            if (coordinates == pathfinder.Destination_coordinates) return;
            if (gridManager.Get_node(coordinates).is_walkable && pathfinder.Will_block_path(coordinates) == false)
            {
                Block_nodes(coordinates);
                Instantiate_towers();
                pathfinder.NotifyReceivers();
            }
        }
    }

    private void Block_nodes(Vector2Int coordinates)
    {
        if (gridManager.Grid.ContainsKey(coordinates))
        {
            gridManager.Grid[coordinates].is_walkable = false;
        }
    }


    private void Start()
    {
            
            if (!is_placable)
            {
            Block_nodes(coordinates);
            }
    }

    private void Instantiate_towers()
    {
        GameObject Instantiated_towers = Instantiate(shooting_tower, transform.position, Quaternion.identity);
        Instantiated_towers.transform.parent = parent.transform;
        bank.Withdraw(cost);
        is_placable = false;
    }
}

