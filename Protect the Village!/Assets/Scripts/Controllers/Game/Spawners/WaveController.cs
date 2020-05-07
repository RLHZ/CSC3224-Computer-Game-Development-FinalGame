using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemySpawner))]
public class WaveController : MonoBehaviour
{
    public float waveDuration = 60f;
    float lastWaveTime;

    public float breakDuration = 10f;
    float lastBreakTime;

    private float waveTimer;
    private float breakTimer;
    bool withinWave;

    EnemySpawner enemySpawner;

    public int initialWaveEnemies = 10;
    public int wavePercentageIncrease = 40;

    int waveCount;
    int currentWaveSize;

    bool firstWaveStarted = false;
    
    void Awake() {
        waveTimer = 0;
        waveCount = 0;
        currentWaveSize = 0;
        lastWaveTime = Time.time;
        lastBreakTime = Time.time;
        enemySpawner = GetComponent<EnemySpawner>();     
    }

    void Start() {
        //NewWave();
    }

    void Update() {
        if (GameController.Instance.isInTutorial) return;

        if (!GameController.isPaused && !firstWaveStarted) {
            NewWave();
            firstWaveStarted = true;
        }
        else { 
            if (withinWave && waveTimer > waveDuration) 
                TakeWaveBreak();
            else if (!withinWave && breakTimer > breakDuration) 
                NewWave();
        }



        /*
        if (/*Time.time - lastWaveTime waveTimer > waveDuration) {
            
            //Take a break
            if (Time.time - lastBreakTime breakTimer > breakDuration + waveDuration) { 
                //lastBreakTime = Time.time;
                withinWave = false;
                breakTimer = 0;
                enemySpawner.EndSpawning();
            }
            else 
                NewWave();         
        }*/
        if (!GameController.isFinished)
            DisplayWaveInfo();
    }

    void TakeWaveBreak() {
        breakTimer = 0;
        withinWave = false;
        enemySpawner.EndSpawning();
    }

    void NewWave() {
        waveCount++;
        withinWave = true;
        waveTimer = 0;
        enemySpawner.StartSpawning(waveDuration, EstimateWaveSize());
    }

    void DisplayWaveInfo() {
        if (withinWave) {
            waveTimer += Time.deltaTime;

            float timeLeft = waveDuration - waveTimer;

            string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
            string seconds = (timeLeft % 60).ToString("00");

            GameController.Instance.uiController.UpdateWaveInfo(string.Format("Wave ends in: {0}:{1}", minutes, seconds));
        }
        else {
            breakTimer += Time.deltaTime;


            float timeLeft = breakDuration - breakTimer;

            string minutes = Mathf.Floor(timeLeft / 60).ToString("00");
            string seconds = (timeLeft % 60).ToString("00");

            GameController.Instance.uiController.UpdateWaveInfo(string.Format("Next Wave: {0}:{1}", minutes, seconds));
        }
    }

    //Increase the Wave size by 20% * Wave number 
    int EstimateWaveSize() {
        currentWaveSize = initialWaveEnemies + Mathf.RoundToInt(initialWaveEnemies * wavePercentageIncrease * waveCount / 100);
        return currentWaveSize;
    }

    public int GetCurrentWaveSize() {
        return currentWaveSize;
    }
}
