using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Detatch : MonoBehaviour
{
    public void AttachToSocket()
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
}
