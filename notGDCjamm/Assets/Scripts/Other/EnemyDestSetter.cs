using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyDestSetter : MonoBehaviour {
    
    public static EnemyDestSetter DestSetter;

    [SerializeField] private Transform destanation;

    private List<NavMeshAgent> agents;

    private void Awake()
    {
        if(DestSetter == null){
            DestSetter = this;
        }else if(DestSetter != this){
            Destroy(this);
            return;
        }

        agents = new List<NavMeshAgent>();
    }

    private void Start()
    {
        GlobalTimer.timer.TimerStateChenged += SetAllDest;
    }

    private void OnDisable() {
        GlobalTimer.timer.TimerStateChenged -= SetAllDest;
    }

    public void Chekout(NavMeshAgent agent){
        agents.Add(agent);
    }

    public void SetAllDest(){
        foreach (NavMeshAgent agent in agents)
        {
            agent.SetDestination(destanation.position);
        }
    }

    public void StopAll(){
        foreach (NavMeshAgent agent in agents)
        {
            agent.SetDestination(agent.transform.position);
        }
    }
    

}