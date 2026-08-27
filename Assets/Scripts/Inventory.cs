using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform[] things;
    
    int curThing = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToInventory(Transform thing)
    {

        //wrap around if too many
        //could also assume that inv is full and not add new
        if (curThing == things.Length - 1)
            curThing = 0;

        //add to the array
        things[curThing] = thing;
        //next slot
        curThing++;
        
        

    }
}
