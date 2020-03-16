using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxTurnSpeed;
    [SerializeField] private float minAcc;
    [SerializeField] private float maxAcc;
    [SerializeField] private float maxDec;
    [SerializeField,Range(0f,1f)] private float accuracy;

    private Rigidbody2D myRb;
    private Vector2 moveDirection;
    private float currentAcc;

    private void Awake()
    {
        
        myRb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        ProcessInput();
        
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
        
    }

    private void ProcessInput()
    {
        Vector2 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        moveDirection = mouseWorldPos - (Vector2) transform.position;
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
        myRb.velocity +=(Vector2) transform.up * currentAcc * accuracy;
        myRb.velocity = Vector2.ClampMagnitude(myRb.velocity,maxSpeed);
        if(currentAcc == 0)
            myRb.velocity = myRb.velocity * maxDec;
    }
    private void Rotate()
    {
        Vector2 turnVector = (moveDirection.normalized - (Vector2) transform.up); 
        turnVector = Vector2.ClampMagnitude(turnVector, maxTurnSpeed);
        turnVector *= myRb.velocity.magnitude/maxSpeed;
        //Debug.Log();
        transform.up += (Vector3) turnVector ;
    }
}
