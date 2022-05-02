using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrezinawithMisha : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxis("Horizontal");
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveXD", change.x);
            animator.SetBool("movingXD", true);
        }
        else
        {
            animator.SetBool("movingXD", false);
        }
    }
    void MoveCharacter()
    {
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.fixedDeltaTime
            );
    }
}