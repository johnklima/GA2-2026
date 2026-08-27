using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CameraCollider")  //in this demo it is the box child of the camera
        {
            Debug.Log("pickup hit " + other.name);
            //get the camera parent of the box and its inventory
            Inventory inv = other.transform.parent.GetComponent<Inventory>();
            //add the pickup
            inv.AddToInventory(transform);
            //hide it
            transform.position = Vector3.up * -666.0f;
        }
    }
}
