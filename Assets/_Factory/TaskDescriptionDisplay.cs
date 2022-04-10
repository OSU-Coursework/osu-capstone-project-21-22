using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDescriptionDisplay : MonoBehaviour
{
    public Text TaskDescriptionText;

    private string[] TaskDescriptions = {
        "\n\n- Go to the board storage area.\n" +
        "- Pick up a band board and carry it over to the building table and place it\n" +
        "- Repeat the same thing for the 2nd band board.",

        "\n\n- Go to the board storage area.\n" +
        "- Pick up a rim joist and carry it over to the building table and place it\n" +
        "- Repeat the same thing for the 2nd band board.",

        "- Pick up joist hangers and start installing them one by one by nailing them to the band board.\n" +
        "- Once the joist hangers are installed, pick up a floor joist and install it.",
        
        "\n\n- Pick up a joist from the board storage area.\n" +
        "- Start with one end (rim joist) and continue with the floor joists\n" +
        "- Lay joists on joist hangers and secure to joist hanger."
    };

    public GameObject FloorStationPrefab;

    private FloorStationMaster FloorStationScript = null;

    void Start()
    {
        if (FloorStationPrefab !=  null)
        {
            this.FloorStationScript = FloorStationPrefab.GetComponent<FloorStationMaster>();
            
        }
    }

    void Update()
    {
        int currentStage = this.FloorStationScript.GetCurrentStage();
        TaskDescriptionText.text = TaskDescriptions[currentStage];
    }
}
