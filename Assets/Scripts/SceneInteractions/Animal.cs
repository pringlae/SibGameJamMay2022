using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : SceneInteraction
{
    public SceneInteraction wantedItem;
    public bool questCompleted { get; protected set; } = false;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled || other.gameObject.layer != 6) return;

        var bear = other.GetComponent<BearMovement>();
        if (bear.itemHeld == null || bear.itemHeld != wantedItem || !bear.itemOnHand) return;

        base.OnTriggerEnter2D(other);
    }

    public override void OnUse(BearMovement bear)
    {
        base.OnUse(bear);
        bear.ClearItem();
        questCompleted = true;
        UIController.Instance.AddCoins(20);
    }
}
