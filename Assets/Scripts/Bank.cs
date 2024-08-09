using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class Bank : MonoBehaviour
{
    [SerializeField] int current_balance = 150;
    [SerializeField] TextMeshProUGUI Balance;
    public int Current_balance { get { return current_balance; } }


    private void Start()
    {
        Update_display();
    }

    public void Deposit(int amount)
    {
        current_balance += amount;
        Update_display();
    }

    public void Withdraw(int amount)
    {
        current_balance -= amount;
        Update_display();
        if (current_balance < 0)
        {
            Reload_scene();
        }
        
    }

    private void Reload_scene()
    { 
            Scene active_scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(active_scene.buildIndex); 
    }

    private void Update_display()
    {
        Balance.text = "Gold : " + current_balance.ToString("0000000");
    }
}
