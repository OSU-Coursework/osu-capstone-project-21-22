using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController Controller;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;



    public float Speed = 10f;
    public float Gravity = -9.81f;

    Vector3 Velocity;

    bool IsOnGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Check if player is standing on the ground
        IsOnGround = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        //Get movement inputs
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");

        Vector3 MoveDirection = transform.forward * Z + transform.right * X;

        Controller.Move(MoveDirection * Speed * Time.deltaTime);


        if(IsOnGround && Velocity.y < 0)
        {
           Velocity.y = -2f;
        }

        Velocity.y += Gravity * Time.deltaTime;

        Controller.Move(Velocity * Time.deltaTime);
    }
}
