using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : SceneInteraction
{
    public SceneInteraction carrot;
    public Sprite[] carrotSprites;
    int state = 0;
    
    public override void OnDayStart(int dayIndex)
    {
        if (state == 0)
            carrot.gameObject.SetActive(false);
        else if (state == 1)
        {
            carrot.gameObject.SetActive(true);
            carrot.GetComponent<SpriteRenderer>().sprite = carrotSprites[0];
            carrot.enabled = false;
        }
        else if (state == 2)
        {
            interactionName = UIController.InfoButton.Take;
            carrot.GetComponent<SpriteRenderer>().sprite = carrotSprites[1];
            carrot.gameObject.SetActive(true);
            carrot.enabled = true;
        }
        if (state < 3)
            Activate();
    }

    public override void OnUse(BearMovement bear)
    {
        base.OnUse(bear);
        state++;
        if (state == 3)
            bear.TakeItem(carrot);
        enabled = false;
    }
}
