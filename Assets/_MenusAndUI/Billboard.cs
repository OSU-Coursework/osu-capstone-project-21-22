using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    private Camera cam;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the camera is set, look at it
        if (cam != null)
        {
            transform.LookAt(cam.transform.position, Vector3.up);
        }
        
        // Follow the parent's task object (OBJECT)
        if (transform.parent.GetComponent<TaskInteractablePickUp>() != null)
        {
            transform.position = transform.parent.GetComponent<TaskInteractablePickUp>()._interactableGameObject.transform.position + new Vector3(0, 0.4f, 0);
        }
        // Follow the parent's task object (SOCKET)
        if (transform.parent.GetComponent<TaskPlaceInSocket>() != null)
        {
            transform.position = transform.parent.GetComponent<TaskPlaceInSocket>()._socket.transform.position + new Vector3(0, 0.4f, 0);
        }
    }
}
