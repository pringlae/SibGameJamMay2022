using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimator : MonoBehaviour
{
    public float frameLength;
    public Sprite[] frames;

    private new SpriteRenderer renderer;
    private float untilNext;
    private int currentFrame;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        untilNext = Random.value * frameLength;
        currentFrame = Random.Range(0, frames.Length);
        renderer.sprite = frames[currentFrame];
    }

    // Update is called once per frame
    void Update()
    {
        untilNext -= Time.deltaTime;
        if (untilNext < 0)
        {
            untilNext += frameLength;
            currentFrame = (currentFrame + 1) % frames.Length;
            renderer.sprite = frames[currentFrame];
        }
    }
}
