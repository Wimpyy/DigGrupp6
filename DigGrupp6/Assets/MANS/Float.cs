using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public float frequency;
    public Vector3 direction;

    Vector3 startPos;
    float time;
    const float tau = 6.28318530718f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime * frequency;
        time = time % 1;

        transform.position = startPos + (direction * Mathf.Sin(time * tau));
    }
}
