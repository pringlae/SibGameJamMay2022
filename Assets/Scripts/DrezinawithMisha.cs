using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrezinawithMisha : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

 
    public bool playerInRange;
    public GameObject player;
    public GameObject car;
    public GameObject carReady;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetActive(true);
            car.SetActive(true);
            carReady.SetActive(false);

 //         player.transform.position = transform.position;
            player.transform.position = new Vector3(transform.position.x, -0.1f, transform.position.z);
            car.transform.position = transform.position;

        }
    }

        void MoveCharacter()
    {
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.fixedDeltaTime
            );
    }
}