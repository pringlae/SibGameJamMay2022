using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drezina : MonoBehaviour
{
    public bool playerInRange;
    public GameObject player;
    public GameObject car;
    public GameObject carReady;

    void Start()
    {
        
    }


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
