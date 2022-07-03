using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public Stats stats;
    private int droppedAmmo;
    private bool alreadyTook;
    public Levels level;
    
    private void Start()
    {
        if (level.level == 2)
        {
            droppedAmmo = 30;
        }
        else
        { droppedAmmo = Random.Range(10, 100-(2*level.level)); }
        alreadyTook = false;
    }
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            if (stats.allAmmo + droppedAmmo >= 100)
            {
                if (alreadyTook == false)
                { droppedAmmo = 100 - stats.allAmmo; alreadyTook = true; }

                stats.allAmmo = 100;
                Debug.Log("Can't carry more ammo");
            }
            else if(stats.allAmmo+droppedAmmo<100)
            {
                stats.allAmmo = stats.allAmmo + droppedAmmo;
                Debug.Log("am luat munitie");
                Destroy(gameObject);
            }
        }           
    }
    void Update()
    {
        if (level.reset == 1)
        {
            if (this.name == "box_ammo(Clone)")
            {
                Destroy(gameObject);
            }
        }
    }
}
