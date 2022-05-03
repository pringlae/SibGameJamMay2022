using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : SceneInteraction
{
    public static Dictionary<string, bool> rescued = new Dictionary<string, bool>();
    public SceneInteraction wantedItem;
    public bool questCompleted { get; protected set; } = false;

    protected virtual string className { get; }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled || other.gameObject.layer != 6) return;

        var bear = other.GetComponent<BearMovement>();
        if (bear.itemHeld == null || bear.itemHeld != wantedItem || !bear.itemOnHand) return;

        SetNearBy();
    }

    public override void OnUse(BearMovement bear)
    {
        base.OnUse(bear);
        bear.itemHeld.transform.parent = transform;
        bear.itemHeld.transform.localPosition = new Vector3(0.1f, -0.05f, 0f);
        bear.ClearItem();
        questCompleted = true;
        UIController.Instance.AddCoins(20);
    }
}
