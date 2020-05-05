using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject spawnmarker;
    Transform enemyPos;
    PlaySoundEvent psEvent;

    float lastSpawnTime;
    public float spawnWaitTime = 5f;

    float lastInstantSpawnTime;
    public float instantSpawnWaitTime = 5f;
    
    public int maxNumEnemies = 40;
    public int instantSpawnPercentage = 20;

    int enemiesToSpawn = 0;
    float randomDistance = 2;


    bool isWaveActive = false;

    void Awake()
    {
        lastSpawnTime = Time.time;
        spawnmarker = GameObject.Find("SpawnMarker");
        enemyPos = spawnmarker.transform;
        psEvent = spawnmarker.GetComponent<PlaySoundEvent>();
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - lastSpawnTime > spawnWaitTime && GameSettings.BadGuys.Count < maxNumEnemies && isWaveActive && 
               !GameController.isFinished && enemiesToSpawn > 0 && Time.time - lastInstantSpawnTime > instantSpawnWaitTime) {

            Instantiate(enemy, new Vector3(Random.Range(enemyPos.position.x + randomDistance, enemyPos.position.x - randomDistance),
                enemyPos.position.y, Random.Range(enemyPos.position.z + randomDistance, enemyPos.position.z - randomDistance)), enemyPos.rotation);

            psEvent.PlayEnemySpawn();
            lastSpawnTime = Time.time;
            enemiesToSpawn--;
        }
    }

    public void StartSpawning(float waveDuration, int enemiesCount) {
        enemiesToSpawn = enemiesCount;
        InstantSpawn(waveDuration);
        isWaveActive = true;        
    }

    public void EndSpawning() {
        isWaveActive = false;

    }

    void InstantSpawn(float waveDuration) {
        lastInstantSpawnTime = Time.time;
        
        int instantEnemies = enemiesToSpawn * instantSpawnPercentage / 100;
        enemiesToSpawn -= instantEnemies;
        spawnWaitTime = (waveDuration - 5) / (float) enemiesToSpawn;

        for (int i = instantEnemies; i > 0; i--) {
            Instantiate(enemy, new Vector3(Random.Range(enemyPos.position.x + randomDistance, enemyPos.position.x - randomDistance),
                enemyPos.position.y, Random.Range(enemyPos.position.z + randomDistance, enemyPos.position.z - randomDistance)), enemyPos.rotation);
            //psEvent.PlayEnemySpawn();           
        }
    }
}
