using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearMovement : MonoBehaviour
{
    public float speed;
    public new Camera camera;
    public float maxMoveFromCamera;
    public DrezinawithMisha drezina;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public SceneInteraction itemHeld { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public bool itemOnHand => itemHeld != null && itemHeld.transform.parent == transform;

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

    public void TakeItem(SceneInteraction item)
    {
        if (itemHeld != null && itemHeld != item)
        {
            itemHeld.transform.parent = null;
            itemHeld.transform.position = new Vector3(itemHeld.transform.position.x, -0.3f, itemHeld.transform.position.z);
            itemHeld.enabled = true;
            itemHeld.SetNearBy();
        }
        itemHeld = item;
        itemHeld.transform.parent = transform;
        itemHeld.transform.localPosition = new Vector3(0.02f, -0.02f, 0);
        itemHeld.enabled = false;
    }

    public void PutItem()
    {
        itemHeld.transform.parent = drezina.transform;
        itemHeld.transform.localPosition = new Vector3(0.11f, 0f, 0f);
    }

    public void ClearItem()
    {
        itemHeld.gameObject.SetActive(false);
        itemHeld = null;
    }
}
