using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private float spawnRange = 9;
    public GameObject[] powerupPrefab;
    public int waveNumber = 1;
    public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        int randomPowerup = UnityEngine.Random.Range(0,powerupPrefab.Length);
        Instantiate(powerupPrefab[randomPowerup], GenerateSpawnPosition(), powerupPrefab[randomPowerup].transform.rotation);
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = UnityEngine.Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemy], GenerateSpawnPosition(), enemyPrefab[randomEnemy].transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPozX = UnityEngine.Random.Range(-spawnRange, spawnRange);
        float spawnPozZ = UnityEngine.Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPozX, 0, spawnPozZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            int randomPowerup = UnityEngine.Random.Range(0, powerupPrefab.Length);
            Instantiate(powerupPrefab[randomPowerup], GenerateSpawnPosition(), powerupPrefab[randomPowerup].transform.rotation);
        }
    }
}
