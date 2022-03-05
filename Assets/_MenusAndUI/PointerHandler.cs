using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using Valve.VR.Extras;
//using Valve.VR.InteractionSystem;

public class PointerHandler : MonoBehaviour
{

    //private Hand hand;
    //private SteamVR_LaserPointer laser;

    //// Initialize the hand
    //void Awake()
    //{
    //    // setup the hand
    //    hand = GetComponent<Hand>();

    //    // setup the laser pointer
    //    laser = GetComponent<SteamVR_LaserPointer>();
    //    if (laser != null)
    //    {
    //        laser.PointerClick += Pclick;
    //        laser.PointerIn += Pin;
    //        laser.PointerOut += Pout;
    //    }
    //}

    //void Update()
    //{
    //    if (laser == null && GetComponent<SteamVR_LaserPointer>() != null)
    //    {
    //        // setup the laser pointer
    //        laser = GetComponent<SteamVR_LaserPointer>();
    //        laser.PointerClick += Pclick;
    //        laser.PointerIn += Pin;
    //        laser.PointerOut += Pout;
    //    }
    //}

    //// on pointer click
    //public void Pclick(object sender, PointerEventArgs e)
    //{
    //    // if there is a button, activate it!
    //    if (e.target.transform.parent.GetComponent<UIElement>() != null)
    //    {
    //        e.target.transform.parent.GetComponent<UIElement>().onHandClick.Invoke( hand );
    //    }
    //}

    //// on pointer enter
    //public void Pin(object sender, PointerEventArgs e)
    //{
    //    // if there is a button, activate it!
    //    if (e.target.transform.childCount > 0)
    //    {
    //        if (e.target.transform.GetChild(0).GetComponent<MeshRenderer>() != null)
    //        {
    //            e.target.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
    //        }
    //    }
    //}

    //// on pointer enter
    //public void Pout(object sender, PointerEventArgs e)
    //{
    //    // if there is a button, activate it!
    //    if (e.target.transform.childCount > 0)
    //    {
    //        if (e.target.transform.GetChild(0).GetComponent<MeshRenderer>() != null)
    //        {
    //            e.target.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    //        }
    //    }
    //}

}
