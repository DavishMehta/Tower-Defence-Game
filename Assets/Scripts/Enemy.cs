using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int gold_reward = 25;
    [SerializeField] int gold_penalty = 25;
    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();  
    }

    public void reward_player()
    {
        if (bank == null) return;
        bank.Deposit(gold_reward);
    }

    public void penalise_player()
    {
        if (bank == null) return;
        bank.Withdraw(gold_penalty);
    }

}
