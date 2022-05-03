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
    public BearMovement player;
    public new GameObject camera;

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
                UIController.Instance.SetMovementState(true, topKey);
            }
            else
            {
                animator.SetInteger("moving", 0);
            }
        }

    }

    public void Reset()
    {
        GetOff();

        myRigidbody.velocity = Vector2.zero;
        inputReceived = false;
        UIController.Instance.SetInfoButtonsState(UIController.InfoButton.None);
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
                UIController.Instance.SetInfoButtonsState(UIController.InfoButton.Brake, true);
                if (Input.GetKey(KeyCode.Space))
                    myRigidbody.velocity = Vector3.MoveTowards(myRigidbody.velocity, Vector3.zero, brakingSpeed * Time.deltaTime);
            }
            else
            {
                UIController.Instance.SetInfoButtonsState(UIController.InfoButton.GetOff, true);
                if (Input.GetKeyDown(KeyCode.Space))
                    GetOff();
            }
            return;
        }

        if (empty && playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetOn();
                return;
            }
            if (player.itemHeld != null && Input.GetKeyDown(KeyCode.E))
            {
                if (player.itemOnHand)
                {
                    player.PutItem();
                    UIController.Instance.SetInfoButtonsState(UIController.InfoButton.Take, true); 
                }
                else
                {
                    player.TakeItem(player.itemHeld);
                    UIController.Instance.SetInfoButtonsState(UIController.InfoButton.Put, true); 
                }
                return;
            }
        }
    }

    void GetOff()
    {
        player.gameObject.SetActive(true);
        animator.SetBool("Empty", true);
        animator.SetInteger("moving", 0);
        empty = true;
        outline.enabled = true;

        player.transform.position = new Vector3(transform.position.x, -0.1f, transform.position.z);
        UIController.Instance.SetInfoButtonsState(UIController.InfoButton.GetOn, true);
        UIController.Instance.SetMovementState(false);
    }

    void GetOn()
    {
        DaysController.Instance.StartTime();
        player.gameObject.SetActive(false);
        animator.SetBool("Empty", false);
        empty = false;
        outline.enabled = false;
        UIController.Instance.SetInfoButtonsState(UIController.InfoButton.GetOff, true);
        UIController.Instance.SetMovementState(true, topKey);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;

        if (other.gameObject.layer == 6) // player
        {
            playerInRange = true;
            UIController.Instance.SetInfoButtonsState(UIController.InfoButton.GetOn, true);
            if (player.itemHeld != null)
            {
                if (player.itemOnHand)
                    UIController.Instance.SetInfoButtonsState(UIController.InfoButton.Put, true); 
                else
                    UIController.Instance.SetInfoButtonsState(UIController.InfoButton.Take, true);
            }
            outline.enabled = true;
        }
        if (other.gameObject.layer == 7 && !onHouseBlock) // house
        {
            onHouseBlock = true;
            UIController.Instance.SetMovementState(false);
            DaysController.Instance.EndDay();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (!enabled) return;

        if (other.gameObject.layer == 6) // player
        {
            playerInRange = false;
            UIController.Instance.SetInfoButtonsState(UIController.InfoButton.GetOn, false);
            if (player.itemHeld != null)
            {
                if (player.itemOnHand)
                    UIController.Instance.SetInfoButtonsState(UIController.InfoButton.Put, false); 
                else
                    UIController.Instance.SetInfoButtonsState(UIController.InfoButton.Take, false);
            }
            outline.enabled = false;
        }
    }
}