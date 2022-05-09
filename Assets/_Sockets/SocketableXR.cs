using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketableXR : MonoBehaviour
{

    public bool _canBeSocketed = true;
    private Rigidbody _rigidbody;
    private Socket _visibleSocket;
    public Socket VisibleSocket {get {return _visibleSocket; } }

    // these flags are useful for managing the state of a
    //   socketable object.
    private bool _inSocketZone;
    private bool _attachedToSocket;
    public bool AttachedToSocket { get { return _attachedSocket; } }
    // by keeping a reference to the socket this item is
    //   attached to, we can use its values directly
    //   and not leak into other visible sockets
    protected Socket _attachedSocket;

    private XRGrabInteractable _interactable;
    private XRController _interactor;

    public void Awake()
    {
       // get handle for XR script
       _interactable = GetComponent<XRGrabInteractable>();
       _interactable.OnActivate += AttachedToSocket;
       _interactable.OnDeactivate += DetachFromSocket;
       // get handle for attached rigidbody to disable
       //   gravity when needed

       _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Update()
    {
       // reset the rigidbody
       if (_rigidbody == null && GetComponent<Rigidbody>() != null)
       {
           _rigidbody = GetComponent<Rigidbody>();
       }

       // reset the interactable
       if (_interactable == null && GetComponent<XRGrabInteractable>() != null)
       {
           _interactable = GetComponent<XRGrabInteractable>();
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

    public void AttachToSocket(XRController hand)
    {
       // if inside socket zone while being let go from hand, attach to socket.
       if (!_attachedToSocket && _inSocketZone && !_visibleSocket.HoldingSocketable)
       {
           if (_visibleSocket.AllowedObjectType == null ||
               gameObject.name.Contains(_visibleSocket.AllowedObjectType.gameObject.name))
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

    public void DetachFromSocket(XRController hand)
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

    public void OnTriggerStay(Collider other)
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

    public void OnTriggerExit(Collider other)
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
