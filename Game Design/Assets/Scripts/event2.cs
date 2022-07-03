using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event2 : MonoBehaviour
{
    public Levels level;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && level.level == 1)
        {
            level.level = 2;
            this.enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
