using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text waveInfoText;
    public Text coinNumberText;
    public Text allyNumberText;
    public Text buildingNumberText;
    public Text healthPotionNumberText;
    public Text playerHealthText;
    public SimpleHealthBar playerUiHealthbar;


    public void UpdateWaveInfo(string text) {
        waveInfoText.text = text;
    }

    public void UpdateCoinNumber(int coins) {
        coinNumberText.text = coins.ToString();
    }

    public void UpdateHealthPotionsNumber(int potions) {
        healthPotionNumberText.text = potions.ToString();
    }

    public void UpdatePlayerHealth(int health, int maxHealth) {
        playerHealthText.text = string.Format("{0}/{1}", health.ToString(), maxHealth.ToString());
        playerUiHealthbar.UpdateBar(health, maxHealth);

    }

    public void UpdateAllyNumber() {
        allyNumberText.text = GameSettings.allies.Count.ToString();
    }

    public void UpdateBuildingNumber() {
        buildingNumberText.text = "Buildings Left:  " + GameSettings.Buildings.Count.ToString();
    }
}
