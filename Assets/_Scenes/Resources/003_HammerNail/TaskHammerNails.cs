using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHammerNails : Task
{
    public GameObject[] _hammerableBoards;
    private List<NailSocket> _nailSockets = new List<NailSocket>();

    private string _taskDescription;
    private string _taskText = "Place and hammer {0} more nails";
    
    void Awake()
    {
        if (_hammerableBoards.Length == 0)
        {
            Debug.LogError("ERROR :: " +
                            this.GetType().ToString() +
                            " :: contains no GameObjects in list!");
        }

        foreach (GameObject board in _hammerableBoards)
        {
            // get nail sockets from game object
            NailSocket[] sockets = board.GetComponentsInChildren<NailSocket>();

            if (sockets.Length == 0)
            {
                Debug.LogError("ERROR :: " +
                                this.GetType().ToString() +
                                " :: GameObject with name " +
                                board +
                                " has no nail sockets!");
            }

            foreach (NailSocket socket in sockets)
            {
                _nailSockets.Add(socket);
            }
        }

        // set string with initial socket count
        _taskDescription = string.Format(_taskText, _nailSockets.Count);
    }

    // Update is called once per frame
    protected override void Update()
    {
        // run parent update function
        base.Update();

        int nailCount = 0;
        foreach (NailSocket socket in _nailSockets)
        {
            if (!socket.HasBeenNailed)
            {
                nailCount++;
            }
        }

        _taskDescription = string.Format(_taskText, nailCount);

        // update task
        if (nailCount == 0)
        {
            _taskComplete = true;
        }
    }

    public override string ToString()
    {
        return _taskDescription;
    }
}
