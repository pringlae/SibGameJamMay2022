using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hunter : SceneInteraction
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void OnDayStart(int dayIndex)
    {
        if (dayIndex == 1)
            animator.SetTrigger("StandUp");
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled || other.gameObject.layer != 6) return;

        Animal.rescued["Hunter"] = true;
        StartCoroutine(ToFinal());
    }

    private IEnumerator ToFinal()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Final");
    }
}
