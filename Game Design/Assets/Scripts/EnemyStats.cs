using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float damage;
    public float range;
    public float speed;
    public float currentWeapon;
    public Levels level;
    void Start()
    {
         health=Random.Range(50,50*level.level);
        if (level.level == 3) currentWeapon = 1;
        else
        currentWeapon = Random.Range(1, level.level);
        speed = Random.Range(0.5f, 5)+level.level/100;
        if(currentWeapon==1)
        {
            damage = 50f+level.level;
            range = 100f+ level.level;

        }
        if(currentWeapon==2)
        {
            damage = 10f+level.level;
            range = 50f+level.level;
        }
    }
}
