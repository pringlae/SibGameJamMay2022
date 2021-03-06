using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{

    public float speed;
    public float x;
    Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Repeat(Time.time * speed, x);
        transform.position = startPosition + new Vector2(-offset, 0);
    }
}
