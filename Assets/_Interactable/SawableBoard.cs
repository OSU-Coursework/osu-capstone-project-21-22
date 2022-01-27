using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SawableBoard : MonoBehaviour
{
    // whether it can be used to saw
    public bool active = false;

    // whether it is being sawed
    private bool _inUse = false;

    // whether this is colliding with a saw
    private bool hittingSaw = false;

    // we can use the onAttachedToHand/onDetachedFromHand
    //   events on an interactable to trigger socket attach
    //   and release methods.
    private Interactable _interactable;
    // a reference to the objects rigidbody will allow us to
    //   disable gravity so that the object hovers in the socket.
    private Rigidbody _rigidbody;
    // a socket is visible when an object is inside of its
    //   collision boundary.
    private Socket _visibleSocket;

    private Socket _lastSocket;

    // these flags are useful for managing the state of a
    //   socketable object.
    private bool _inSocketZone;
    public bool _attachedToSocket;

    // when this count hits 0, the board breaks
    private int hits = 10;
    private bool broken = false;

    // this keeps track of whether the board has been initialized
    private bool _doneInit = false;

    // the max size of the board, when put together
    public int max_length = 5;

    // how long the left board should be
    [Header("Size of the left board")]
    public float board1Len;

    [Header("Related Objects")]
    // the default board object
    public GameObject defaultBoard;

    // the first board object
    public GameObject Board1;

    // the second board object
    public GameObject Board2;

    void Awake()
    {
        if (active)
        {
            InitBoards();
        }
    }

    public void InitBoards()
    {
        _doneInit = true;

        // get handle for steamvr interactable script
        _interactable = GetComponent<Interactable>();

        // register socket functions with interactable events
        _interactable.onAttachedToHand += DetachFromSocket;
        _interactable.onDetachedFromHand += AttachToSocket;

        // get handle for attached rigidbody to disable
        //   gravity when needed
        _rigidbody = GetComponent<Rigidbody>();

        // Spawn a default board
        if (Board1 == null)
        {
            Board1 = Instantiate(defaultBoard);
            Board1.transform.parent = transform.GetChild(0);
        }
        // Spawn another default board
        if (Board2 == null)
        {
            Board2 = Instantiate(defaultBoard);
            Board2.transform.parent = transform.GetChild(0);
        }
        // destroy the physics of both boards
        Destroy(Board1.GetComponent<Throwable>());
        Destroy(Board1.GetComponent<Interactable>());
        Destroy(Board1.GetComponent<Rigidbody>());
        Destroy(Board2.GetComponent<Throwable>());
        Destroy(Board2.GetComponent<Interactable>());
        Destroy(Board2.GetComponent<Rigidbody>());
        Board1.GetComponent<BoxCollider>().enabled = false;
        Board2.GetComponent<BoxCollider>().enabled = false;

        if (Board1.GetComponent<Socketable>() != null) Board1.GetComponent<Socketable>()._canBeSocketed = false;
        if (Board1.GetComponent<SawableBoard>() != null) Board1.GetComponent<SawableBoard>().active = false;
        // make board2 usable
        if (Board2.GetComponent<Socketable>() != null) Board2.GetComponent<Socketable>()._canBeSocketed = false;
        if (Board2.GetComponent<SawableBoard>() != null) Board2.GetComponent<SawableBoard>().active = false;

        float ratio = (float)board1Len / (float)max_length;
        float size = (float)ratio * max_length;

        // Change the length of board1
        Board1.transform.localScale = new Vector3(Board1.transform.localScale.x, size, Board1.transform.localScale.z);

        // Change the length of board2
        Board2.transform.localScale = new Vector3(Board2.transform.localScale.x, max_length - size, Board2.transform.localScale.z);

        // Move the boards into their positions
        Board1.transform.localPosition = new Vector3((-0.153f * Board1.transform.localScale.y), 0f, 0f);
        Board1.transform.localEulerAngles = new Vector3(-90f, 0f, 90f);
        Board2.transform.localPosition = new Vector3((0.153f * Board2.transform.localScale.y), 0f, 0f);
        Board2.transform.localEulerAngles = new Vector3(-90f, 0f, 90f);

        // Set the size of the box
        Component[] boxes = GetComponents(typeof(BoxCollider));
        foreach (BoxCollider box in boxes)
        {
            // only get the non-trigger box
            if (!box.isTrigger)
            {
                // fix the scale
                box.size = new Vector3(max_length*0.3f, Board1.transform.localScale.x * 0.04f, Board1.transform.localScale.z*0.1f);
                box.center = new Vector3(0.5f-ratio, 0f, 0f);
            }
        }
    }

    void Update()
    {
        if (active && !_doneInit)
        {
            InitBoards();
        }
        if (!active)
        {
            transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            return;
        }
        if (_inUse && !transform.GetChild(1).GetComponent<MeshRenderer>().enabled)
        {
            transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
        }
        else if (!_inUse && transform.GetChild(1).GetComponent<MeshRenderer>().enabled)
        {
            transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        }
        // Move the boards into their positions
        Board1.transform.localPosition = new Vector3((-0.153f * Board1.transform.localScale.y), 0f, 0f);
        Board1.transform.localEulerAngles = new Vector3(-90f, 0f, 90f);
        Board2.transform.localPosition = new Vector3((0.153f * Board2.transform.localScale.y), 0f, 0f);
        Board2.transform.localEulerAngles = new Vector3(-90f, 0f, 90f);
        // if attached to socket, disable gravity to
        //   'hover' object and keep at socket position
        if (_visibleSocket != null)
        {
            _lastSocket = _visibleSocket;
        }
        if (_attachedToSocket)
        {
            _inUse = true;
            if (_rigidbody != null)
            {
                _rigidbody.useGravity = false;
            }
            // just use the socket position
            if (_lastSocket != null)
            {
                // fix the position and rotation
                transform.position = _lastSocket.AttachTransform.position;
                if (_lastSocket.gameObject.name == "SawBoardSocket" ||
                    _lastSocket.gameObject.name.Contains("SawBoardSocket (")) _lastSocket.AttachTransform.localEulerAngles = new Vector3(0, 90, 90);
                transform.rotation = _lastSocket.AttachTransform.rotation;
            }
        }

    }

    private void AttachToSocket(Hand hand)
    {
        // if inside socket zone while being let go from hand, attach to socket.
        if (_inSocketZone && !_visibleSocket.HoldingSocketable)
        {
            // otherwise, complete attaching
            _attachedToSocket = true;
            _visibleSocket.HoldingSocketable = true;
            _rigidbody.useGravity = false;
            if (_visibleSocket._vanishOnUse) _visibleSocket.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void DetachFromSocket(Hand hand)
    {
        // if attached to socket while being grabbed by hand, release from socket.
        if (_attachedToSocket)
        {
            _attachedToSocket = false;
            _visibleSocket.HoldingSocketable = false;
            _rigidbody.useGravity = true;
            _visibleSocket.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (!active) return;
        // don't run unless colliding with a socket.
        if (other.gameObject.name == "SawBoardSocket" || other.gameObject.name.Contains("SawBoardSocket ("))
        {
            // Remove any velocity
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            // set other values
            _inUse = true;
            _inSocketZone = true;
            _visibleSocket = other.GetComponent<Socket>();
            _lastSocket = _visibleSocket;
            // if this is a sawblade, break instantly
            if (hittingSaw && _attachedToSocket)
            {
                BreakBoard();
                return;
            }
        }
        // if in a socket and hitting a saw
        if (other.gameObject.name == "SawHitbox" && _visibleSocket != null)
        {
            hittingSaw = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!active) return;
        // don't run unless colliding with a socket.
        if (other.gameObject.name == "SawBoardSocket" || other.gameObject.name.Contains("SawBoardSocket ("))
        {
            _inUse = false;
            _inSocketZone = false;
            _visibleSocket = null;
            _rigidbody.useGravity = true;
        }
        if (other.gameObject.name == "SawHitbox")
        {
            hittingSaw = false;
        }
    }

    private void DecHitCount()
    {
        // do nothing if this is broken
        if (broken || !_inUse) return;
        // add a hit, then test if the hitcount is done
        hits -= 1;
        if (hits <= 0)
        {
            broken = true;
            BreakBoard();
        }
    }

    private void BreakBoard()
    {
        // Free the socket
        _attachedToSocket = false;
        _lastSocket.HoldingSocketable = false;
        _rigidbody.useGravity = true;
        _inUse = false;
        _inSocketZone = false;
        _lastSocket = null;
        _visibleSocket = null;

        // Reparent the boards
        Board1.transform.parent = transform.parent;
        Board2.transform.parent = transform.parent;

        // make the first board 
        Board1.GetComponent<BoxCollider>().enabled = true;
        Board1.gameObject.AddComponent<Rigidbody>();
        Board1.gameObject.AddComponent<Interactable>();
        Board1.gameObject.AddComponent<Throwable>();

        // make the second board interactable
        Board2.GetComponent<BoxCollider>().enabled = true;
        Board1.gameObject.AddComponent<Rigidbody>();
        Board1.gameObject.AddComponent<Interactable>();
        Board2.gameObject.AddComponent<Throwable>();

        // make board1 usable
        if (Board1.GetComponent<Socketable>() != null) Board1.GetComponent<Socketable>()._canBeSocketed = true;
        if (Board1.GetComponent<SawableBoard>() != null) Board1.GetComponent<SawableBoard>().active = true;
        // make board2 usable
        if (Board2.GetComponent<Socketable>() != null) Board2.GetComponent<Socketable>()._canBeSocketed = true;
        if (Board2.GetComponent<SawableBoard>() != null) Board2.GetComponent<SawableBoard>().active = true;

        // Destroy this object
        GameObject.Destroy(gameObject);
    }
}
