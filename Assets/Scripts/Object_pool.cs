using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_pool_container : MonoBehaviour
{
    [SerializeField] GameObject enemys_prefab;
    [SerializeField] int pool_size = 5;
    [SerializeField] [Range(0.1f,5f)]float spawn_timer = 2f;
    GameObject[] Object_pool;

    private void Awake()
    {
        Object_pool = new GameObject[pool_size];
        for (int i = 0;i<pool_size;i++)
        {
            Object_pool[i] = Instantiate(enemys_prefab, transform);
            Object_pool[i].SetActive(false);
        }
    }

    void EnemysSetActiveFunction()
    {
        for (int i = 0;i<pool_size;i++)
        {
            if (!Object_pool[i].activeInHierarchy)
            {
                Object_pool[i].SetActive(true);
                return;
            }
        }
    }

    void Start()
    {
        StartCoroutine(object_pool());
    }

    IEnumerator object_pool() {
        while (true)
        {
            yield return new WaitForSecondsRealtime(spawn_timer);
            EnemysSetActiveFunction();
            
        }
    }
}
