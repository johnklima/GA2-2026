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
        things[curThing] = thing;
        curThing++;
        if (curThing == things.Length - 1 )
            curThing = 0;

    }
}
