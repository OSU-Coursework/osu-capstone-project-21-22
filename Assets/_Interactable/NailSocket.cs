using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailSocket : MonoBehaviour
{
    public bool HoldingSocketable = false;

    private void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = !HoldingSocketable;
    }
}
