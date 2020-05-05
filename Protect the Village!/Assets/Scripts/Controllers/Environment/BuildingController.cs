using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    SimpleHealthBar healthBar;
    CharacterStats stats;
    AudioSource audioSource;
    bool isAlive;

    public List<AudioClip> buildingHitSounds;
    public AudioClip buildingDie;
    public AudioClip buildingFire;

    public List<GameObject> torches;

    void Awake() {
        isAlive = true;
        GameSettings.Buildings.Add(transform);
        healthBar = transform.Find("Cube_Healthbar").Find("Canvas").Find("Simple Bar soldier").Find("Status Fill 01 soldier").gameObject.GetComponent<SimpleHealthBar>(); 
        stats = GetComponent<CharacterStats>();
        audioSource = GetComponent<AudioSource>();
        stats.isBuilding = true;       
    }

    void Start() {
        healthBar.UpdateBar(stats.currentHealth, stats.maxHealth);
    }

    public void Die() {
        if (isAlive) {
            Destroy(transform.Find("Cube_Healthbar").gameObject);

            foreach(GameObject torch in torches)
                Destroy(torch);

            ReasignTargetToEnemies();
            GameSettings.Buildings.Remove(transform);

            audioSource.volume = 1;
            audioSource.PlayOneShot(buildingDie);
            audioSource.clip = buildingFire;
            audioSource.Play();
            isAlive = false;
        }
    }

    public void GetHit() {
        audioSource.PlayOneShot(buildingHitSounds[Random.Range(0, buildingHitSounds.Count)]);
    }

    private void ReasignTargetToEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Bad");
        
        List<Transform> buildingsLeft = new List<Transform>(GameSettings.Buildings);
        buildingsLeft.Remove(transform);

        foreach (GameObject enemy in enemies) {
            EnemyController ec = enemy.GetComponent<EnemyController>();

            if (ec.buildingTarget == transform) {
                ec.AssignNewBuilding(buildingsLeft);
            }
        }
    }
}
