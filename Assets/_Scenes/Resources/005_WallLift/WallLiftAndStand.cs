using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLiftAndStand : MonoBehaviour
{
    private bool _standing = false;
    public bool Standing { get { return _standing; } }

    private Vector3 _standingPosition = new Vector3(90, 0, 0);

    // wall should be at standing position +/- this threshold to
    //   be considered standing.
    private float _threshold = 3.0f;

    // for remembering wall position on standing.
    private Vector3 _fixedPosition;

    // for destroying component once wall is standing.
    private HingeJoint _hingeRef = null;

    private void Awake()
    {
        transform.gameObject.TryGetComponent(out _hingeRef);

        if (_hingeRef == null)
        {
            Debug.LogError("ERROR :: " +
                            this.GetType().ToString() +
                            " :: Wall object has no hinge joint!");
        }
    }

    void Update()
    {
        // check if wall is within standing threshold
        Vector3 wallAngle = transform.rotation.eulerAngles;
        if (!_standing &&
            wallAngle.x >= _standingPosition.x - _threshold &&
            wallAngle.x <= _standingPosition.x + _threshold)
        {
            _fixedPosition = transform.position;
            _standing = true;
        }

        // fix position and rotation if standing
        if (_standing)
        {
            transform.position = _fixedPosition;
            transform.eulerAngles = _standingPosition;

            // destroy hingejoint to prevent weird behavior
            if (_hingeRef)
            {
                Destroy(_hingeRef);
            }
        }
    }
}
