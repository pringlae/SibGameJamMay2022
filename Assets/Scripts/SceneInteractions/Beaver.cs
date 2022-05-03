using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaver : Animal
{
    public List<SceneInteraction> logs = new List<SceneInteraction>();

    private int logsNeeded;

    protected override string className => "Beaver";

    void Start()
    {
        logsNeeded = logs.Count;
    }
    
    public override void OnDayStart(int dayIndex)
    {
        if (logsNeeded > 0)
            Activate();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled || other.gameObject.layer != 6) return;

        var bear = other.GetComponent<BearMovement>();
        if (bear.itemHeld == null || !logs.Contains(bear.itemHeld) || !bear.itemOnHand) return;

        SetNearBy();
    }

    public override void OnUse(BearMovement bear)
    {
        logs.Remove(bear.itemHeld);
        bear.itemHeld.transform.parent = transform;
        bear.itemHeld.transform.localPosition = new Vector3(0.07f, -0.04f, 0f) * logsNeeded;
        bear.ClearItem();
        logsNeeded--;
        if (logsNeeded <= 0)
            base.OnUse(bear);
    }
}
