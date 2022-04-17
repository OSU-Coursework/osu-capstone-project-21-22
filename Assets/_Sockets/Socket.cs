using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to an object with a collider to
/// make it a socket.  It can then hold a game object
/// that has an attached Socketable script.
///
/// The Socket object should have its IsTrigger property
/// checked so it doesn't behave like a physics object.
/// </summary>
public class Socket : MonoBehaviour
{
    // allow socket to know if it's currently holding a socketable
    private bool _holdingSocketable;
    // socketable objects can set and read above value via this property
    public bool HoldingSocketable
    {
        get { return _holdingSocketable; }
        set { _holdingSocketable = value; }
    }

    // vanish the ghost when the socket is in use
    public bool _vanishOnUse = false;
    // only accept sawable boards
    public bool _sawBoardSocket = false;

    // optional game object to define what this socket can hold.
    [SerializeField] private GameObject _allowedObjectType;
    public GameObject AllowedObjectType { get { return _allowedObjectType; } }

    // optional transform to provide specialized position/rotation
    //   of socketable while socketed.
    [SerializeField] private Transform _attachTransform;
    public Transform AttachTransform { get { return _attachTransform; } }

    // these functions don't do anything other than print a message to the console.
    // they're useful for debugging but can ultimately be removed.
    private void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponent<Socketable>())
        //{
        //    Debug.Log("Colliding with " + other.name);
        //}
    }
    
    private void OnTriggerExit(Collider other)
    {
        //if (other.GetComponent<Socketable>())
        //{
        //    Debug.Log("No longer colliding with " + other.name);
        //}
    }
}
