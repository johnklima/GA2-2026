using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Unity;
using UnityEngine;
using UnityEngine.AI;

public class AvatarAnimationDriver : CommunicationBridge
{
    public Animator animator;
    private NavMeshAgent agent;


    //will happen after Awake but before Start
    //called when player enters room
    public override void Possessed(bool isMe, User user)
    {
        // disables this script for remote players
        enabled = isMe;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Velocity", agent.velocity.magnitude);


    }
}
