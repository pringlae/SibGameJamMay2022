using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : SceneInteraction
{
    public Sprite eaten;

    public override void OnUse(BearMovement bear)
    {
        base.OnUse(bear);
        GetComponent<SpriteRenderer>().sprite = eaten;
        enabled = false;
    }
}
