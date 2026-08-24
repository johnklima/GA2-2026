using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshDriver : MonoBehaviour
{
    //the agent
    NavMeshAgent agent;
    //the list of path targets
    public Transform[] PathTarget;
    //index in list of targets
    private int pathIndex = 0;

    //zero state is path following
    public int State = 0;

    public float timer = 1;

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the agent
        agent = GetComponent<NavMeshAgent>();

        //tell it where to go first
        agent.SetDestination(PathTarget[pathIndex].position);

    }

    // Update is called once per frame
    void Update()
    {
        if (State == 0)
        {
            RandomPath();  
        }
        if (State == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetDestinationToMousePosition();
            }
        }
    }
    void SetDestinationToMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Pointer hit " + hit.transform.name);
            agent.SetDestination(hit.point);
        }
    }
    void RandomPath()
    {
        //continuosly update the position, as the target might change or be moved
        agent.SetDestination(PathTarget[pathIndex].position);


    }

    public void ChangeTarget(float seconds)
    {
        StartCoroutine(WaitAtTarget(seconds));

    }
    IEnumerator WaitAtTarget(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        //new path
        int curIndex = pathIndex;
        while (curIndex == pathIndex) 
        {
            pathIndex = Random.Range(0, PathTarget.Length);
        }        
        agent.SetDestination(PathTarget[pathIndex].position);
        agent.isStopped = false;
        
    }
    void LoopPath()
    {
        //arrived?
        if (agent.remainingDistance < 1.0f)
        {
            pathIndex++;
        }
        //keep in bounds of array, looping
        if (pathIndex >= PathTarget.Length)
        {
            pathIndex = 0;
        }

        //continuosly update the position, as the target might change or be moved
        agent.SetDestination(PathTarget[pathIndex].position);

    }
}