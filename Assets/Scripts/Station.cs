using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject DrezinaBubble;
    public BoxCollider2D Collider;
    public LetterBox LetterBox;

    public void SetEndStation()
    {
        DrezinaBubble.gameObject.SetActive(false);
        Collider.enabled = true;
        LetterBox.enabled = false;
    }

    public void SetStartStation()
    {
        DrezinaBubble.gameObject.SetActive(false);
        Collider.enabled = false;
        LetterBox.Activate();
    }

    public void OnLetterTaken()
    {
        LetterBox.OnUse(null);
        DrezinaBubble.gameObject.SetActive(true);
        LetterBox.enabled = false;
    }
    
}
