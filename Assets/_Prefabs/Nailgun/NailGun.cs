using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class NailGun : MonoBehaviour
{
    // The nail object to instantiate
    public GameObject nail;

    // we can use the onAttachedToHand/onDetachedFromHand
    //   events on an interactable to trigger socket attach
    //   and release methods.
    private Interactable _interactable;

    // this flag is for checking when the object is in your hand
    private bool isHeld = false;

    // SteamVR hands
    public SteamVR_Input_Sources leftHand;
    public SteamVR_Input_Sources rightHand;

    private void Awake()
    {
        // get handle for steamvr interactable script
        _interactable = GetComponent<Interactable>();

        // register nailgun functions with interactable events
        _interactable.onAttachedToHand += MakeHeld;
        _interactable.onDetachedFromHand += MakeUnheld;
    }

    private void MakeHeld(Hand hand)
    {
        isHeld = true;
    }

    private void MakeUnheld(Hand hand)
    {
        isHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((SteamVR_Input.GetStateDown("GrabGrip", rightHand) || SteamVR_Input.GetStateDown("GrabGrip", leftHand)) && isHeld)
        {
            Vector3 angles = transform.eulerAngles;
            Vector3 pos = transform.position;
            Vector3 newPos = -transform.up + new Vector3(0f, -0.4f, 0.11f);
            var newNail = Instantiate(nail, pos-0.2f*transform.up+0.08f*transform.forward, Quaternion.Euler(angles));
            newNail.GetComponent<Rigidbody>().AddForce(-transform.up * 2, ForceMode.Impulse);
        }
    }
}
