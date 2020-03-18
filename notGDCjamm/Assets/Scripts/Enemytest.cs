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
        Debug.DrawLine(transform.position,transform.position + agent.desiredVelocity,Color.red,.1f);
        Vector2 input = Vector2.zero;
        Vector3 delta = agent.desiredVelocity.normalized - transform.forward;
        delta = delta.normalized;
        input.x = delta.x * -1;
        input.y = agent.desiredVelocity.magnitude;
        input.y = Mathf.Min(input.y,1);
        car.InputDir = input;
        Debug.Log(car.InputDir + " : " + input + " d: " + delta);
        
    }
}
