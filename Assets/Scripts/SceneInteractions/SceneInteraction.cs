using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInteraction : MonoBehaviour
{
    public GameObject bubble;
    public UIController.InfoButton interactionName;
    protected SpriteOutline outline;
    public bool BearNearBy { get; private set; } = false;

    protected void Awake()
    {
        outline = GetComponent<SpriteOutline>();
        if (outline == null)
            throw new System.Exception("No outline on object " + name);
        outline.enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled || other.gameObject.layer != 6) return;

        outline.enabled = true;
        BearNearBy = true;
        UIController.Instance.SetInfoButtonsState(interactionName, true);
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (!enabled || other.gameObject.layer != 6) return;
        
        outline.enabled = false;
        BearNearBy = false;
        UIController.Instance.SetInfoButtonsState(interactionName, false);
    }

    public void Activate()
    {
        enabled = true;
        bubble.gameObject.SetActive(true);
    }

    public virtual void OnDayStart(int dayIndex) {}
    public virtual void OnDayEnd(int dayIndex) {}
    public virtual void OnUse(BearMovement bear)
    {
        bubble.gameObject.SetActive(false);
        outline.enabled = false;
        BearNearBy = false;
        UIController.Instance.SetInfoButtonsState(interactionName, false);
    }
}
