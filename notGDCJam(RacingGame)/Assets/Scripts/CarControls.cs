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

    private Vector2 inpuDirection;
    


    private Rigidbody carRb;
    private float speed;

    private void Awake()
    {
        carRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inpuDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        //carRb.velocity +=  -1 * Physics.gravity * Time.deltaTime;
        ChekHight();
        Vector3 moveDir = carRb.velocity;
        Debug.Log(Mathf.Sign(inpuDirection.y * transform.forward.z) +" . "+ Mathf.Sign(moveDir.z));
        speed += inpuDirection.y * acc * Time.deltaTime;
        speed *= (1 - drag);
        speed = Mathf.Clamp(speed,-frontMaxSpeed,frontMaxSpeed);
        if(Input.GetKey(KeyCode.Space)){
            speed -= Mathf.Sign(speed) * dec * Time.deltaTime;
            if(Mathf.Abs(speed) <= 0)
                speed = 0;
        }

        carRb.velocity = new Vector3(transform.forward.x * speed,carRb.velocity.y,transform.forward.z * speed);
        transform.forward += transform.right * inpuDirection.x * Mathf.Abs(speed/frontMaxSpeed) *  turnSpeed * Time.deltaTime;

    }

    private void ChekHight(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.up * -1,out hit,carHight,groung)){
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

}
