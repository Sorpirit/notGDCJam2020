using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    [SerializeField] private float frontMaxSpeed;
    [SerializeField] private float acc; 

    [Range(0f,1f)]
    [SerializeField] private float drag;
    [SerializeField] private float dec;
    [SerializeField] private float turnSpeed;

    [SerializeField] private float carHight;
    [SerializeField] private float accuracy;
    [SerializeField] private LayerMask groung;
    [SerializeField] private Transform frontSide;
    [SerializeField] private Transform backSide;
    [SerializeField] private Transform rightSide;
    [SerializeField] private Transform leftSide;
    [SerializeField] private Transform botomCenter;
    [SerializeField] private bool useKeyboardInput;

    private Vector2 inpuDirection;
    private bool blockMovment = false;

    public Vector2 InputDir{get => inpuDirection; set => inpuDirection = value;}
    public bool BlockMovment{get => blockMovment; set => blockMovment = value;}
    


    private Rigidbody carRb;
    private float speed;

    private void Awake()
    {
        carRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(useKeyboardInput)
            inpuDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        ChekHight();
        Chek();
        if(!blockMovment)
            Move();

    }

    private void Move(){
        Vector3 moveDir = carRb.velocity;
        //Debug.Log(Mathf.Sign(inpuDirection.y * transform.forward.z) +" . "+ Mathf.Sign(moveDir.z));
        speed += inpuDirection.y * acc * Time.deltaTime;
        speed *= (1 - drag);
        speed = Mathf.Clamp(speed,-frontMaxSpeed,frontMaxSpeed);
        if(Input.GetKey(KeyCode.Space)){
            speed -= Mathf.Sign(speed) * dec * Time.deltaTime;
            if(Mathf.Abs(speed) <= 0)
                speed = 0;
        }

        carRb.velocity = new Vector3(transform.forward.x * speed,carRb.velocity.y,transform.forward.z * speed);
        //transform.Rotate(new Vector3(0,0,1) * Time.deltaTime * turnSpeed * inpuDirection.x , Space.World);
        Vector3 rot = (transform.forward + transform.right * inpuDirection.x * (speed/frontMaxSpeed) * turnSpeed * Time.deltaTime).normalized;
        transform.rotation = Quaternion.LookRotation(rot, transform.up);
        //transform.forward += Vector3.right * inpuDirection.x * (speed/frontMaxSpeed) * turnSpeed;
    }

    private void ChekHight(){
        RaycastHit hit;
        if(Physics.Raycast(botomCenter.position,transform.up * -1,out hit,carHight,groung)){
            float delta = carHight - hit.distance;
            if(delta <= accuracy)
                delta = 0;
            if(delta == 0)
            {
                carRb.velocity = new Vector3(carRb.velocity.x,0,carRb.velocity.z);
                carRb.velocity +=  -1 * Physics.gravity * Time.deltaTime;
            }
            else
            {
                carRb.velocity +=  -1 * Vector3.up * Physics.gravity.y * (1 + delta) * Time.deltaTime;
            }
        }
    }

    private void Chek(){
        RaycastHit hit;
        Vector3 sumNormals = Vector3.zero;

        if(Physics.Raycast(frontSide.position,frontSide.up * -1,out hit,carHight,groung)){
             sumNormals += hit.normal;
        }
        if(Physics.Raycast(backSide.position,backSide.up * -1,out hit,carHight,groung)){
            sumNormals += hit.normal;
        }
        if(Physics.Raycast(rightSide.position,rightSide.up * -1,out hit,carHight,groung)){
            sumNormals += hit.normal;
        }
        if(Physics.Raycast(leftSide.position,leftSide.up * -1,out hit,carHight,groung)){
            sumNormals += hit.normal;
        }
        transform.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.up,sumNormals.normalized,Time.deltaTime * 5), -transform.forward);
        transform.Rotate(Vector3.right, 90f);
    }



}
