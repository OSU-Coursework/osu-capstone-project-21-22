using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStation_SideRunner : MonoBehaviour
{

    public FloorStationHelper.SubpartState CurrentState = FloorStationHelper.SubpartState.Hidden;
    public GameObject Object;

    public Material SolidMaterial;
    public Material GhostMaterial;

    private bool IsEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        //Hide the object
        this.SetState(this.CurrentState);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsEnabled)
        {
            //check for updates
        }
    }

    public void Enable()
    {
        this.IsEnabled = true;
        this.SetState(FloorStationHelper.SubpartState.Ghost);
    }

    public void Disable()
    {
        this.IsEnabled = true;
    }

    public void SetState(FloorStationHelper.SubpartState newState)
    {

        if (this.IsEnabled)
        {
            this.CurrentState = newState;

            switch (newState)
            {
                case FloorStationHelper.SubpartState.Hidden:
                    this.MakeHidden();
                    break;
                case FloorStationHelper.SubpartState.Ghost:
                    this.MakeGhost();
                    break;
                case FloorStationHelper.SubpartState.Placed:
                    this.MakeSolid();
                    break;
            }
        }
    }

    private void MakeHidden()
    {
        this.Object.SetActive(false);
        this.Object.GetComponent<BoxCollider>().enabled = false;
    }

    private void MakeGhost()
    {
        this.Object.SetActive(true);
        this.Object.GetComponent<MeshRenderer>().material = GhostMaterial;
        this.Object.GetComponent<BoxCollider>().enabled = false;
    }

    private void MakeSolid()
    {
        this.Object.SetActive(true);
        this.Object.GetComponent<MeshRenderer>().material = SolidMaterial;
        this.Object.GetComponent<BoxCollider>().enabled = true;
    }

    public bool GetCompleted()
    {
        if (this.CurrentState == FloorStationHelper.SubpartState.Placed)
        {
            return true;
        }
        return false;
    }
}
