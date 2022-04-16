using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable objects don't have a velocity...
/// Attach this to an object for a quick way to get a value to use.
/// </summary>

public class GenericVelocityCalculator : MonoBehaviour
{
    private Vector3 _velocity;
    private Vector3 _lastPos;

    public float getMagnitude()
    {
        return _velocity.magnitude;
    }

    void Start()
    {
        _lastPos = transform.position;
    }

    void Update()
    {
        _velocity = transform.position - _lastPos;
                
        // update pos for next frame
        _lastPos = transform.position;

    }
}
