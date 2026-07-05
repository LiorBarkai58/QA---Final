using System;
using UnityEngine;

public class BossMovement : MonoBehaviour

{
    public float range = 2f;      // How far left/right from the center
    public float speed = 2f;      // How fast it moves

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * range;
        transform.position = startPosition + new Vector3(x, 0f, 0f);
    }
}
