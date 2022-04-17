using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSawBoards : Task
{
    public SawableBoard[] _sawableBoards;

    private string _taskDescription;
    private string _taskText = "Saw {0} more boards";

    void Awake()
    {
        if (_sawableBoards.Length == 0)
        {

            Debug.LogError("ERROR :: " +
                            this.GetType().ToString() +
                            " :: contains no GameObjects in list!");
        }

        // set string with initial socket count
        _taskDescription = string.Format(_taskText, _sawableBoards.Length);
    }

    // Update is called once per frame
    protected override void Update()
    {
        // run parent update function
        base.Update();

        int boardCount = 0;
        foreach (SawableBoard board in _sawableBoards)
        {
            // parent sawable board GameObjects are destroyed when cut,
            //   so this array should result in objects with "null" as an
            //   object name after all sawable boards have been destroyed.
            string test = board.ToString();
            if (board.ToString() != "null")
            {
                boardCount++;
            }
        }

        _taskDescription = string.Format(_taskText, boardCount);

        // update task
        if (boardCount == 0)
        {
            _taskComplete = true;
        }
    }
    
    public override string ToString()
    {
        return _taskDescription;
    }
}
