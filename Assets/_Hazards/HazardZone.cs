using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class HazardZone : MonoBehaviour
{

    // if this is set, hands will be tracked in this zone
    // NOTE: This should not be used if the player can walk through this area. this is for tabletop/small areas.
    public bool detectHands = false;

    // if this is set, the mesh will not extend infinitely
    public bool limitMeshHeight = true;


    // the UI object and collision counts
    private HazardDisplay hazardUI;
    private bool headColl = false;
    private bool handColl1 = false;
    private bool handColl2 = false;

    // the player object
    public GameObject player;

    // the hand objects
    public GameObject lHand;
    public GameObject rHand;

    // whether this is in use
    public bool in_use = false;

    void Awake()
    {
        // get the UI
        hazardUI = GameObject.FindGameObjectsWithTag("HazardUI")[0].GetComponent<HazardDisplay>();

        // deactivate the mesh renderer
        GetComponent<MeshRenderer>().enabled = false;

        // make the child very tall if not disabled
        if (!limitMeshHeight) transform.GetChild(0).localScale = new Vector3(1, 1000, 1);

        // if detecting hands, disable the collider
        if (!detectHands) GetComponent<BoxCollider>().enabled = false;
    }

    // change the visibility of the area mesh
    void ToggleMesh()
    {
        // if either fail condition is met
        if ((!headColl && !detectHands) || (!headColl && !handColl1 && !handColl2 && detectHands))
        {
            // hide the box
            transform.GetChild(0).gameObject.SetActive(false);
        }
        // if either true condition is met
        else if (((headColl || handColl1 || handColl2) && detectHands) ||
                (headColl && !detectHands))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        // update the references and check for the player pos
        UpdateRefs();
    }

    void UpdateRefs()
    {
        // if the player is not set, try to set it!
        if (player == null)
        {
            var objs = GameObject.FindGameObjectsWithTag("MainCamera");
            if (objs[0] != null)
            {
                player = objs[0].transform.GetChild(0).GetChild(0).gameObject;
            }
        }
        // if the hand is not set, set it!
        if (lHand == null || rHand == null)
        {
            var objs = Object.FindObjectsOfType<HandCollider>();
            if (objs[0] != null && objs[1] != null)
            {
                lHand = Object.FindObjectsOfType<HandCollider>()[0].gameObject;
                rHand = Object.FindObjectsOfType<HandCollider>()[1].gameObject;
            }
        }

        // if all these are set, get the positions
        if (player != null && lHand != null && rHand != null) CheckPlayerPos();
    }

    void CheckPlayerPos()
    {
        // head stuff
        var playerPos = player.transform.position;

        int x_mod = -1;
        if (transform.position.x < 0) x_mod = 1;

        int y_mod = 1;
        if (transform.position.y < 0) y_mod = -1;

        int z_mod = 1;
        if (transform.position.z < 0) z_mod = -1;

        bool x_in = ((playerPos.x < transform.position.x + x_mod*(transform.localScale.x / 2f)) &&
                    (playerPos.x > transform.position.x - x_mod*(transform.localScale.x / 2f)));
        bool y_in = ((playerPos.y < transform.position.y + y_mod*(transform.localScale.y / 2f)) &&
                    (playerPos.y > transform.position.y - y_mod*(transform.localScale.y / 2f)));
        bool z_in = ((playerPos.z < transform.position.z + z_mod*(transform.localScale.z / 2f)) &&
                    (playerPos.z > transform.position.z - z_mod*(transform.localScale.z / 2f)));

        // if in all of these, set the collision count
        if (x_in && y_in && z_in && !detectHands) headColl = true;
        // otherwise, if not detecting hands, set the count to 0
        else if (!detectHands) headColl = false;

        ToggleMesh();
        UpdateUIState();
    }

    void UpdateUIState()
    {
        // deactivate if nothing that should be detected is present
        // ONLY if NO areas detect anything
        if ((!headColl && !detectHands) || (!headColl && !handColl1 && !handColl2 && detectHands))
        {
            in_use = false;
            int flag = 0;
            foreach (HazardZone zone in Object.FindObjectsOfType<HazardZone>())
            {
                if (zone.in_use) flag = 1;
            }
            if (flag == 0) {
                hazardUI.Deactivate(gameObject);
            }
        }
        
        // send generic message
        if (headColl && !detectHands)
        {
            in_use = true;
            hazardUI.Activate(gameObject, 0);
            hazardUI.SetMessage(0);
        }
        // if there are hands, show the hand message
        if ((headColl || handColl1 || handColl2) && detectHands) 
        {
            in_use = true;
            hazardUI.Activate(gameObject, 1);
            hazardUI.SetMessage(1);
        }
    }


    // handle hands
    void OnTriggerEnter(Collider other)
    {
        // if the head enters
        if (other.gameObject == player) headColl = true;

        // if the hands enter
        if (other.transform.parent.parent.gameObject == lHand) handColl1 = true;
        if (other.transform.parent.parent.gameObject == rHand) handColl2 = true;
    }

    // handle hands
    void OnTriggerExit(Collider other)
    {
        // if the hands are removed
        if (other.transform.parent.parent.gameObject == lHand) handColl1 = false;
        if (other.transform.parent.parent.gameObject == rHand) handColl2 = false;

        // if the head is removed
        if (other.gameObject == player) headColl = false;
    }
}
