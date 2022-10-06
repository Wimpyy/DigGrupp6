using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerManager : MonoBehaviour
{
    [SerializeField] Renderer renderer;
    [SerializeField] AmmoTypeClass ammo;
    [SerializeField] Vector3 endSize;
    [SerializeField] float colorFade, sizeFade;
    [SerializeField] Color endColor;

    private void Start()
    {
        Vector3 awayDirection = transform.position - Camera.main.transform.position;
        Quaternion awayRotation = Quaternion.LookRotation(awayDirection);

        transform.GetChild(0).rotation = awayRotation;
    }

    private void Update()
    {
        Material mat = new Material(renderer.material);
        renderer.material = mat;
        renderer.material.color = Color.Lerp(mat.color, endColor, colorFade);
        transform.localScale = Vector3.Lerp(transform.localScale, endSize, sizeFade);

    }

}
