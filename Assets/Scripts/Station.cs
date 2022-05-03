using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject LetterBubble;
    public GameObject DrezinaBubble;
    public BoxCollider2D Collider;
    public LetterBox LetterBox;

    public void SetEndStation()
    {
        LetterBubble.gameObject.SetActive(false);
        DrezinaBubble.gameObject.SetActive(false);
        Collider.enabled = true;
        LetterBox.enabled = false;
    }

    public void SetStartStation()
    {
        LetterBubble.gameObject.SetActive(true);
        DrezinaBubble.gameObject.SetActive(false);
        Collider.enabled = false;
        LetterBox.enabled = true;
    }

    public void OnLetterTaken()
    {
        LetterBox.OnOpened();
        LetterBubble.gameObject.SetActive(false);
        DrezinaBubble.gameObject.SetActive(true);
        LetterBox.enabled = false;
    }
    
}
