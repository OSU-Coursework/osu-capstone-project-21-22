using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningDisplay : MonoBehaviour
{
    public Text WarningText;

    public GameObject FloorStationPrefab;

    private FloorStationMaster FloorStationScript = null;

    void Start()
    {
        if (FloorStationPrefab !=  null)
        {
            this.FloorStationScript = FloorStationPrefab.GetComponent<FloorStationMaster>();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        int currentStage = this.FloorStationScript.GetCurrentStage();
        if(currentStage != 2) // Not equal to joist hangers ie anything else
        {
            WarningText.text = "This should be a two person carry.";
        }
        else
        {
            WarningText.text = "";
        }
    }
}
