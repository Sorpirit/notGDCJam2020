using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxTurnSpeed;
    [SerializeField] private float minAcc;
    [SerializeField] private float maxAcc;
    [SerializeField] private float maxDec;
    [SerializeField,Range(0f,1f)] private float accuracy;

    private Rigidbody myRb;
    private Vector3 moveDirection;
    private float currentAcc;

    private void Awake()
    {
        
        myRb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        ProcessInput();
        
    }

    private void FixedUpdate()
    {
        // Rotate();
        Move();
        
    }

    private void ProcessInput()
    {
        Vector3 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        moveDirection = mouseWorldPos - transform.position;
        moveDirection = Vector2.ClampMagnitude(moveDirection,maxAcc);

        if(Mathf.Abs(currentAcc) <= maxAcc){
            currentAcc += Input.GetAxisRaw("Vertical") * .5f  * Time.deltaTime ;
            currentAcc *= Input.GetAxisRaw("Vertical");
        }
        else 
            currentAcc = maxAcc;

        Debug.Log(currentAcc);
        moveDirection = moveDirection.normalized;
        //if(currentAcc <= minAcc)
        //    currentAcc = 0;
    }
    private void Move()
    {
        myRb.velocity += transform.forward * currentAcc * accuracy;
        myRb.velocity = Vector3.ClampMagnitude(myRb.velocity,maxSpeed);
        if(currentAcc == 0)
            myRb.velocity = myRb.velocity * maxDec;
    }
    private void Rotate()
    {
        Vector3 turnVector = (moveDirection.normalized - transform.forward); 
        turnVector = Vector2.ClampMagnitude(turnVector, maxTurnSpeed);
        turnVector *= myRb.velocity.magnitude/maxSpeed;
        //Debug.Log();
        transform.forward += (Vector3) turnVector ;
    }
}
