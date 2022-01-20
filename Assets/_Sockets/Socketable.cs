using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// Attach this script to an interactable game object
/// to allow it to be placed into a socket.
/// </summary>
public class Socketable : MonoBehaviour
{
    // we can use the onAttachedToHand/onDetachedFromHand
    //   events on an interactable to trigger socket attach
    //   and release methods.
    private Interactable _interactable;
    // a reference to the objects rigidbody will allow us to
    //   disable gravity so that the object hovers in the socket.
    private Rigidbody _rigidbody;
    // a socket is visible when an object is inside of its
    //   collision boundary.
    public Socket _visibleSocket;

    // these flags are useful for managing the state of a
    //   socketable object.
    private bool _inSocketZone;
    public bool _attachedToSocket;

    void Awake()
    {
        // get handle for steamvr interactable script
        _interactable = GetComponent<Interactable>();
 
        // register socket functions with interactable events
        _interactable.onAttachedToHand += DetachFromSocket;
        _interactable.onDetachedFromHand += AttachToSocket;

        // get handle for attached rigidbody to disable
        //   gravity when needed
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // if attached to socket, disable gravity to
        //   'hover' object and keep at socket position
        if (_attachedToSocket)
        {
            _rigidbody.useGravity = false;

            if (_visibleSocket.AttachTransform != null)
            {
                // this socket has a specialized transform, so make the
                //   object take its position and rotation
                transform.position = _visibleSocket.AttachTransform.position; 
                transform.rotation = _visibleSocket.AttachTransform.rotation;
            }
            else
            {
                // just use the socket position
                transform.position = _visibleSocket.transform.position;
            }
        }

    }

    private void AttachToSocket(Hand hand)
    {
        // if inside socket zone while being let go from hand, attach to socket.
        if (!_attachedToSocket && _inSocketZone && !_visibleSocket.HoldingSocketable)
        {
            if (_visibleSocket.AllowedObjectType == null ||
                _visibleSocket.AllowedObjectType == this.gameObject)
            {
                _attachedToSocket = true;
                _visibleSocket.HoldingSocketable = true;
                if (_visibleSocket._vanishOnUse)
                {
                    _visibleSocket.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    private void DetachFromSocket(Hand hand)
    {
        // if attached to socket while being grabbed by hand, release from socket.
        if (_attachedToSocket)
        {
            _attachedToSocket = false;
            _visibleSocket.HoldingSocketable = false;
            _rigidbody.useGravity = true;
            _visibleSocket.GetComponent<MeshRenderer>().enabled = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        // don't run unless colliding with a socket.
        if (other.GetComponent<Socket>())
        {
            _inSocketZone = true;
            _visibleSocket = other.GetComponent<Socket>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // don't run unless colliding with a socket.
        if (other.GetComponent<Socket>())
        {
            _inSocketZone = false;
            _visibleSocket = null;
        }
    }
}
