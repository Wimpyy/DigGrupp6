using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBush : MonoBehaviour
{
    Collider coll;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.BoxCast(coll.bounds.center, coll.bounds.extents / 2, Vector3.zero, Quaternion.identity, 0, playerLayer))
        {

        }
    }
}
