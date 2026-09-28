
using UnityEngine;
using UnityEngine.AI;


public class AvatarAnimationDriver : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;
    public AnimSynch sync;

  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        sync = GetComponent<AnimSynch>();

    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Velocity", agent.velocity.magnitude);
        sync.SetAvatarSpeed(agent.velocity.magnitude);

    }
}
