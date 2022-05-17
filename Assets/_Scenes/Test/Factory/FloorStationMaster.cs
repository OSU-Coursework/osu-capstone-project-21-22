using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStationMaster : MonoBehaviour
{

    public GameObject SideWallsMasterObject;
    public GameObject HeadersMasterObject;
    public GameObject JoistsMasterObject;
    public GameObject JoistHangerMasterObject_1;
    public GameObject JoistHangerMasterObject_2;

    private SideWallsMaster SWMScript;// = SideWallsMasterObject.GetComponent<SideWallsMaster>();
    private HeadersMaster HMScript;// = HeadersMasterObject.GetComponent<HeadersMaster>();
    private JoistsMaster JMScript;// = JoistsMasterObject.GetComponent<JoistsMaster>();

    private JoistHangerMaster JHMScript_1;
    private JoistHangerMaster JHMScript_2;



    public int CurrentStage = 0;
    //0 == SideWalls
    //1 == Headers
    //2 == Joist Hangers
    //3 == Joists
    //4 == Completed


    private bool IsComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        SWMScript = SideWallsMasterObject.GetComponent<SideWallsMaster>();
        HMScript = HeadersMasterObject.GetComponent<HeadersMaster>();
        JMScript = JoistsMasterObject.GetComponent<JoistsMaster>();

        JHMScript_1 = JoistHangerMasterObject_1.GetComponent<JoistHangerMaster>();
        JHMScript_2 = JoistHangerMasterObject_2.GetComponent<JoistHangerMaster>();

        this.CompletePreviousStages();

        this.ActivateCurrentStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.IsComplete)
        {
            this.CheckCurrentStage();
        }
    }

    public bool GetCompleted()
    {
        return this.IsComplete;
    }

    private void ActivateCurrentStage()
    {
        switch (this.CurrentStage)
        {
            case 0: SWMScript.Enable();
                break;
            case 1: HMScript.Enable();
                break;
            case 2:
                {
                    JHMScript_1.Enable();
                    JHMScript_2.Enable();
                }
                break;
            case 3: JMScript.Enable();
                break;
            case 4: this.IsComplete = true;
                break;
        }
    }

    public int GetCurrentStage()
    {
        return this.CurrentStage;
    }

    private void CheckCurrentStage()
    {
        switch (this.CurrentStage)
        {
            case 0: this.CheckSideWalls();
                break;
            case 1: this.CheckHeaders();
                break;
            case 2: this.CheckJoistHangers();
                break;
            case 3: this.CheckJoists();
                break;
        }
    }

    private void CompleteStage(int stage)
    {
        switch (stage)
        {
            case 0:  SWMScript.MakeComplete();
                break;
            case 1: HMScript.MakeComplete();
                break;
            case 2:
                {
                    JHMScript_1.MakeComplete();
                    JHMScript_2.MakeComplete();
                }
                break;
            case 3: JMScript.MakeComplete();
                break;
        }
    }

    private void CheckSideWalls()
    {
        if (SWMScript.GetCompleted())
        {
            SWMScript.Disable();
            this.CurrentStage += 1;

            this.ActivateCurrentStage();
        }
    }

    private void CheckHeaders()
    {
        if (HMScript.GetCompleted())
        {
            HMScript.Disable();
            this.CurrentStage += 1;
            this.ActivateCurrentStage();
        }
    }

    private void CheckJoists()
    {
        if (JMScript.GetCompleted())
        {
            JMScript.Disable();
            this.CurrentStage += 1;
            this.ActivateCurrentStage();
        }
    }

    private void CheckJoistHangers()
    {
        if(JHMScript_1.GetCompleted() && JHMScript_2.GetCompleted())
        {
            JHMScript_1.Disable();
            JHMScript_2.Disable();
            this.CurrentStage += 1;
            this.ActivateCurrentStage();
        }
    }

    private void CompletePreviousStages()
    {
        for(int i = 0; i < this.CurrentStage; ++i)
        {
            this.CompleteStage(i);
        }
    }
}
