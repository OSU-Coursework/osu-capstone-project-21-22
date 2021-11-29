using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This base class serves as the template for creating
/// a game object that can be watched by the TaskWatcher.
/// It provides extendable functions for defining when
/// and how a task is completed.
/// 
/// Derived classes should always override Update() and
/// call base.Update() to ensure events are raised.
/// </summary>
[AddComponentMenu("Task Management/Task")]
public class Task : MonoBehaviour
{
    public string _taskName = "";

    [Header("Ordered Tasks")]
    public uint _taskNumber;
    public uint TaskNumber { get { return _taskNumber; } }

    protected bool _taskComplete = false;
    public bool TaskComplete { get { return _taskComplete; } }
    private bool _taskCompleteEventHandled = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Debug.Log(string.Format("Task Started: {0}", _taskName));
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // trigger task complete event if task completed
        if (!_taskCompleteEventHandled && _taskComplete)
        {
            TaskEventManager.RaiseOnTaskComplete();

            _taskCompleteEventHandled = true;
        }
    }

    public override string ToString()
    {
        return _taskName;
    }
}

public class TaskEventManager : MonoBehaviour
{
    // refer to these articles for detailed information regarding
    //   delegates and events
    // https://www.theappguruz.com/blog/use-c-delegates-in-unity
    // https://www.theappguruz.com/blog/use-c-events-in-unity

    public delegate void OnTaskComplete();
    public static event OnTaskComplete onTaskComplete;

    public static void RaiseOnTaskComplete()
    {
        if (onTaskComplete != null)
        {
            onTaskComplete();
        }
    }
}
