using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : PickupObject
{
    public override void OnTriggerEnter(Collider collider) {
        base.OnTriggerEnter(collider);
        GameController.Instance.AddPotion();
    }
}
