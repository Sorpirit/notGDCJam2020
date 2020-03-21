using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        EnemyDestSetter.DestSetter.Chekout(agent);
    }

}