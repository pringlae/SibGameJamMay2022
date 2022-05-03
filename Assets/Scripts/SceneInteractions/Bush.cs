using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : SceneInteraction
{
    public SceneInteraction acorn;

    public void Start()
    {
        acorn.enabled = false;
    }
    public override void OnUse(BearMovement bear)
    {
        base.OnUse(bear);
        acorn.enabled = true;
        acorn.bubble.gameObject.SetActive(true);
        acorn.SetNearBy();
        gameObject.SetActive(false);
    }
}
