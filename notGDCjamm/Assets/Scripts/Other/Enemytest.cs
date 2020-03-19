using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemytest : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask groundMask;

    private NavMeshAgent agent;
    private CarControls car;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        car = GetComponent<CarControls>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,groundMask))
            {
                agent.Warp(transform.position);
                agent.SetDestination(hit.point);
            }
        }
        Vector2 input = Vector2.zero;
        Vector3 delta = agent.desiredVelocity.normalized - transform.forward;
        delta = delta.normalized;
        if(Vector3.Dot(agent.desiredVelocity.normalized,transform.forward) <= -.9){input.x = 0;}
        input.x = Mathf.Clamp(delta.x,-1,1);
        input.y = agent.desiredVelocity.magnitude;
        input.y = Mathf.Clamp(input.y,-1,1);
        if(agent.steeringTarget.magnitude <= agent.stoppingDistance)
            input.y = 0;
        car.InputDir = input;
        Debug.Log(car.InputDir + " : " + input + " d: " + delta + " m: " + Vector3.Dot(agent.desiredVelocity.normalized,transform.forward));
        Debug.DrawLine(transform.position,transform.position + agent.desiredVelocity,Color.red,.1f);
        //Debug.DrawLine(transform.position,delta + transform.position,Color.blue,.1f);
        //Debug.DrawLine(transform.position,delta,Color.blue,.1f);
        
    }
}
