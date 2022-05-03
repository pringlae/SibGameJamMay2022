using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearMovement : MonoBehaviour
{
    public float speed;
    public new Camera camera;
    public float maxMoveFromCamera;
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
        change.x = Input.GetAxisRaw("Horizontal");
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    void MoveCharacter()
    {
        Vector3 newPosition = transform.position + change * speed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, camera.transform.position.x - maxMoveFromCamera, camera.transform.position.x + maxMoveFromCamera);
        myRigidbody.MovePosition(newPosition);
    }
}
