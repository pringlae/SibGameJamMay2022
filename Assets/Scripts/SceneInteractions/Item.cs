using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : SceneInteraction
{
    public override void OnUse(BearMovement bear)
    {
        base.OnUse(bear);
        bear.TakeItem(this);
    }
}
