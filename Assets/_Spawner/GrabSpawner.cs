using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;
//using Valve.VR;


//[RequireComponent(typeof(Interactable))]
public class GrabSpawner : MonoBehaviour
{
    // The base object to spawn
    public GameObject spawnObj;

    // the hand types to look for input from
    //public SteamVR_Input_Sources leftHand;
    //public SteamVR_Input_Sources rightHand;

    //// The mesh of this object
    //private Mesh _objMesh;

    //// the hand currently hovering
    //private Hand _currHand;

    //// whether this has recently spawned something
    //private bool _hasSpawnedObj = false;

    //private void Start()
    //{
    //    // reference this object mesh
    //    _objMesh = GetComponent<MeshFilter>().mesh;
    //}


    //// spawn the object, then attach it to the hand
    //private void SpawnObject()
    //{
    //    _hasSpawnedObj = true;
    //    var spawnedItem = Instantiate(spawnObj);

    //    // set the hovering hand, then attach to it
    //    _currHand = GetComponent<Interactable>().hoveringHand;
    //    if (_currHand != null) _currHand.AttachObject(spawnedItem, GrabTypes.Pinch);
    //}


    //private void Update()
    //{

    //    // check if a hand is hovering, and if the hand is NOT holding something
    //    if (!_hasSpawnedObj && GetComponent<Interactable>().hoveringHands.Count > 0)
    //    {
    //        // check for hand input before spawning the object
    //        if (SteamVR_Input.GetStateDown("GrabPinch", leftHand) || SteamVR_Input.GetStateDown("GrabPinch", rightHand) || Input.GetKeyDown((KeyCode)'p'))
    //        {
    //            SpawnObject();
    //        }
    //    }
        
    //    // if the hold key is released, allow spawning again
    //    if (SteamVR_Input.GetStateUp("GrabPinch", leftHand) || SteamVR_Input.GetStateUp("GrabPinch", rightHand) || Input.GetKeyUp((KeyCode)'p'))
    //    {
    //        _hasSpawnedObj = false;
    //    }
    //}
}
