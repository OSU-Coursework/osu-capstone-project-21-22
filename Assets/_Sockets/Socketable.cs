using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Socketable : MonoBehaviour
{
    [SerializeField] private bool _inSocketZone;
    private bool _attachedToSocket;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Socket>())
        {
            _inSocketZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Socket>())
        {
            _inSocketZone = false;
        }
    }
}
