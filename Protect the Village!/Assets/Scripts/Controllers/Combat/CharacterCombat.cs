using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;
    SimpleHealthBar healthBar;
    string opponentTag;

    public float halfAngleOfView = 20f;

    List<Transform> targets;

    void Awake() {
        myStats = GetComponent<CharacterStats>();
        healthBar = GetComponentsInChildren<SimpleHealthBar>()[0];
    }


    private void SortTargetsByDistance() {
        targets.Sort(delegate (Transform t1, Transform t2) {
            return Vector3.Distance(t1.position, transform.position).CompareTo(Vector3.Distance(t2.position, transform.position));
        });
    }

    public Transform DetermineTarget() {

        targets = transform.tag.Equals("Good") || transform.tag.Equals("Player") ? GameSettings.BadGuys : GameSettings.GoodGuys;
        SortTargetsByDistance();

        foreach (Transform opponent in targets) {
            if (Vector3.Angle(transform.forward, opponent.transform.position - transform.position) > halfAngleOfView)
                continue;
            else {
                return opponent;
            }
        }
        return null;
    }

    public void Attack(Transform opponent) {
        CharacterStats opponentStats = opponent.GetComponent<CharacterStats>();

        if (!opponentStats.isImmune) { 
            opponentStats.TakeDamage(myStats.damage.GetValue());

            SimpleHealthBar[] healthbar = opponent.GetComponentsInChildren<SimpleHealthBar>();

            if (healthbar.Length > 0)
                healthbar[0].UpdateBar(opponentStats.currentHealth, opponentStats.maxHealth);
        }
    }
}
