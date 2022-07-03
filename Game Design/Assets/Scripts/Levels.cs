using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Levels : MonoBehaviour
{
    public GameObject Player;
    public int level;
    public Text text;
    public Stats stats;
    public EnemyStats Estats;
    public Image croshair;
    private float timer;
    public GameObject healthbar;
    public Text ammoInGunDisplay;
    public Text allAmmoDisplay;
    public GameObject medBox;
    public GameObject ammoBox;
    private bool oneTime=true;
    private bool oneTime2 = true;
    private bool oneTime3 = true;
    private bool oneTime4 = true;
    public GameObject enemy;
    public RandomSpawn spawn;
    public int reset;
    public float waitingTimeToRespawnAgain;
    public float timerForRespawn;
    public Light light;
    public int killcount;
    public int spawnedEnemies;

    void Start()
    {
        level = 0;
        reset = 0;
        killcount = 0;
        light.enabled = true;
        timerForRespawn = 0f;
        waitingTimeToRespawnAgain = 3f;
        spawn.enabled = false;
        stats.currentHealth = 10;
        stats.maxHealth = 100;
        stats.ammoInGun1 = 0;
        stats.ammoInGun2 = 0;
        stats.allAmmo = 0;
        stats.maxAmmoInGun1 = 30;
        stats.maxAmmoInGun2 = 10;
        stats.currentWeapon = 3;
        stats.haveWeapon1 = false;
        croshair.enabled = false;    
        healthbar.SetActive(false);
        ammoInGunDisplay.enabled = false;
        allAmmoDisplay.enabled = false;
        text.text = "Welcome agent";

    }
    void Update()
    {
        //Debug.Log(level+" "+timer);
        timer += Time.deltaTime;
        if(timer>2&&timer<5)
        {
            text.text = "Welcome agent\nUse WASD to move and mouse to look arround";
        }
        if (level==1)
        {
            text.text = "in left down side of the screen you have your health\n pick some health";
            healthbar.SetActive(true);
            if (oneTime == true)
            {
                GameObject newMedBox = Instantiate(medBox, new Vector3(80, 3, 7), Quaternion.identity);
                GameObject newMedBox2 = Instantiate(medBox, new Vector3(77, 3, 7), Quaternion.identity);
                GameObject newMedBox3 = Instantiate(medBox, new Vector3(74, 3, 7), Quaternion.identity);


                oneTime = false;
            }
        }
        if (level == 2)
        {
            text.text = "in right down side of the screen you have your ammo\n go get some ammo";
            ammoInGunDisplay.enabled = true;
            allAmmoDisplay.enabled = true;
            if(oneTime2==true)
            {
                GameObject newAmmoBox = Instantiate(ammoBox, new Vector3(80, 3, -4), Quaternion.identity);
                GameObject newAmmoBox2 = Instantiate(ammoBox, new Vector3(75, 3, -4), Quaternion.identity);
                GameObject newAmmoBox3 = Instantiate(ammoBox, new Vector3(70, 3, -4), Quaternion.identity);

                oneTime2 = false;
            }
        }
        if (level == 3)
        {
            text.text = "press 2 and kill the enemy";
            croshair.enabled = true;
            if (oneTime3 == true)
            {
                GameObject newEnemy = Instantiate(enemy, new Vector3(47, 3, -3), Quaternion.identity);
                oneTime3 = false;
            }
        }
        if(level==4)
        {
            if (oneTime4 == true)
            {
                text.text = "";
                stats.currentWeapon = 2;
                stats.haveWeapon1 = false;
                spawn.enabled = true;
                oneTime4 = false;
            }         
        }
        if(level>=4)
        {
            text.text = "kill count: " + killcount;
        }    
        if(reset==1)
        {
            timerForRespawn += Time.deltaTime;
            if (timerForRespawn > waitingTimeToRespawnAgain)
            {
                killcount = 0;
                reset = 0;
                level = 4;
                timerForRespawn = 0;
                timer = 0;
                spawnedEnemies = 0;
            }
        }
        if(level>3&&timer>level*20)
        {
            level++;
        }
        if(level>3&&level%2==0)
        {
            light.enabled = true;
        }
        else if (level > 3 && level % 2 == 1)
        {
            light.enabled = false;
        }
    }
}
