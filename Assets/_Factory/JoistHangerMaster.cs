using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoistHangerMaster : MonoBehaviour
{
    public List<GameObject> JoistHangerObjects = new List<GameObject>();

    public int AutoCompleteCount;

    private int CurrentHangerIndex = 0;
    private int HangerCount;

    private bool IsEnabled = false;
    private bool IsComplete = false;

    private GameObject CurrentJoist;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            JoistHangerObjects.Add(this.transform.GetChild(i).gameObject);
            //Debug.Log("Child name: " + this.transform.GetChild(i).transform.name);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsEnabled)
        {
            int completed_count = 0;

            foreach(GameObject g in this.JoistHangerObjects)
            {
                ComponentScript hangerscript = g.GetComponent<ComponentScript>();
                if (hangerscript != null)
                {
                    if (hangerscript.GetCompleted())
                    {
                        ++completed_count;
                        if(completed_count >= this.AutoCompleteCount)
                        {
                            Debug.Log("JoistHangerMaster has acheived auto_complete count and is now complete");
                            this.IsComplete = true;
                            this.MakeComplete();
                            break;

                        }
                    }
                }
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
        foreach (GameObject g in this.JoistHangerObjects)
        {
            //Get the script that controls the side walls
            ComponentScript hangerscript = g.GetComponent<ComponentScript>();
            if (hangerscript != null)
            {
                hangerscript.Enable();
            }
            else
            {
                Debug.Log("Error getting script for joist hanger child!");
            }
        }
    }

    public void Disable()
    {
        this.IsEnabled = false;

        //Disable all subobjects too!
        foreach (GameObject g in this.JoistHangerObjects)
        {
            //Get the script that controls the side walls
            ComponentScript hangerscript = g.GetComponent<ComponentScript>();
            if (hangerscript != null)
            {
                hangerscript.Disable();
            }
        }
    }

    public void MakeComplete()
    {
        foreach(GameObject go in this.JoistHangerObjects)
        {
            ComponentScript hangerscript = go.GetComponent<ComponentScript>();
            if (hangerscript != null)
            {
                hangerscript.MakeComplete();
            }
        }
        this.IsComplete = true;
    }
}
