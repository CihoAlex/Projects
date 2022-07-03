using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject spawnObject;
    private int xPos;
    private int zPos;
    private int spawnCount;
    public Levels level;

    void Start()
    {
        StartCoroutine(SpawnDrop());
    }

    IEnumerator SpawnDrop()
    {
        while(true)
        {
            if (level.spawnedEnemies <= 30)
            {
                xPos = Random.Range(-30, 50);
                zPos = Random.Range(-55, 18);
                GameObject newEnemy = Instantiate(spawnObject, new Vector3(xPos, 3, zPos), Quaternion.identity);
                level.spawnedEnemies++;
                newEnemy.name = "enemy" + spawnCount;
                yield return new WaitForSeconds(1.0f);
                spawnCount++;
            }
            yield return null;

        }
    } 
}
