using UnityEngine;
using UnityEngine.AI;

public class CharactorAnimationDriver : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Velocity", agent.velocity.magnitude);
        
      
    }
}
