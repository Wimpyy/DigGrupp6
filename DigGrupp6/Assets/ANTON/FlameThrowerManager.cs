using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerManager : MonoBehaviour
{
    [SerializeField] Renderer renderer;
    [SerializeField] AmmoTypeClass ammo;
    [SerializeField] Color endColor;
    private float t;

    private void Start()
    {
        t = 0;
        Vector3 awayDirection = transform.position - Camera.main.transform.position;
        Quaternion awayRotation = Quaternion.LookRotation(awayDirection);

        transform.GetChild(0).rotation = awayRotation;
    }

    private void Update()
    {
        t += Time.deltaTime / ammo.bulletLifeTime;
        renderer.material.color = Color.Lerp(renderer.material.color, endColor,t);

    }

}
