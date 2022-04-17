using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailSocket : Socket
{
    [SerializeField] private Transform _nailHammeredPosition;
    public Transform NailHammeredPosition { get { return _nailHammeredPosition; } }

    // exposed bool for managing state
    private bool _hasBeenNailed = false;
    public bool HasBeenNailed
    {
        get => _hasBeenNailed;
        set => _hasBeenNailed = value;
    }
}
