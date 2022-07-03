using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class medBox : MonoBehaviour
{
    public Stats stats;
    private float droppedHealth;
    private bool alreadyTook;
    public GameObject blood;
    public Levels level;
    private void Start()
    {
        if (level.level > 3)
        {
            droppedHealth = Random.Range(10f, 100f-(2*level.level));
        }
        else if(level.level==1)
        {
            droppedHealth = 30;
        }
        alreadyTook = false;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {  
                if (stats.currentHealth + droppedHealth > 100f)
                {
                    if (alreadyTook == false)
                    { droppedHealth = 100f - stats.currentHealth; alreadyTook = true; }
                    var color = blood.GetComponent<Image>().color;
                    color.a = 0;
                    blood.GetComponent<Image>().color = color;
                    stats.currentHealth = 100f;
                    Debug.Log("nu mai pot lua viata");
                }
                else if (stats.currentHealth + droppedHealth <= 100f)
                {
                    var color = blood.GetComponent<Image>().color;
                    color.a = 0;
                    blood.GetComponent<Image>().color = color;
                    stats.currentHealth = stats.currentHealth + droppedHealth;
                    Debug.Log("am luat viata");
                    Destroy(gameObject);
                }
        }
        
    }
    void Update()

    {
        if (level.reset == 1)
        {
            if (this.name == "box_med(Clone)")
            {
                Destroy(gameObject);
            }
        }
    }
}
