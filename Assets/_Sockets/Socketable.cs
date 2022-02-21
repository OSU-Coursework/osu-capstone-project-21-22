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
    private Socket _visibleSocket;
    public Socket VisibleSocket { get { return _visibleSocket; } }

    // these flags are useful for managing the state of a
    //   socketable object.
    private bool _inSocketZone;
    private bool _attachedToSocket;
    public bool AttachedToSocket { get { return _attachedSocket; } }
    // by keeping a reference to the socket this item is
    //   attached to, we can use its values directly
    //   and not leak into other visible sockets
    protected Socket _attachedSocket;

    // This flag allows an object to be socketed
    public bool _canBeSocketed = true;

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
        // reset the rigidbody
        if (_rigidbody == null && GetComponent<Rigidbody>() != null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // reset the interactable
        if (_interactable == null && GetComponent<Interactable>() != null)
        {
            _interactable = GetComponent<Interactable>();
            // register socket functions with interactable events
            _interactable.onAttachedToHand += DetachFromSocket;
            _interactable.onDetachedFromHand += AttachToSocket;
        }

        // if attached to socket, disable gravity to
        //   'hover' object and keep at socket position
        if (_attachedToSocket)
        {
            _rigidbody.useGravity = false;

            if (_attachedSocket.AttachTransform != null)
            {
                // this socket has a specialized transform, so make the
                //   object take its position and rotation
                transform.position = _attachedSocket.AttachTransform.position; 
                transform.rotation = _attachedSocket.AttachTransform.rotation;
            }
            else
            {
                // just use the socket position
                transform.position = _attachedSocket.transform.position;
            }
        }

        LateUpdate();
    }

    /// <summary>
    /// Extended classes can implement this to run code
    /// after the normal update routine is complete.
    /// </summary>
    protected virtual void LateUpdate()
    {
        return;
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
                _attachedSocket = _visibleSocket;
                _attachedSocket.HoldingSocketable = true;
                if (_attachedSocket._vanishOnUse)
                {
                    _attachedSocket.GetComponent<MeshRenderer>().enabled = false;
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
            _attachedSocket.HoldingSocketable = false;
            _rigidbody.useGravity = true;
            _attachedSocket.GetComponent<MeshRenderer>().enabled = true;
            _attachedSocket = null;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (!_canBeSocketed) return;
        // don't run unless colliding with a socket.
        if (other.GetComponent<Socket>())
        {
            // do NOT attach to a sawable socket
            if (other.gameObject.name == "SawBoardSocket" || other.gameObject.name.Contains("SawBoardSocket (")) return;
            _inSocketZone = true;
            _visibleSocket = other.GetComponent<Socket>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_canBeSocketed) return;
        // don't run unless colliding with a socket.
        if (other.GetComponent<Socket>())
        {
            // do NOT attach to a sawable socket
            if (other.gameObject.name == "SawBoardSocket" || other.gameObject.name.Contains("SawBoardSocket (")) return;
            _inSocketZone = false;
            _visibleSocket = null;
        }
    }
}
