using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy_Heath;

public class Enemy_Heath : MonoBehaviour
{
    public int expReward = 3;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;

    public int currentHeath;
    public int maxHeath;

    private void Start()
    {
        currentHeath = maxHeath;
    }

    public void ChangeHeath( int amount)
    {
        currentHeath += amount;

        if(currentHeath > maxHeath)
        {
            currentHeath = maxHeath;
        }

        else if(currentHeath <=0)
        {
            OnMonsterDefeated(expReward);
            Destroy(gameObject);
        }

    }
}
