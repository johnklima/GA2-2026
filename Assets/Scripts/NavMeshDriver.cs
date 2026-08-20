using UnityEngine;
using UnityEngine.AI;


public class NavMeshDriver : MonoBehaviour
{
    NavMeshAgent agent;
    
    public Transform[] PathTarget;

    public int pathPoint = 0;
    
    //Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(PathTarget[pathPoint].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1.0f)
        {
            pathPoint++;            
        }

        if (pathPoint >= PathTarget.Length)
        {
            pathPoint = 0;
        }
                    

        agent.SetDestination(PathTarget[pathPoint].position);
    }
}