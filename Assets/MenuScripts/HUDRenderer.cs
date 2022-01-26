using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDRenderer : MonoBehaviour
{
    private bool visible = false;
    public bool isRightHand = true;

    private void Awake()
    {
        // if this is a right hand object and should be active, but isn't
        if (OptionState.RightHandDominant && !isRightHand)
        {
            visible = true;
            EnableHUD(true);
        }
        // Enable this if 
        else if (!OptionState.RightHandDominant && isRightHand)
        {
            visible = true;
            EnableHUD(true);
        }
        else
        {
            visible = false;
            EnableHUD(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // if this is a right hand object and should be active, but isn't
        if (OptionState.RightHandDominant && !isRightHand && !visible)
        {
            visible = true;
            EnableHUD(true);
        }
        // Enable this if 
        else if (!OptionState.RightHandDominant && isRightHand && !visible)
        {
            visible = true;
            EnableHUD(true);
        }
        // if this is a right hand to be disabled
        else if (!OptionState.RightHandDominant && !isRightHand && visible)
        {
            visible = false;
            EnableHUD(false);
        }
        // if this is a left hand to be disabled
        else if (OptionState.RightHandDominant && isRightHand && visible)
        {
            visible = false;
            EnableHUD(false);
        }

        if (!visible)
        {
            EnableHUD(false);
        }
    }

    private void EnableHUD(bool mode)
    {
        GetComponent<MeshRenderer>().enabled = mode;
        transform.GetChild(0).GetComponent<Canvas>().enabled = mode;
        transform.GetChild(1).GetComponent<MeshRenderer>().enabled = mode;
        transform.GetChild(2).GetComponent<MeshRenderer>().enabled = mode;
        transform.GetChild(3).GetComponent<MeshRenderer>().enabled = mode;
        transform.GetChild(4).GetComponent<MeshRenderer>().enabled = mode;
        transform.GetChild(5).GetComponent<MeshRenderer>().enabled = mode;
    }
}
