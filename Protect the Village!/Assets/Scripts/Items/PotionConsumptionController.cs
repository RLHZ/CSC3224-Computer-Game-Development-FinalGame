using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionConsumptionController : MonoBehaviour
{
    public int healthPotionValue = 30;
    PlayerStats stats;

    void Awake() {
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !GameController.isFinished)
            ConsumeHealthPotion();
    }

    void ConsumeHealthPotion() {
        if (GameController.Instance.healthPotionsAvailable > 0 && stats.currentHealth < stats.maxHealth) { 
            stats.IncreaseHealthByPotion(healthPotionValue);
            GameController.Instance.RemovePotion();
        }
    }
}
