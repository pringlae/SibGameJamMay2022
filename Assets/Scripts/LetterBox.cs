using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : SceneInteraction
{
    private new BoxCollider2D collider;
    private new Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        animation = GetComponent<Animator>();
    }

    public override void OnUse(BearMovement bear)
    {
        base.OnUse(bear);
        animation.SetTrigger("Open");
    }
}
