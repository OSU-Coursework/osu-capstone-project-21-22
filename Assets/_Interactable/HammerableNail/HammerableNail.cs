using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HammerableNail : Socketable
{
    // nails will only be allowed to be nailed when in a nail socket.
    private bool _canBeNailed = false;

    // a reference to the child nail object is useful for attaching
    //   the mesh to the board once it has been nailed.
    [SerializeField] private GameObject _nail;

    // how hard does the nail have to be hit for it to enter the board?
    [SerializeField] private float _impactStrengthToNail;

    protected override void LateUpdate()
    {
        if (AttachedToSocket && _attachedSocket.GetType() == typeof(NailSocket))
        {
            _canBeNailed = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Stop if this is not in the nail area
        if (!_canBeNailed)
        {
            return;
        }
        // If the colliding object is NOT a hammer of any kind, stop this event
        if (!(other.gameObject.name == "Hammer" || other.gameObject.name == "SocketableHammer" || other.gameObject.name.Contains("Hammer (") || other.gameObject.name.Contains("SocketableHammer (")))
        {
            return;
        }
        
        // If the collision speed is not fast enough, stop
        if (other.relativeVelocity.magnitude < _impactStrengthToNail)
        {
            return;
        }

        // move nail model position to nailed position
        NailSocket socket = _attachedSocket as NailSocket;
        _nail.transform.position = socket.NailHammeredPosition.position;

        // set nail model parent to the socket parent
        GameObject board = _attachedSocket.transform.parent.gameObject;
        _nail.transform.SetParent(board.transform);

        // disable the socket
        _attachedSocket.gameObject.SetActive(false);

        // disable self
        gameObject.SetActive(false);
    }

    //private void NailObject()
    //{
    //    RaycastHit hit;
    //    Vector3 offset = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    //    offset -= (-transform.up * 0.074f);
    //    Ray ray = new Ray(offset, -transform.up);
    //    // if there is a raycast collision
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        // if a collider has been hit
    //        if (hit.collider != null)
    //        {
    //            // if the object can be nailed
    //            if (hit.collider.gameObject.GetComponent<HammerableBoard>() != null)
    //            {
    //                gameObject.AddComponent<HingeJoint>();
    //                gameObject.GetComponent<HingeJoint>().connectedBody = hit.collider.gameObject.GetComponent<Rigidbody>();
    //                gameObject.GetComponent<HingeJoint>().axis = new Vector3(0, 1, 0);
    //            }
    //        }
    //    }
    //}
}
