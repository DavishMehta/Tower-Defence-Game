using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_rotation_handler : MonoBehaviour
{
    Transform target_position;
    [SerializeField] Transform tower_top;
    [SerializeField] ParticleSystem bullets;
    [SerializeField] float range = 1f;


    void Update()
    {
        FindClosestEnemy();
        AimWeapon();   
    }

    void FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closest_target = null;
        float max_distance = Mathf.Infinity;
        
        foreach(Enemy enemy in enemies) {
            float distance = Vector3.Distance(transform.position,enemy.transform.position);
            if (distance < max_distance)
            {
                closest_target = enemy.transform;
                max_distance = distance;
            }

        }
        target_position = closest_target;

    }

    void AimWeapon() {
        if (target_position != null)
        {
            float distance = Vector3.Distance(transform.position, target_position.position);
            if (distance < range)
            {
                var emmisiomodule = bullets.emission;
                emmisiomodule.enabled = true;

            }
            else
            {
                var emmisiomodule = bullets.emission;
                emmisiomodule.enabled = false;
            }
            tower_top.LookAt(target_position);
        }
    }

}
