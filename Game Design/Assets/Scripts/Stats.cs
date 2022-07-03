using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stats : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public int ammoInGun1;
    public int ammoInGun2;
    public int allAmmo;
    public int maxAmmoInGun1;
    public int maxAmmoInGun2;
    public int currentWeapon;
    public bool haveWeapon1;
    public float damage;
    public float range;
    public float impactForce;
    public float fireRate;
    public float nextFire;
    public float reloadTime;

    private void Update()
    {
        if(currentWeapon==2)
        {
            damage = 10;
            impactForce = 20;
            fireRate = 0.4f;
            //nextFire = 0;
            reloadTime = 1;
        }
        if(currentWeapon==1)
        {
            damage = 50;
            impactForce = 100;
            fireRate = 0.1f;
           // nextFire = 0;
            reloadTime = 5;

        }
        if(currentWeapon==3)
        {
            damage = 30;
            impactForce = 40;
            fireRate = 0.0f;
            // nextFire = 0;
            reloadTime = 5;
        }
    }
    public int getCurrentWeapon()
    {
        return currentWeapon;
    }

}
