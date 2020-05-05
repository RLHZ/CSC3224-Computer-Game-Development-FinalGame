using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionConsumptionController : MonoBehaviour
{
    public int healthPotionValue = 20;
    PlayerStats stats;

    void Awake() {
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ConsumeHealthPotion();
    }

    void ConsumeHealthPotion() {
        if (GameController.Instance.healthPotionsAvailable > 0 && stats.currentHealth < stats.maxHealth) { 
            stats.IncreaseHealthByPotion(healthPotionValue);
            GameController.Instance.RemovePotion();
        }
    }
}
