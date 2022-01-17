using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    private bool _holdingSocketable;
    public bool HoldingSocketable
    {
        get { return _holdingSocketable; }
        set { _holdingSocketable = value; }
    }

    [SerializeField] private Transform _attachTransform;
    public Transform AttachTransform { get { return _attachTransform; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Socketable>())
        {
            Debug.Log("Colliding with " + other.name);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Socketable>())
        {
            Debug.Log("No longer colliding with " + other.name);
        }
    }
}
