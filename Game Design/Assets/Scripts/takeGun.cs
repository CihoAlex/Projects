using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeGun : MonoBehaviour
{
    public Stats stats;
    public Levels level;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            stats.haveWeapon1 = true;
            Destroy(gameObject);
        }
    }
    void Update()

    {
        if (level.reset == 1)
        {
            if (this.name == "dropRifle(Clone)")
            {
                Destroy(gameObject);
            }
        }
    }
}