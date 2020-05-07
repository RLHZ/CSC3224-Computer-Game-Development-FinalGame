using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugAssist : MonoBehaviour
{
    public bool isDebugMode = false;
    public bool isIgnorePlayer = false;
    public Text statsText;
    public Text rollingText;
    public static DebugAssist Instance;

    float deltaTime = 0.0f;

    public CharacterStats building1Stats;
    public CharacterStats building2Stats;
    public CharacterStats building3Stats;
    public CharacterStats building4Stats;
    public CharacterStats building5Stats;
    public CharacterStats building6Stats;

    public PlayerStats playerStats;
    WaveController waveController;

    List<System.Tuple<string, float>> updates = new List<System.Tuple<string, float>>();

    void Awake() {
        Instance = this;
        waveController = GetComponent<WaveController>();
        statsText.enabled = isDebugMode;
    }



    void Update() {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Input.GetKey(KeyCode.LeftShift))
            ProcessShiftInput();

        if (Input.GetKey(KeyCode.LeftAlt) && isDebugMode)
            if(Input.GetKey(KeyCode.LeftControl))
                ProcessCtrlInput();

        if (!isDebugMode) return;
   
        UpdateStats();
        UpdateRollingText();
    }


    void UpdateStats() {
        //Time Elapsed
        statsText.text = "Stats\nTime elapsed: " + System.Math.Round(Time.time,2);

        //Framerate
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        statsText.text += string.Format("\n{0:0.0} ms ({1:0.} fps)", msec, fps);

  
        statsText.text += "\n\nEnemies in the Scene: " + GameSettings.BadGuys.Count;
        statsText.text += "\nBuildings in the Scene: " + GameSettings.Buildings.Count;
        statsText.text += "\nEnemies in current wave: " + waveController.GetCurrentWaveSize();

        statsText.text += "\n";

        if (building1Stats.isImmune)
            statsText.text += "\nBuilding 1 is Immune to attack.";
        if (building2Stats.isImmune)
            statsText.text += "\nBuilding 2 is Immune to attack.";
        if (building3Stats.isImmune)
            statsText.text += "\nBuilding 3 is Immune to attack.";
        if (building4Stats.isImmune)
            statsText.text += "\nBuilding 4 is Immune to attack.";
        if (building5Stats.isImmune)
            statsText.text += "\nBuilding 5 is Immune to attack.";
        if (building6Stats.isImmune)
            statsText.text += "\nBuilding 6 is Immune to attack.";

        if (playerStats.isImmune)
            statsText.text += "\nPlayer is Immune to attack.";

        if (isIgnorePlayer)
            statsText.text += "\nPlayer is being ignored by enemies.";
    }

    void ProcessShiftInput() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            isDebugMode = !isDebugMode;
            statsText.enabled = isDebugMode;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            building1Stats.isImmune = !building1Stats.isImmune;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            building2Stats.isImmune = !building2Stats.isImmune;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            building3Stats.isImmune = !building3Stats.isImmune;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            building4Stats.isImmune = !building4Stats.isImmune;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            building5Stats.isImmune = !building5Stats.isImmune;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            building6Stats.isImmune = !building6Stats.isImmune;

        if (Input.GetKeyDown(KeyCode.C))
            playerStats.isImmune = !playerStats.isImmune;

        if (Input.GetKeyDown(KeyCode.I))
            isIgnorePlayer = !isIgnorePlayer;
    }

    void ProcessCtrlInput() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) { 
            building1Stats.Die();
            updates.Add(new System.Tuple<string, float>("Building 1 destroyed", Time.time));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { 
            building2Stats.Die();
            updates.Add(new System.Tuple<string, float>("Building 2 destroyed", Time.time));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            building3Stats.Die();
            updates.Add(new System.Tuple<string, float>("Building 3 destroyed", Time.time));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            building4Stats.Die();
            updates.Add(new System.Tuple<string, float>("Building 4 destroyed", Time.time));
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            building5Stats.Die();
            updates.Add(new System.Tuple<string, float>("Building 5 destroyed", Time.time));
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            building6Stats.Die();
            updates.Add(new System.Tuple<string, float>("Building 6 destroyed", Time.time));
        }

        if (Input.GetKeyDown(KeyCode.X)) { 
            playerStats.Die();
            updates.Add(new System.Tuple<string, float>("Player Killed", Time.time));
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            GameController.Instance.AddCoins(100);
            updates.Add(new System.Tuple<string, float>("+100 Coins", Time.time));
        }            
        if (Input.GetKeyDown(KeyCode.V)) {
            GameController.Instance.AddHealthPotions(10);
            updates.Add(new System.Tuple<string, float>("+10 Potions", Time.time));
        }      
        if (Input.GetKeyDown(KeyCode.B)) {
            playerStats.IncreaseHealthByPotion(50);
            updates.Add(new System.Tuple<string, float>("+50 Health", Time.time));
        }      
        if (Input.GetKeyDown(KeyCode.N)) {
            playerStats.IncreaseArmour(10);
            updates.Add(new System.Tuple<string, float>("+10 Armour", Time.time));
        }       
        if (Input.GetKeyDown(KeyCode.M)) {
            playerStats.IncreaseAttack(10);
            updates.Add(new System.Tuple<string, float>("+10 Attack", Time.time));
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Time.timeScale *= 2;
            updates.Add(new System.Tuple<string, float>("Speed x2, Current Speed: " + Time.timeScale, Time.time));
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            Time.timeScale /= 2;
            updates.Add(new System.Tuple<string, float>("Speed /2, Current Speed: " + Time.timeScale, Time.time));
        }

    }

    void UpdateRollingText() {
        if (updates.Count > 0 && Time.time - updates[0].Item2 > 5)
            updates.RemoveAt(0);

        string rollingValue = " ";

        foreach(var update in updates) {
            rollingValue += ("\n" + update.Item1);
        }

        rollingText.text = rollingValue;
    }

    public void MakeAllImmune() {
        building1Stats.isImmune = true;
        building2Stats.isImmune = true;
        building3Stats.isImmune = true;
        building4Stats.isImmune = true;
        building5Stats.isImmune = true;
        building6Stats.isImmune = true;
        playerStats.isImmune = true;
    }
}
