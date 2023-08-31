using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave();
    }
    void SpawnEnemyWave()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPozX = Random.Range(-spawnRange, spawnRange);
        float spawnPozZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPozX, 0, spawnPozZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
