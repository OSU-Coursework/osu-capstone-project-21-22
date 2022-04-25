using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentScript : MonoBehaviour
{
 

    public string BuildItemTag;


    public FloorStationHelper.SubpartState CurrentState = FloorStationHelper.SubpartState.Hidden;
    

    public Material SolidMaterial;
    public Material GhostMaterial;

    public bool EnableOnStart = false;

    private bool IsEnabled = false;

   

    // Start is called before the first frame update
    void Start()
    {
        this.SetState(this.CurrentState);

        if (this.EnableOnStart)
        {
            this.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Enable()
    {
        this.IsEnabled = true;
        if (this.CurrentState == FloorStationHelper.SubpartState.Hidden)
        {
            this.SetState(FloorStationHelper.SubpartState.Ghost);
        }
    }

    public void Disable()
    {
        this.IsEnabled = false;
        if (this.CurrentState != FloorStationHelper.SubpartState.Placed)
        {
            this.SetState(FloorStationHelper.SubpartState.Hidden);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("----OnTriggerEnter----");
        //Debug.Log("----BuildItemTag: " + BuildItemTag + "----");
        if (this.IsEnabled)
        {
            //Debug.Log("----ObjectIsEnabled----");
            //Debug.Log("Expecting to receive item tag: " + BuildItemTag + ". Received: " + other.gameObject.tag);
            if (other.gameObject.tag == BuildItemTag && this.CurrentState != FloorStationHelper.SubpartState.Placed)
            {
                //Debug.Log("The build item has entered the trigger");
                this.SetState(FloorStationHelper.SubpartState.Placed);
                Destroy(other.gameObject);
            }
        }
        else
        {
            //Debug.Log("----OBJECT DISABLED----");
        }

    }

    public void SetState(FloorStationHelper.SubpartState newState)
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

    public void MakeComplete()
    {
        Debug.Log("Making component complete");
        this.SetState(FloorStationHelper.SubpartState.Placed);
        this.Disable();
    }
}
