using UnityEngine;
using System.Collections;
public class DoorCollision : MonoBehaviour
{
    public Transform key;
    public Transform door;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "MainCamera") 
        {
            Debug.Log("cam hit door");
            Inventory inv = collision.transform.GetComponent<Inventory>();
            bool haskey = false;
            for (int i = 0; i < inv.things.Length; i++)
            {
                if (inv.things[i] == key)
                    haskey = true;
            }

            if (haskey)
            {
                //play the door amination
                door.GetComponent<Animation>().Play();

                StartCoroutine(waitDoor());
                
            }

        }
    }
    IEnumerator waitDoor()
    {
        yield return new WaitForSeconds(1);
        transform.position = Vector3.down * 666;

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "MainCamera")
        {
            Debug.Log("cam exit door");
            collision.transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }
}
