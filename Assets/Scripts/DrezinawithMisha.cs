using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrezinawithMisha : MonoBehaviour
{
    public float speed;
    public float brakingSpeed;
    public float moveKeyPressTime = 1f;
    private Rigidbody2D myRigidbody;
    private Animator animator;
    private new SpriteRenderer renderer;
    private SpriteOutline outline;

    public bool MovingRight
    {
        get => !renderer.flipX;
        set 
        {
            Debug.Log(value);
            renderer.flipX = !value;
        }
    }

    public bool Moving => myRigidbody.velocity.magnitude > 0.05f;

 
    private bool playerInRange, empty, inputReceived, onHouseBlock;
    private float movePressKeyTimer = 0;
    private bool topKey = false;
    public GameObject player;
    public GameObject camera;
    public UIController ui;

    void Awake()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        outline = GetComponent<SpriteOutline>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (empty || onHouseBlock) return;
        
        float input = Input.GetAxis("Vertical");
        bool inputMatch = Mathf.Abs(input) > 0.1f && Mathf.RoundToInt(Mathf.Sign(input)) == (topKey ? 1 : -1);

        if (!inputReceived)
        {
            if (inputMatch)
            {
                inputReceived = true;
                animator.SetInteger("moving", topKey ? 1 : -1);
            }
        }

        if (inputMatch && !Input.GetKey(KeyCode.Space))
        {
            myRigidbody.velocity += (MovingRight ? Vector2.right : Vector2.left) * speed * Time.fixedDeltaTime;
        }
        
        if (!Moving) return;

        movePressKeyTimer -= Time.fixedDeltaTime;
        if (movePressKeyTimer < 0)
        {
            movePressKeyTimer = moveKeyPressTime;
            if (inputReceived)
            {
                inputReceived = false;
                topKey = !topKey;
                ui.SetMovementState(true, topKey);
            }
            else
            {
                animator.SetInteger("moving", 0);
            }
        }

    }

    public void Reset()
    {
        myRigidbody.velocity = Vector2.zero;
        empty = true;
        inputReceived = false;
        animator.SetBool("Empty", true);
        ui.SetInfoButtonsState(UIController.InfoButtonsState.OffDrezina);
        outline.enabled = false;
        onHouseBlock = false;
    }

    void Update()
    {
        if (onHouseBlock) return;

        if (!empty)
        {
            if (Moving)
            {
                ui.SetInfoButtonsState(UIController.InfoButtonsState.MovingDrezina);
                if (Input.GetKey(KeyCode.Space))
                    myRigidbody.velocity = Vector3.MoveTowards(myRigidbody.velocity, Vector3.zero, brakingSpeed * Time.deltaTime);
            }
            else
            {
                ui.SetInfoButtonsState(UIController.InfoButtonsState.DrezinaIdle);
                if (Input.GetKeyDown(KeyCode.Space))
                    GetOff();
            }
            return;
        }

        if (empty && playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            GetOn();
            return;
        }
    }

    void GetOff()
    {
        player.SetActive(true);
        animator.SetBool("Empty", true);
        animator.SetInteger("moving", 0);
        empty = true;
        outline.enabled = true;

        player.transform.position = new Vector3(transform.position.x, -0.1f, transform.position.z);
        ui.SetInfoButtonsState(UIController.InfoButtonsState.NearDrezina);
        ui.SetMovementState(false);
    }

    void GetOn()
    {
        DaysController.Instance.StartTime();
        player.SetActive(false);
        animator.SetBool("Empty", false);
        empty = false;
        outline.enabled = false;
        ui.SetInfoButtonsState(UIController.InfoButtonsState.DrezinaIdle);
        ui.SetMovementState(true, topKey);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6) // player
        {
            playerInRange = true;
            ui.SetInfoButtonsState(UIController.InfoButtonsState.NearDrezina);
            outline.enabled = true;
        }
        if (other.gameObject.layer == 7 && !onHouseBlock) // house
        {
            onHouseBlock = true;
            ui.SetMovementState(false);
            DaysController.Instance.EndDay();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6) // player
        {
            playerInRange = false;
            ui.SetInfoButtonsState(UIController.InfoButtonsState.OffDrezina);
            outline.enabled = false;
        }
    }
}