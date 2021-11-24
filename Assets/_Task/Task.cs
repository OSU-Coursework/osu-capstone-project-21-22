using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This base class serves as the template for creating
/// a game object that can be watched by the TaskWatcher.
/// It provides extendable functions for defining when
/// a task is completed.
/// </summary>
[AddComponentMenu("Task Management/Task")]
public class Task : MonoBehaviour
{
    public string TaskName = "";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(string.Format("Task Started: {0}", TaskName));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
