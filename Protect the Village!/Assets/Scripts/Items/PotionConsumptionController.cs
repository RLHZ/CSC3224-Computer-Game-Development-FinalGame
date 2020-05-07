using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionConsumptionController : MonoBehaviour
{
    public int healthPotionValue = 30;
    PlayerStats stats;

    public GameObject floatingTextPrefab;
    public Text text;

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
            var prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            string message = stats.maxHealth - stats.currentHealth >= 30 ? "+30" : "+" + (stats.maxHealth - stats.currentHealth).ToString();
            prefab.GetComponent<TextMesh>().text = message;            

            stats.IncreaseHealthByPotion(healthPotionValue);
            GameController.Instance.RemovePotion();
            
        }
    }
}
