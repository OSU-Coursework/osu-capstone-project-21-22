using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPlaceBoards : Task
{
    public Socketable _boardA;
    public Socketable _boardB;
    public Socketable _boardC;
    public Socketable _boardD;

    private string _taskDescription;
    
    void Awake()
    {
        _taskDescription = string.Format("Place boards in the green areas");
    }

    // Update is called once per frame
    protected override void Update()
    {
        // run parent update function
        base.Update();

        // update task
        if (_boardA.AttachedToSocket &&
            _boardB.AttachedToSocket &&
            _boardC.AttachedToSocket &&
            _boardD.AttachedToSocket)
        {
            _taskComplete = true;
        }
    }

    public override string ToString()
    {
        return _taskDescription;
    }
}
