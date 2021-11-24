using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoistsMaster : MonoBehaviour
{

    public List<GameObject> JoistObjects = new List<GameObject>();

    public int AutoCompleteCount;

    private int CurrentJoistIndex = 0;
    private int JoistCount;

    private bool IsEnabled;
    private bool IsComplete = false;

    private GameObject CurrentJoist;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < this.transform.childCount; ++i)
        {
            JoistObjects.Add(this.transform.GetChild(i).gameObject);
            //Debug.Log("Child name: " + this.transform.GetChild(i).transform.name);
        }

        /*this.JoistCount = JoistObjects.Count;
        Debug.Log("Joist controller has " + this.JoistCount.ToString() + " joists");
        this.CurrentJoist = this.JoistObjects[this.CurrentJoistIndex];
        FloorStation_Joist joistscript = this.CurrentJoist.GetComponent<FloorStation_Joist>();
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsEnabled)
        {

            int completed_count = 0;

            foreach (GameObject g in this.JoistObjects)
            {
                ComponentScript joistscript = g.GetComponent<ComponentScript>();
                if (joistscript != null)
                {
                    if (joistscript.GetCompleted())
                    {
                        ++completed_count;
                        if (completed_count >= this.AutoCompleteCount)
                        {
                            Debug.Log("JoistMaster has acheived auto_complete count and is now complete");
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

        foreach(GameObject g in this.JoistObjects)
        {
            ComponentScript joistscript = g.GetComponent<ComponentScript>();
            joistscript.Enable();
        }
    }

    public void Disable()
    {
        this.IsEnabled = false;

        //Disable all subobjects too!
        foreach (GameObject g in this.JoistObjects)
        {
            //Get the script that controls the side walls
            ComponentScript joistscript = g.GetComponent<ComponentScript>();
            if (joistscript != null)
            {
                joistscript.Disable();
            }
        }
    }

    public void MakeComplete()
    {
        foreach (GameObject go in this.JoistObjects)
        {
            ComponentScript joistscript = go.GetComponent<ComponentScript>();
            if (joistscript != null)
            {
                joistscript.MakeComplete();
            }
        }
        this.IsComplete = true;
    }
}
