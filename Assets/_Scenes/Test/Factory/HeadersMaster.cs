using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadersMaster : MonoBehaviour
{

    private bool IsComplete = false;
    private bool IsEnabled = false;


    public List<GameObject> HeaderObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsEnabled)
        {
            List<GameObject> CompletedObjects = new List<GameObject>();
            //check all objects in WallObjects for completion
            foreach (GameObject g in this.HeaderObjects)
            {
                //Get the script that controls the side walls
                ComponentScript headerscript = g.GetComponent<ComponentScript>();

                //Check if the wall is completed and if so add it to the completed list
                if (headerscript.GetCompleted())
                {
                    CompletedObjects.Add(g);
                }

            }

            //If the counts are the same then all objects are complete
            if (CompletedObjects.Count == HeaderObjects.Count)
            {
                //Set the new state to complete and deactivate the object
                this.IsComplete = true;
                this.IsEnabled = false;
            }
        }
    }

    public bool GetCompleted()
    {
        return this.IsComplete;
    }

    public void Enable()
    {
        this.IsEnabled = true;

        //Enable all subobjects too!
        foreach (GameObject g in this.HeaderObjects)
        {
            //Get the script that controls the side walls
            ComponentScript headerscript = g.GetComponent<ComponentScript>();
            headerscript.Enable();
        }
    }

    public void Disable()
    {
        this.IsEnabled = false;

        //Disable all subobjects too!
        foreach (GameObject g in this.HeaderObjects)
        {
            //Get the script that controls the side walls
            ComponentScript headerscript = g.GetComponent<ComponentScript>();
            headerscript.Disable();
        }
    }

    public void MakeComplete()
    {
        foreach (GameObject g in this.HeaderObjects)
        {
            ComponentScript headerscript = g.GetComponent<ComponentScript>();
            headerscript.MakeComplete();
        }

        this.IsComplete = true;
    }
}
