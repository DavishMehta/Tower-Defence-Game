//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyMovement : MonoBehaviour
//{
//    List<Node> Travel_path = new List<Node>();
//    [SerializeField] float enemys_speed = 1f;
//    GridManager gridmanager;
//    PathFinder pathfinder;
//    Enemy enemy;

//    private void Awake()
//    {
//        enemy = GetComponent<Enemy>();
//        gridmanager = FindObjectOfType<GridManager>();
//        pathfinder = FindObjectOfType<PathFinder>();

//    }
//    void OnEnable ()
//    {
//        Findpath();
//        Returntostart();
//        StartCoroutine(Enemy_mover());
//    }

//    void Returntostart()
//    {
//        transform.position = gridmanager.GetPositionFromCoordinates(pathfinder.Start_coordinates);
//    }

//    void Findpath()
//    {
//        Travel_path.Clear();
//        Travel_path = pathfinder.Get_new_path();
//    }

//    IEnumerator Enemy_mover()
//    {
//        for(int i = 0;i<Travel_path.Count;i++)
//        {
//            Vector3 starting_point = transform.position;
//            Vector3 Ending_point = gridmanager.GetPositionFromCoordinates(Travel_path[i].coordinates);
//            float Travel_percent = 0f;
//            while (Travel_percent < 1f)
//            {
//                Travel_percent += Time.deltaTime* enemys_speed;
//                transform.position = Vector3.Lerp(starting_point, Ending_point, Travel_percent);
//                transform.LookAt(Ending_point);
//                yield return new WaitForEndOfFrame();
//            }
//        }

//        path_ending_function();
//    }

//    private void path_ending_function()
//    {
//        gameObject.SetActive(false);
//        GetComponent<Enemy>().penalise_player();
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathfinder;

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();
    }

    void FindPath()
    {
        path.Clear();
        path = pathfinder.Get_new_path();
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.Start_coordinates);
    }

    void FinishPath()
    {
        enemy.penalise_player();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}


