using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float LookSensitivity = 100f;

    public Transform ControllerBody;

    float XRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //locks cursor so it doesnt leave the window
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        //Get mouse inputs
        float MouseX = LookSensitivity * Time.deltaTime * Input.GetAxis("Mouse X");
        float MouseY = LookSensitivity * Time.deltaTime * Input.GetAxis("Mouse Y");


        XRotation -= MouseY;

        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation, 0, 0);
        ControllerBody.Rotate(Vector3.up * MouseX);
    }
}
