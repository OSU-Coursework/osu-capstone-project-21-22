using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopSawStay : MonoBehaviour
{
    private Transform oldPos;

    // Start is called before the first frame update
    void Awake()
    {
        oldPos = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = oldPos.position;
    }
}
