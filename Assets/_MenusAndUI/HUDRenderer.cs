using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Valve.VR.InteractionSystem;

public class HUDRenderer : MonoBehaviour
{
    private bool visible = false;
    public bool isRightHand = true;

    //private void Awake()
    //{
    //    // if this is a right hand object and should be active, but isn't
    //    if (OptionState.RightHandDominant && !isRightHand)
    //    {
    //        visible = true;
    //        EnableHUD(true);
    //    }
    //    // Enable this if 
    //    else if (!OptionState.RightHandDominant && isRightHand)
    //    {
    //        visible = true;
    //        EnableHUD(true);
    //    }
    //    else
    //    {
    //        visible = false;
    //        EnableHUD(false);
    //    }
    //}

    //// Update is called once per frame
    //private void Update()
    //{
    //    // if this is a right hand object and should be active, but isn't
    //    if (OptionState.RightHandDominant && !isRightHand && !visible)
    //    {
    //        visible = true;
    //        EnableHUD(true);
    //    }
    //    // Enable this if 
    //    else if (!OptionState.RightHandDominant && isRightHand && !visible)
    //    {
    //        visible = true;
    //        EnableHUD(true);
    //    }
    //    // if this is a right hand to be disabled
    //    else if (!OptionState.RightHandDominant && !isRightHand && visible)
    //    {
    //        visible = false;
    //        EnableHUD(false);
    //    }
    //    // if this is a left hand to be disabled
    //    else if (OptionState.RightHandDominant && isRightHand && visible)
    //    {
    //        visible = false;
    //        EnableHUD(false);
    //    }

    //    if (!visible)
    //    {
    //        EnableHUD(false);
    //    }

    //    // make the UI invisible
    //    if (transform.parent.parent.parent.gameObject.GetComponent<Hand>().currentAttachedObject != null)
    //    {
    //        transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = false;
    //        EnableHUD(false);
    //    }
    //    // make the UI visible
    //    else
    //    {
    //        if ((OptionState.RightHandDominant && !isRightHand) || (!OptionState.RightHandDominant && isRightHand))
    //        {
    //            transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled = true;
    //            EnableHUD(true);
    //        }
    //    }
    //}

    //private void EnableHUD(bool mode)
    //{
    //    GetComponent<MeshRenderer>().enabled = mode;
    //    transform.GetChild(0).GetComponent<Canvas>().enabled = mode;
    //    transform.GetChild(1).GetComponent<MeshRenderer>().enabled = mode;
    //    transform.GetChild(2).GetComponent<MeshRenderer>().enabled = mode;
    //    transform.GetChild(3).GetComponent<MeshRenderer>().enabled = mode;
    //    transform.GetChild(4).GetComponent<MeshRenderer>().enabled = mode;
    //    transform.GetChild(5).GetComponent<MeshRenderer>().enabled = mode;
    //}
}
