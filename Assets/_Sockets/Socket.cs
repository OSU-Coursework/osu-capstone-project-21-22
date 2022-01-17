using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    private Socketable _socketableItem;

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
