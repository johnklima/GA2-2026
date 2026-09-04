using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AvatarNavMeshDriver : MonoBehaviour
{
    //the agent
    NavMeshAgent agent;
    //the list of path targets
    public Transform[] PathTarget;
    //index in list of targets
    private int pathIndex = 0;

    public Transform MainTarget;

    //zero state is path following
    public int State = 0;


    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the agent from Alteruna Avatar
        agent = transform.GetComponent<NavMeshAgent>();

        //tell it where to go first
        if (PathTarget.Length > 0)
            agent.SetDestination(PathTarget[pathIndex].position);

    }

    // Update is called once per frame
    void Update()
    {
        if (State == 0)     //pathing
        {
            RandomPath();
        }
        if (State == 1)     //simple mouse
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetDestinationToMousePosition();
            }
        }
        if (State == 2)      //player
        {
            SetDestinationToMainTarget();
        }
    }
    void SetDestinationToMainTarget()
    {
        if(MainTarget)
            agent.SetDestination(MainTarget.position);
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

        if (PathTarget.Length == 0)
            return;



        //continuosly update the position, as the target might change or be moved
        if (PathTarget[pathIndex])
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
