using UnityEngine;

public class Target : MonoBehaviour
{

    public float waitTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log(transform.name + " has met " + other.name + " wait time = " + waitTime);
            NavMeshDriver driver = other.transform.GetComponent<NavMeshDriver>();
            driver.ChangeTarget(waitTime);
        }

    }
}
