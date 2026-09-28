using UnityEngine;

public class AvatarAnimationDriver : MonoBehaviour
{
    public Animator animator;
    public AvatarNavMeshDriver NMAdriver;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        animator = GetComponent<Animator>();
        NMAdriver = transform.parent.GetComponent<AvatarNavMeshDriver>(); 
       

    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Velocity", NMAdriver.velocityMagnitude);
        

    }
}
