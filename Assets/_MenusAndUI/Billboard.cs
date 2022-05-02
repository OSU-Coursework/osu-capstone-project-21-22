using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // the main camera
    private Camera cam;

    // the subtask to refer to
    public int ref_idx = 0;

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
        // Follow the parent's task object (BOARDS)
        // Set the cursor active only if the board is NOT in a socket
        if (transform.parent.GetComponent<TaskPlaceBoards>() != null)
        {
            if (ref_idx == 0)
            {
                transform.position = transform.parent.GetComponent<TaskPlaceBoards>()._boardA.transform.position + new Vector3(0, 0.4f, 0);
                transform.GetChild(0).gameObject.SetActive(!transform.parent.GetComponent<TaskPlaceBoards>()._boardA.AttachedToSocket);
            }
            if (ref_idx == 1)
            { 
                transform.position = transform.parent.GetComponent<TaskPlaceBoards>()._boardB.transform.position + new Vector3(0, 0.4f, 0);
                transform.GetChild(0).gameObject.SetActive(!transform.parent.GetComponent<TaskPlaceBoards>()._boardB.AttachedToSocket);
            }
            if (ref_idx == 2)
            {
                transform.position = transform.parent.GetComponent<TaskPlaceBoards>()._boardC.transform.position + new Vector3(0, 0.4f, 0);
                transform.GetChild(0).gameObject.SetActive(!transform.parent.GetComponent<TaskPlaceBoards>()._boardC.AttachedToSocket);
            }
            if (ref_idx == 3)
            {
                transform.position = transform.parent.GetComponent<TaskPlaceBoards>()._boardD.transform.position + new Vector3(0, 0.4f, 0);
                transform.GetChild(0).gameObject.SetActive(!transform.parent.GetComponent<TaskPlaceBoards>()._boardD.AttachedToSocket);
            }
        }
        // Follow the parent's task object (NAILS)
        // If the board is nailed, remove the cursor!
        if (transform.parent.GetComponent<TaskHammerNails>() != null)
        {
            transform.position = transform.parent.GetComponent<TaskHammerNails>()._hammerableBoards[ref_idx].transform.position + new Vector3(0, 0.4f, 0);
            int nailCount = 0;
            foreach (NailSocket socket in transform.parent.GetComponent<TaskHammerNails>()._hammerableBoards[ref_idx].GetComponentsInChildren<NailSocket>())
            {
                if (!socket.HasBeenNailed)
                {
                    nailCount++;
                }
            }
            if (nailCount <= 0) transform.GetChild(0).gameObject.SetActive(false);
            else transform.GetChild(0).gameObject.SetActive(true);
        }
        // Follow the parent's task object (SAWS)
        if (transform.parent.GetComponent<TaskSawBoards>() != null)
        {
            transform.position = transform.parent.GetComponent<TaskSawBoards>()._sawableBoards[ref_idx].transform.position + new Vector3(0, 0.4f, 0);
        }
        // Follow the parent's task object (WALL LIFT)
        if (transform.parent.GetComponent<TaskWallLift>() != null)
        {
            transform.position = transform.parent.GetComponent<TaskWallLift>()._wall.transform.position + new Vector3(0, 0.4f, 0);
        }
    }
}
