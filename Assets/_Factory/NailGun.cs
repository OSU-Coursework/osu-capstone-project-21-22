using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class NailGun : MonoBehaviour
{

    public float Range = 100f;

    //Delay between when the nail gun can fire
    public float FireDelay = 0.2f;

    //The location to cast the hit ray from
    public Transform NailOrigin;


    //The prefab to spawn at the hit location
    public GameObject NailObject;

    // The action associated with controller triggers
    public SteamVR_Action_Boolean TriggerPress;

    //Private Variables

    //The time since the last fire
    float FireTime = 0f;

    //Is the nail gun delaying until the next fire?
    bool InFireDelay;


    private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        // Call the TriggerPressed function when the trigger of either controller is
        // pressed
        TriggerPress.AddOnStateDownListener(TriggerPressed, SteamVR_Input_Sources.Any);
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            if (TriggerPress[source].stateDown)
            {
                Fire();
            }
        }
    }

    void TriggerPressed(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
    {

        Debug.Log("Nail gun trigger pulled");
        // If trigger is pressed, check if we're still in fire delay
        if (InFireDelay)
        {
            // If yes and FireTime is less than FireDelay, update the FireTime
            if (FireTime < FireDelay)
                FireTime += Time.deltaTime;
            // Otherwise, FireTime >= FireDelay so we're not in the fire delay anymore
            else
                InFireDelay = false;
        }
        // We're not in the fire delay, so fire
        else
        {
            // Set InFireDelay to true because we just fired
            InFireDelay = true;

            // Reset the fire time
            FireTime = 0f;

            // Fire the nailgun
            Fire();
        }
    }

    void Fire()
    {
        //Use a RaycastHit to check if the nailgun would hit something
        RaycastHit HitInfo;
        
        //Check if we hit something
        if(Physics.Raycast(NailOrigin.position, NailOrigin.forward, out HitInfo, Range))
        {
            Debug.Log("Nail would hit something");
            //Do some hot vector math to determine how the nail should be oriented

            //Calculate the vector from the nail gun to the hit point
            Vector3 Ray = NailOrigin.position - HitInfo.point;

            //Then get the angle between the up vector and the ray
            float Angle = Vector3.Angle(new Vector3(0, 1, 0), Ray);

            //Then get the axis to rotate the nail about by using the cross product
            Vector3 Axis = Vector3.Cross(new Vector3(0, 1, 0), Ray);

            //Now we can spawn the nail at the hit point and set the rotation to match the angle it was fired from
            Instantiate(NailObject, HitInfo.point, Quaternion.AngleAxis(Angle, Axis));
        }
        else
        {
            Debug.Log("Nail would not hit something");
        }
    }
}
