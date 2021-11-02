using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTitleDisplay : MonoBehaviour
{
    public Text taskTitleText;

    public GameObject FloorStationPrefab;

    private FloorStationMaster FloorStationScript = null;

    /*
        0 = band boards
        1 = rim joists
        2 = joist hangers
        3 = joistboards 
    */

    private string[] taskTitles = { 
        "Place Band Boards",
        "Place Rim Joists",
        "Place Joist Hangers",
        "Place Joists"
    };

    void Start()
    {
        if (FloorStationPrefab !=  null)
        {
            this.FloorStationScript = FloorStationPrefab.GetComponent<FloorStationMaster>();
            
        }
    }

    private void Update()
    {
        int currentStage = this.FloorStationScript.GetCurrentStage();
        taskTitleText.text = taskTitles[currentStage];
    }
}