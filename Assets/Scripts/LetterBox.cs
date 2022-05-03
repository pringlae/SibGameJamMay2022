using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    private SpriteOutline outline;
    private new BoxCollider2D collider;
    private new Animator animation;

    public bool BearNearBy { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<SpriteOutline>();
        collider = GetComponent<BoxCollider2D>();
        animation = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled || other.gameObject.layer != 6) return;

        outline.enabled = true;
        BearNearBy = true;
        UIController.Instance.SetInfoButtonsState(UIController.InfoButtonsState.UseItem);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!enabled || other.gameObject.layer != 6) return;
        
        outline.enabled = false;
        BearNearBy = false;
        UIController.Instance.SetInfoButtonsState(UIController.InfoButtonsState.Empty);
    }

    public void OnOpened()
    {
        animation.SetTrigger("Open");
        outline.enabled = false;
        UIController.Instance.SetInfoButtonsState(UIController.InfoButtonsState.Empty);
    }
}
