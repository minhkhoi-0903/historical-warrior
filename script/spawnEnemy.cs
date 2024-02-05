using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public GameObject enemy;
    //[SerializeField] private GameObject e_sword;
    [SerializeField] private enemy_china enemyScript;
    [SerializeField] private Transform spawnPoints;
    [SerializeField] private float IsDestroy;
    [SerializeField] private float Maxenemy;
    [SerializeField] private float enemySpawnNow;

    void Start()
    {
        //IsDestroy = 0;
        Maxenemy = 1;
        enemySpawnNow = 0;
    }
    
    void Update()
    {
        bool IsActive = enemy.activeSelf;

        if (enemy != null && !IsActive)
        {
            enemyScript.enemyIsDestroy(IsDestroy = 1);
        }

        if (IsDestroy == 1 && enemySpawnNow <= Maxenemy)
        {
            enemySpawn();
            enemySpawnNow += 1;
        }
    }

    public void enemySpawn()
    {
        Instantiate(enemyPrefab, spawnPoints.position, Quaternion.identity);
    }
}
