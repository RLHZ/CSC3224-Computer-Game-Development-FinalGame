using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armour;

    public bool isBuilding = false;
    AliveCharacterController characterController;
    BuildingController buildingController;

    public bool isImmune = false;

    protected virtual void Awake() {
        currentHealth = maxHealth;

        if (isBuilding)
            buildingController = GetComponent<BuildingController>();        
        else
            characterController = GetComponent<AliveCharacterController>();
    }

    void Update() {
        /*if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(30);
        }*/
    }

    public void IncreaseHealth(int increase) {
        int currentDifference = maxHealth - currentHealth;
        increase = Mathf.Clamp(increase, 0, currentDifference);

        currentHealth += increase;
    }

    public void TakeDamage(int damage) {

        damage -= armour.GetValue();
        damage = Mathf.Clamp(damage, 1, int.MaxValue);
        
        currentHealth -= damage;

        if (isBuilding)
            buildingController.GetHit();
        else
            characterController.GetHit(damage);

        if (currentHealth <= 0) {
            currentHealth = 0;
            Die();
        }
    }

    public virtual void Die() {
        if(isBuilding)
            buildingController.Die();
        else
            characterController.Die();        
    }

}
