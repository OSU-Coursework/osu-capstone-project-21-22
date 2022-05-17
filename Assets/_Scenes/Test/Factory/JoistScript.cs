using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoistScript : MonoBehaviour
{

    public string BuildItemTag;


    public FloorStationHelper.SubpartState CurrentState = FloorStationHelper.SubpartState.Hidden;
    public GameObject Object;

    public Material SolidMaterial;
    public Material GhostMaterial;

    private bool IsEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        this.SetState(this.CurrentState);
        this.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void OnTriggerEnter(Collider other)
    {

        if (this.IsEnabled)
        {
            if (other.gameObject.tag == BuildItemTag && this.CurrentState != FloorStationHelper.SubpartState.Placed)
            {
                Debug.Log("The build item has entered the trigger");
                this.SetState(FloorStationHelper.SubpartState.Placed);
                Destroy(other.gameObject);
            }
        }
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
        this.gameObject.SetActive(false);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void MakeGhost()
    {
        this.gameObject.SetActive(true);
        this.gameObject.GetComponent<MeshRenderer>().material = GhostMaterial;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void MakeSolid()
    {
        this.gameObject.SetActive(true);
        this.gameObject.GetComponent<MeshRenderer>().material = SolidMaterial;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
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
