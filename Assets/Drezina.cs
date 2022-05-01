using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drezina : MonoBehaviour
{
    public bool playerInRange;
    public GameObject player;
    public GameObject car;
    public GameObject carReady;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            player.SetActive(false);
            car.SetActive(false);
            carReady.SetActive(true);

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            
        }
    }
}
