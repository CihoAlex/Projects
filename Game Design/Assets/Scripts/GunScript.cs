using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    private bool isReloading = false;
    public Transform bulletPosition;
    public GameObject secondWeapon;
    public GameObject firstWeapon;
    public GameObject melee;
    public ParticleSystem muzzleFlash2;
    public ParticleSystem muzzleFlash1;
    public GameObject impactEffect;
    public AudioClip fire;
    public AudioClip reload;
    public Stats stats;
    public EnemyStats Estats;
    public Levels level;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (stats.currentWeapon==2)
        {
            secondWeapon.SetActive(true);
            firstWeapon.SetActive(false);
            melee.SetActive(false);
        }
        if (stats.currentWeapon == 1)
        {
            firstWeapon.SetActive(true);
            secondWeapon.SetActive(false);
            melee.SetActive(false);
        }
        if(stats.currentWeapon==3)
        {
            firstWeapon.SetActive(false);
            secondWeapon.SetActive(false);
            melee.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)&&level.level>2)
        {
            stats.currentWeapon = 2;
            muzzleFlash2.Pause(true);
            muzzleFlash1.Pause(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && stats.haveWeapon1 == true)
        {
            stats.currentWeapon = 1;
            muzzleFlash1.Pause(true);
            muzzleFlash2.Pause(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && stats.haveWeapon1 == false) { Debug.Log("You don't have weapon1"); }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            stats.currentWeapon = 3;
            muzzleFlash1.Pause(true);
            muzzleFlash2.Pause(true);
        }
        if (isReloading)
        {
            return;
        }
        if (stats.currentWeapon == 1)
        {
            if (stats.ammoInGun1 <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
        }
        if (stats.currentWeapon == 2)
        {
            if (stats.ammoInGun2 <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
        }
        if (Input.GetButton("Fire1") &&Time.time>=stats.nextFire&&stats.currentWeapon<3)
        { 
            stats.nextFire = Time.time + stats.fireRate;
            Shoot();
        }
        if (Input.GetButton("Fire1") && Time.time >= stats.nextFire && stats.currentWeapon == 3)
        {
            ShootMelee();
        }
        if (Input.GetKeyDown(KeyCode.R)&& stats.currentWeapon < 3)
        {
            StartCoroutine(Reload());
            return;
        }
    }
    void ShootMelee()
    {
        Debug.Log("dau cutit");
        if (Physics.Raycast(bulletPosition.transform.position, bulletPosition.transform.forward, out RaycastHit hit, 1))
        {
            if (!(hit.transform.name.Equals("Player")))
            { 
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    if (hit.collider is SphereCollider)
                    {
                        enemy.TakeDamage(100);
                    }
                    else
                    {
                        enemy.TakeDamage(stats.damage);
                    }
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * stats.impactForce);
                }
            }
        }
    }
    IEnumerator Reload()
    {
        if (stats.currentWeapon == 1)
        {
            if (stats.ammoInGun1 == stats.maxAmmoInGun1)
            {
                Debug.Log("Gun already full");
            }
            if (stats.allAmmo == 0)
            {
                Debug.Log("no more bullets :(");
            }
            if (stats.ammoInGun1 < stats.maxAmmoInGun1 && stats.allAmmo > 0)
            {
                isReloading = true;
                Debug.Log("Reloading...");
                AudioSource.PlayClipAtPoint(reload, transform.position, 1);
                int bulletsNeeded = stats.maxAmmoInGun1 - stats.ammoInGun1;
                if (stats.allAmmo >= bulletsNeeded)
                {
                    stats.allAmmo = stats.allAmmo - bulletsNeeded;
                    stats.ammoInGun1 = stats.ammoInGun1 + bulletsNeeded;
                }
                else if (stats.allAmmo < bulletsNeeded)
                {
                    stats.ammoInGun1 = stats.ammoInGun1 + stats.allAmmo;
                    stats.allAmmo = 0;
                }
                yield return new WaitForSeconds(reload.length);
                isReloading = false;
            }
        }
        else
        {
            if (stats.ammoInGun2 == stats.maxAmmoInGun2)
            {
                Debug.Log("Gun already full");
            }
            if (stats.allAmmo == 0)
            {
                Debug.Log("no more bullets :(");
            }
            if (stats.ammoInGun2 < stats.maxAmmoInGun2 && stats.allAmmo > 0)
            {
                isReloading = true;
                Debug.Log("Reloading...");
                AudioSource.PlayClipAtPoint(reload, transform.position, 1);
                int bulletsNeeded = stats.maxAmmoInGun2 - stats.ammoInGun2;
                if (stats.allAmmo >= bulletsNeeded)
                {
                    stats.allAmmo = stats.allAmmo - bulletsNeeded;
                    stats.ammoInGun2 = stats.ammoInGun2 + bulletsNeeded;
                }
                else if (stats.allAmmo < bulletsNeeded)
                {
                    stats.ammoInGun2 = stats.ammoInGun2 + stats.allAmmo;
                    stats.allAmmo = 0;
                }

                yield return new WaitForSeconds(reload.length);
                isReloading = false;
            }
        }
    }
    public void Shoot()
    {
       if(stats.currentWeapon==2)
        muzzleFlash2.Play();
       else muzzleFlash1.Play();
        AudioSource.PlayClipAtPoint(fire, transform.position, 1);
        if(stats.currentWeapon==1)
        stats.ammoInGun1--;
        if (stats.currentWeapon == 2)
            stats.ammoInGun2--;
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(bulletPosition.transform.position, bulletPosition.transform.forward, out RaycastHit hit, range))
        if (Physics.Raycast(rayOrigin, out RaycastHit hit))
        {
            Debug.Log(this.name + " A LOVIT PE " + hit.transform.name);
            if (!(hit.transform.name.Equals("Player")))
            {
                GameObject GOimapct = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(GOimapct, 2);
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    if (hit.collider is SphereCollider)
                    {
                        enemy.TakeDamage(100);
                        Debug.Log("HEADSHOT");
                    }
                    else if(hit.collider is CapsuleCollider)
                    {
                        enemy.TakeDamage(stats.damage);
                        enemy.Estats.currentWeapon = 2;
                        Debug.Log("BODY shot");
                    }
                    else if(hit.collider is BoxCollider)
                    {
                        enemy.TakeDamage(stats.damage);
                        enemy.Estats.speed = Estats.speed - 1.5f;
                        Debug.Log("leg shot");

                    }
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * stats.impactForce);
                }
            }
        }
    }
}
