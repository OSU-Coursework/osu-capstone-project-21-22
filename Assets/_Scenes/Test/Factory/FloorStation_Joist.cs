using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStation_Joist : MonoBehaviour
{

    

    public GameObject Hanger1;
    public GameObject Hanger2;
    public GameObject Joist;

    public bool EnableOnStart = false;

    private ComponentScript Hanger1Script;
    private ComponentScript Hanger2Script;
    private ComponentScript JoistScript;


    private bool IsEnabled = false;
    private bool IsComplete = false;




    private int state = 0; //0 for joist hangers, 1 for joist, 2 for done

    // Start is called before the first frame update
    void Start()
    {
        Hanger1Script = Hanger1.GetComponent<ComponentScript>();
        Hanger2Script = Hanger2.GetComponent<ComponentScript>();
        JoistScript = Joist.GetComponent<ComponentScript>();

        Hanger1Script.Disable();
        Hanger2Script.Disable();
        JoistScript.Disable();

        if (this.EnableOnStart)
        {
            this.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsEnabled == true && this.IsComplete == false)
        {
            switch (this.state)
            {
                case 0: this.CheckHangers();
                    break;
                case 1: this.CheckJoist();
                    break;
            }
        }
    }

    public void Enable()
    {
        this.IsEnabled = true;
        if(this.state == 0)
        {
            Hanger1Script.Enable();
            Hanger2Script.Enable();
            JoistScript.Disable();
        }
        if(this.state == 1)
        {
            Hanger1Script.Disable();
            Hanger2Script.Disable();
            JoistScript.Enable();
        }
    }

    public void Disable()
    {
        this.IsEnabled = true;
        Hanger1Script.Disable();
        Hanger2Script.Disable();
        JoistScript.Disable();
    }


    public bool GetCompleted()
    {
        return this.IsComplete;
    }

    private void CheckHangers()
    {
        if(Hanger1Script.GetCompleted() && Hanger2Script.GetCompleted())
        {
            this.state++;
            Hanger1Script.Disable();
            Hanger2Script.Disable();
            JoistScript.Enable();
        }
    }

    private void CheckJoist()
    {
        if (JoistScript.GetCompleted())
        {
            this.state++;
            JoistScript.Disable();
            this.IsComplete = true;
            Debug.Log("Joist is complete");
        }
    }

    public void MakeComplete()
    {
        Hanger1Script.MakeComplete();
        Hanger2Script.MakeComplete();
        JoistScript.MakeComplete();

        this.IsComplete = true;
        this.state = 2;
        this.IsEnabled = false;
    }
}
