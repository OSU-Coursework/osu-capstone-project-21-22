using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HammerableBoard : MonoBehaviour
{
    // The gameObject to attach this board to
    public GameObject _attachTarget;

    // These values will keep track of how many nails have been used
    private int nailsNeeded;
    private int nailsDone;

    // Whether the nails are active
    public bool setActive = true;


    // Start is called before the first frame update
    void Awake()
    {
        nailsNeeded = transform.childCount;
        nailsDone = 0;
        // Set the nails as active or inactive
        SetActive(setActive);
    }


    // Change the state of the nails
    public void SetActive(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }


    // Check if all the nails are in
    public void UpdateNails(GameObject nail)
    {
        // increment the nail count
        nailsDone += 1;

        // If this is the last nail, make the object non-movable
        // and fix it's position
        if (nailsDone >= nailsNeeded)
        {
            // reparent and fix the position
            Transform pos = transform;
            transform.SetParent(_attachTarget.transform);
            transform.position = pos.position;
            transform.rotation = pos.rotation;
            
            // Fix the position between this and the target
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = _attachTarget.GetComponent<Rigidbody>();
        }
    }


    // while the nails are active and this is in a socket
    // it should not be interactable or throwable
    public void Update()
    {
        if (gameObject.GetComponent<Socketable>() != null)
        {
            if (gameObject.GetComponent<Socketable>().AttachedToSocket)
            {
                // If the socket is not specific to this object, do not delete!
                if (gameObject.GetComponent<Socketable>().VisibleSocket != null)
                {
                    if (gameObject.GetComponent<Socketable>().VisibleSocket.AllowedObjectType == gameObject)
                    {
                        SetActive(true);
                        // Make this board not interactable anymore
                        Destroy(gameObject.GetComponent<Throwable>());
                        Destroy(gameObject.GetComponent<Interactable>());
                        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY |
                            RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    }
                }
            }
        }
    }
}
