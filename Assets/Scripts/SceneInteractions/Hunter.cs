using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hunter : SceneInteraction
{
    public Animator animator;
    public BoxCollider2D trigger;

    public override void OnDayStart(int dayIndex)
    {
        if (dayIndex == 11)
        {
            trigger.enabled = true;
            animator.SetTrigger("StandUp");
            enabled = true;
        }
        else
        {
            trigger.enabled = false;
            enabled = false;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        enabled = true;
        UIController.Instance.SetInfoButtonsState(UIController.InfoButton.None);
        Animal.rescued["Hunter"] = true;
        StartCoroutine(ToFinal());
    }

    private IEnumerator ToFinal()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Final");
    }
}
