using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    CharacterCombat characterCombat;
    AttackingCharacterController attackController;

    public void Awake() {
        characterCombat = GetComponent<CharacterCombat>();
        attackController = GetComponent<AttackingCharacterController>();
    }

    public void SlashHit() {
        bool isBuildingAttack = transform.tag.Equals("Bad") && transform.gameObject.GetComponent<EnemyController>().isAttackingBuilding;
        Transform target;
        if (isBuildingAttack) {
            target = transform.gameObject.GetComponent<EnemyController>().buildingTarget;
        }
        else {
            target = characterCombat.DetermineTarget();
        }

        if (target) {
            float distance = Vector3.Distance(target.position, transform.position);

            if (((distance <= attackController.attackDistance) ||
            (isBuildingAttack && (distance <= attackController.buildingAttackDistance)))) {
                characterCombat.Attack(target);
            }
        } 
    }
}
