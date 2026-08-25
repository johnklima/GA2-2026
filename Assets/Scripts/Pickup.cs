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
        if(other.tag == "Collider")
        {
            Debug.Log("pickup hit " + other.name);
            Inventory inv = other.transform.parent.GetComponent<Inventory>();
            inv.AddToInventory(transform);
            transform.position = Vector3.up * -666.0f;
        }
    }
}
