using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;

public class TouchController : MonoBehaviour
{
    // Reference to the touchpad press action
    public SteamVR_Action_Boolean TouchpadPress;
    // Reference to the touchpad movement value
    public SteamVR_Action_Vector2 TouchpadValue;

    // Value determining how sensitively touchpad movements translate to character movement
    public float MovementSensitivity = 0.1f;
    // The maximum speed at which the character can move
    public float MovementMaxSpeed = 1.0f;

    // The character controller
    private CharacterController Controller;
    // Reference to SteamVR camera rig
    private Transform CameraRig;
    // Reference to the character head (position of headset)
    private Transform CharacterHead;

    // How fast the character is moving
    private float MovementSpeed = 0.0f;

    private void Awake()
    {
        // Get a reference to the character controller
        Controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the transforms for the SteamVR camera rig and the character's
        // head
        CameraRig = SteamVR_Render.Top().origin;
        CharacterHead = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the character when their head (headset) moves
        Rotate();
        // Move the character if touchpad is being pressed
        Move();
    }
    
    void Rotate()
    {
 
            // Store the current position and rotation of the camera rig so they can
            // be restored after rotating the character; this will rotate the character
            // without rotating the camera rig
            Vector3 oldPos = CameraRig.position;
            Quaternion oldRotation = CameraRig.rotation;

            // Rotate the character
            transform.eulerAngles = new Vector3(0.0f, CharacterHead.rotation.eulerAngles.y, 0.0f);

            // Restore the position and rotation of the camera rig
            CameraRig.position = oldPos;
            CameraRig.rotation = oldRotation;
   
    }

    void Move()
    {
        // Get the current rotation of the character controller and turn it into a quaternion
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        // The movement that will be calculated and performed by the character controller
        Vector3 movement = Vector3.zero;

        // If the touchpad isn't being pressed, set the movement speed to 0
        if (TouchpadPress.GetStateUp(SteamVR_Input_Sources.Any))
            MovementSpeed = 0;

        // If the touchpad is being rpressed and moved, move the character
        if (TouchpadPress.state)
        {
            // Increase the MovementSpeed based on the movement of the touchpad and sensitivty
            MovementSpeed += TouchpadValue.axis.y * MovementSensitivity;
            // Limit the speed of the character to between -/+ MovementSpeed
            MovementSpeed = Mathf.Clamp(MovementSpeed, -MovementMaxSpeed, MovementMaxSpeed);

            // Calculate the movement using the time since last frame and movement speed;
            // multiply with the direction we're facing to move in that direction
            movement += orientation * (MovementSpeed * Vector3.forward) * Time.deltaTime;
        }

        // Perform the movement on the character controller
        Controller.Move(movement);
    }
}
