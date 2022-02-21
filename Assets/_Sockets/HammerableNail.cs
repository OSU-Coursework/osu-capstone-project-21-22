using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HammerableNail : Socketable
{
    private bool canBeNailed = true;
    private NailSocket _lastSocket;
    public bool _fromNailgun;

    private void OnCollisionEnter(Collision other)
    {
        // Stop if this is not in the nail area
        if (!canBeNailed)
        {
            return;
        }
        // If the colliding object is NOT a hammer of any kind, stop this event
        if (!(other.gameObject.name == "Hammer" || other.gameObject.name == "SocketableHammer" || other.gameObject.name.Contains("Hammer (") || other.gameObject.name.Contains("SocketableHammer (")))
        {
            return;
        }
        // If the parent is socketable, and it is NOT in a socket
        // we should return
        if (_lastSocket != null)
        {
            if (_lastSocket.transform.parent.GetComponent<Socketable>() != null)
            {
                if (!_lastSocket.transform.parent.GetComponent<Socketable>().AttachedToSocket)
                {
                    return;
                }
            }
        }
        // If the collision speed is not fast enough, stop
        if (other.relativeVelocity.magnitude < 0.6)
        {
            return;
        }
        TryConnect();
    }


    private void NailObject()
    {
        RaycastHit hit;
        Vector3 offset = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        offset -= (-transform.up * 0.074f);
        Ray ray = new Ray(offset, -transform.up);
        // if there is a raycast collision
        if (Physics.Raycast(ray, out hit))
        {
            // if a collider has been hit
            if (hit.collider != null)
            {
                // if the object can be nailed
                if (hit.collider.gameObject.GetComponent<HammerableBoard>() != null)
                {
                    gameObject.AddComponent<HingeJoint>();
                    gameObject.GetComponent<HingeJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
                    gameObject.GetComponent<HingeJoint>().axis = new Vector3(0, 1, 0);
                }
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    // don't run unless colliding with a socket.
    //    if (other.gameObject.name == "NailSocket" || other.gameObject.name.Contains("NailSocket ("))
    //    {
    //        // Remove any velocity
    //        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    //        // set other values
    //        canBeNailed = true;
    //        _inSocketZone = true;
    //        _visibleSocket = other.GetComponent<NailSocket>();
    //        _lastSocket = _visibleSocket;
    //        if (_fromNailgun)
    //        {
    //            _attachedToSocket = true;
    //            _visibleSocket.HoldingSocketable = true;
    //            TryConnect();
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    // don't run unless colliding with a socket.
    //    if (other.gameObject.name == "NailSocket" || other.gameObject.name.Contains("NailSocket ("))
    //    {
    //        canBeNailed = false;
    //        _inSocketZone = false;
    //        _visibleSocket = null;
    //    }
    //}

    private void TryConnect()
    {
        if (_lastSocket != null)
        {
            var hit = _lastSocket;
            // Copy the hologram transform
            Transform newpos = hit.gameObject.transform.GetChild(1).transform;
            // Destroy all physical properties
            Destroy(GetComponent<Throwable>());
            Destroy(GetComponent<Interactable>());
            Destroy(GetComponent<CapsuleCollider>());
            // Reparent this to the board, then destroy the socket
            gameObject.transform.SetParent(hit.gameObject.transform.parent);
            //Destroy(_visibleSocket.gameObject);
            Destroy(GetComponent<Rigidbody>());
            // Fix the position and make the visible socket null
            transform.position = newpos.position;
            transform.rotation = newpos.rotation;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY |
                    RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            //NailObject();
        }
    }
}
