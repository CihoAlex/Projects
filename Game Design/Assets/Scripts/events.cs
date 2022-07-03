using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class events : MonoBehaviour
{
    public Levels level;
    private void Start()
    {
        GetComponent<Collider>().enabled = true;
    }
    void OnTriggerEnter(Collider collider)
    {
         if (collider.gameObject.tag == "Player"&&level.level==0)
        {
            level.level = 1;
            this.enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
