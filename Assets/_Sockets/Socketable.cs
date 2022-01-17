using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Socketable : MonoBehaviour
{
    private Interactable _interactable;
    private Rigidbody _rigidbody;
    private Socket _visibleSocket;

    [SerializeField] private bool _inSocketZone;
    private bool _attachedToSocket;

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
                transform.position = _visibleSocket.AttachTransform.position; 
                transform.rotation = _visibleSocket.AttachTransform.rotation;
            }
            else
            {
                transform.position = _visibleSocket.transform.position;
            }
        }

    }

    private void AttachToSocket(Hand hand)
    {
        // if inside socket zone while being let go, attach to socket
        if (_inSocketZone && !_visibleSocket.HoldingSocketable)
        {
            _attachedToSocket = true;
            _visibleSocket.HoldingSocketable = true;
        }
    }

    private void DetachFromSocket(Hand hand)
    {
        if (_attachedToSocket)
        {
            _attachedToSocket = false;
            _visibleSocket.HoldingSocketable = false;
            _rigidbody.useGravity = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Socket>())
        {
            _inSocketZone = true;
            _visibleSocket = other.GetComponent<Socket>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Socket>())
        {
            _inSocketZone = false;
            _visibleSocket = null;
        }
    }
}
