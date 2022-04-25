using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToDoListContents : MonoBehaviour
{

    public Text toDoListText;

    public GameObject FloorStationPrefab;

    private FloorStationMaster FloorStationScript = null;

    private string[] taskTitles = { 
        "Place Band Boards\n",
        "Place Rim Joists\n",
        "Place Joist Hangers\n",
        "Place Joists\n"
    };

    // Start is called before the first frame update
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
        toDoListText.text = "";
        int currentStage = this.FloorStationScript.GetCurrentStage();
        for(int i = 0; i < taskTitles.Length; i++)
        {
            if (currentStage > i){
                toDoListText.text += "\u2611 ";
            }else{
                toDoListText.text += "\u2610 ";
            }
            toDoListText.text += taskTitles[i];
        }

    }
}
