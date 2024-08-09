using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class Enemyhealthcontroller : MonoBehaviour
{
    
    [SerializeField] public int Enemys_health = 5;
    [SerializeField] int bullets_damage = 1;
    [Tooltip("Increases difficulty")]
    [SerializeField] int enemy_ramp = 1;
    int current_health;
    private void OnEnable()
    {
    current_health = Enemys_health;   
    }
    private void OnParticleCollision(GameObject other)
    {
        current_health -= bullets_damage;
        if(current_health == 0)
        {
            
            FindObjectOfType<Enemy>().reward_player();
            Enemys_health += enemy_ramp;
            gameObject.SetActive(false);  
        }
    }
}
