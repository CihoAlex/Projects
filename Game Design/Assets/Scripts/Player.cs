using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Stats stats;
    public Text ammoInGunDisplay;
    public Text allAmmoDisplay;
    public GameObject blood;
    public Levels level;
    public Transform player;
    public Vector3 respawnPosition = new Vector3(77f, 5f, 15f);
    
    void Die()
    {
        Debug.Log("am murit");
        level.level = 4;
        stats.allAmmo = 90;
        stats.currentHealth = 100;
        player.transform.position = respawnPosition;
        level.reset = 1;
    }

    
    public void TakeDamage(float amount)
    {
        stats.currentHealth -= amount;
        var color = blood.GetComponent<Image>().color;
        color.a = amount/100;
        blood.GetComponent<Image>().color = color;
        if (stats.currentHealth <= 0f)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(blood.GetComponent<Image>().color.a>0)
        {
            var color = blood.GetComponent<Image>().color;
            color.a -=0.001f;
            blood.GetComponent<Image>().color = color;
        }
        allAmmoDisplay.text = stats.allAmmo.ToString();
        if (stats.currentWeapon == 1)
        {
            ammoInGunDisplay.gameObject.SetActive(true);

            ammoInGunDisplay.text = stats.ammoInGun1.ToString();
        }
        if (stats.currentWeapon == 2)
        {
            ammoInGunDisplay.gameObject.SetActive(true);
            ammoInGunDisplay.text = stats.ammoInGun2.ToString();
        }
        if (stats.currentWeapon == 3) ammoInGunDisplay.gameObject.SetActive(false);
    }
}
