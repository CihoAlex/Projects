using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event4 : MonoBehaviour
{
    public Levels level;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && level.level == 3)
        {
            level.level = 4;
            this.enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
