using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class FootSounds : MonoBehaviour
{
    public AudioSource foots1;
    public AudioSource foots2;
    NavMeshAgent agent;
    
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
         
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 11; //GroundSound

        RaycastHit hit;

        int groundType = 0;

        if (Physics.Raycast(transform.position + Vector3.up * 2 , Vector3.down, out hit, 1000, layerMask))
        {
            groundType= hit.transform.GetComponent<GroundType>().groundType;
            Debug.Log("ground type " + groundType);
        }

        AudioSource toUse = null;
        if (groundType == 1) { toUse = foots1; }
        
        
        if (groundType == 2) { toUse = foots2; }


        float footSpeed = agent.velocity.magnitude;

        if (footSpeed > 0.1f)
        {
            if (!toUse.isPlaying)
            {
                toUse.Play();
            }

        }
        else
        {
            toUse.Stop();
        }
    }
}
