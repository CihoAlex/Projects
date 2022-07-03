using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event3 : MonoBehaviour
{
    public Levels level;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && level.level == 2)
        {
            level.level = 3;
            this.enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
