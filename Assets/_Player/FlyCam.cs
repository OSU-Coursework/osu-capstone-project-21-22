using UnityEngine;
using System.Collections;

public class FlyCam : MonoBehaviour
{
    // wasd  :: directional movement
    // qe    :: down/up movement
    // shift :: acceleration
    // space :: locks movement to x and z axes

    public float minSpeed = 5.0f;
    public float maxSpeed = 100.0f;
    public float shiftAcc = 0.1f;
    public float mouseSpeed = 0.25f;

    public bool mouseClickToLook = true;
    public bool lockHorzAxes = false;

    private float speed = 0.0f;
    private Vector3 lastLookPos = new Vector3(0, 0, 0);

    void Update()
    {
        // prevent camera from jumping when in click to look mode 
        if (Input.GetMouseButtonDown(1))
        {
            lastLookPos = Input.mousePosition;
        }

        // update mouse look position
        if (!mouseClickToLook ||
            (mouseClickToLook && Input.GetMouseButton(1)))
        {
            lastLookPos = Input.mousePosition - lastLookPos;
            lastLookPos = new Vector3(-lastLookPos.y * mouseSpeed, lastLookPos.x * mouseSpeed, 0);
            lastLookPos = new Vector3(transform.eulerAngles.x + lastLookPos.x, transform.eulerAngles.y + lastLookPos.y, 0);
            transform.eulerAngles = lastLookPos;
            lastLookPos = Input.mousePosition;
        }

        // keyboard input
        Vector3 moveDir = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            moveDir += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir += new Vector3(1, 0, 0);
		}
		if (Input.GetKey(KeyCode.Q))
        {
            moveDir += new Vector3(0, -1, 0);
		}
		if (Input.GetKey(KeyCode.E))
        {
            moveDir += new Vector3(0, 1, 0);
        }

        // adjust speed
        if (Input.GetKey(KeyCode.LeftShift))  // accelerate with shift key
        {
            speed += shiftAcc;
        }
        else  // normal speed
        {
            speed = minSpeed;
        }
        
        // apply speed
        Vector3 newPos = new Vector3();
        newPos += moveDir * speed;
        newPos.x = Mathf.Clamp(newPos.x, -maxSpeed, maxSpeed);
        newPos.y = Mathf.Clamp(newPos.y, -maxSpeed, maxSpeed);
        newPos.z = Mathf.Clamp(newPos.z, -maxSpeed, maxSpeed);

        // keep movement constant
        newPos = newPos * Time.deltaTime;

        // update position
        if (Input.GetKey(KeyCode.Space)  // lock horz position while space key is held
            || (lockHorzAxes && !(mouseClickToLook && Input.GetMouseButton(1))))  // allow horz movement with right mouse button
        {
            Vector3 newPosition = transform.position;
            transform.Translate(newPos);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else
        {
            transform.Translate(newPos);
        }
    }
}