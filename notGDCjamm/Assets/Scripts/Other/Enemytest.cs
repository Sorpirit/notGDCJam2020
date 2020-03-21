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
                agent.SetDestination(hit.point);
            }
        }
        Debug.DrawLine(transform.position,transform.position + agent.desiredVelocity,Color.red,.1f);
        if(car == null)
            return;
        Vector2 input = Vector2.zero;
        Vector3 delta = agent.desiredVelocity.normalized - transform.forward;

        input.x = Mathf.Clamp(Vector3.Dot(new Vector3(transform.forward.x,0,transform.forward.z),agent.desiredVelocity),-1,1);
        input.x = Mathf.Sign(input.x);
        if(input.x == 0){
            if(Vector3.Dot(new Vector3(transform.forward.z,-transform.forward.x),agent.desiredVelocity) > 0)
                input.x = 1;
            else
                input.x = -1;
        }
        input.y = Mathf.Clamp(agent.desiredVelocity.magnitude,0,1);
        // delta = delta.normalized;
        // if(Vector3.Dot(agent.desiredVelocity.normalized,transform.forward) <= -.9){input.x = 0;}
        // input.x = Mathf.Clamp(delta.x,-1,1);
        // input.y = agent.desiredVelocity.magnitude;
        // input.y = Mathf.Clamp(input.y,-1,1);
        // if(agent.steeringTarget.magnitude <= agent.stoppingDistance)
        //     input.y = 0;
        car.InputDir = input;
        Debug.Log(car.InputDir + " : " + input + " d: " + delta + " m: " + Vector3.Dot(agent.desiredVelocity.normalized,transform.forward));
        //Debug.DrawLine(transform.position,delta + transform.position,Color.blue,.1f);
        //Debug.DrawLine(transform.position,delta,Color.blue,.1f);
        
    }
}
