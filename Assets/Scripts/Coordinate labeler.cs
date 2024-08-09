using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class Coordinatelabeler : MonoBehaviour
{
    TextMeshPro label;
    GridManager gridmanager;
    Vector2Int coordinates = new Vector2Int();
    [SerializeField] Color white_color = Color.white;
    [SerializeField] Color grey_color = Color.grey;
    public Vector2Int Coordinates { get { return coordinates; } }
    float grid_size = 1.05f;
    
    void Awake()
    {
        gridmanager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        Display_coordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            Display_coordinates();
            Update_object_name();
        }
        Color_handler();
        toggle_labler();
    }

    void toggle_labler() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            label.enabled = !label.IsActive();
        }
    }

    void Display_coordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x/grid_size);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z/grid_size);
        label.text = coordinates.x + "," + coordinates.y;
    }


    private void Color_handler()
    {
        if (!gridmanager) return;
        if (coordinates == null) return;
        Node node = gridmanager.Get_node(coordinates);
        if (node == null) return;
        if (node.is_path) {
            label.color = Color.red;
        }
        else if (node.is_explored)
        {
            label.color = Color.black;
        }
        else if (node.is_walkable)
        {
            label.color = Color.green;
        }
        else{
            label.color = Color.white;
        }
    } 

    void Update_object_name()
    {
        transform.parent.name = coordinates.x + "," + coordinates.y;
    }
}
