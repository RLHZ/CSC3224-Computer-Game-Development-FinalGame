using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public Text armourText;
    public Text attackText;

    protected override void Awake() {
        base.Awake();
        armourText.text = armour.GetValue().ToString();
        attackText.text = damage.GetValue().ToString();
    }

    public void IncreaseArmour(int ammount) {
        armour.IncreaseValue(ammount);
        armourText.text = armour.GetValue().ToString();
    }

    public void IncreaseAttack(int ammount) {
        damage.IncreaseValue(ammount);
        attackText.text = damage.GetValue().ToString();
    }

    public void IncreaseHealthByPotion(int healthPotionValue) {
        IncreaseHealth(healthPotionValue);
        GameController.Instance.uiController.UpdatePlayerHealth(currentHealth, maxHealth);
    }
}
