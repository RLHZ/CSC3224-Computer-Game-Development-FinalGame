using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    public GameObject ally;
    public GameObject spawnmarker;
    Transform allyPos;

    float randomDistance = 2;

    void Awake() {
        spawnmarker = GameObject.Find("AllySpawnMarker");
        allyPos = spawnmarker.transform;

        //SpawnAllies(1);
    }

    public void SpawnAllies(int numAllies) {
        for (int i = numAllies; i > 0; i--) {
            Instantiate(ally, new Vector3(Random.Range(allyPos.position.x + randomDistance, allyPos.position.x - randomDistance),
                allyPos.position.y, Random.Range(allyPos.position.z + randomDistance, allyPos.position.z - randomDistance)), allyPos.rotation);      
        }
    }
}
