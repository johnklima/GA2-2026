using UnityEngine;

public class MessageInteractable : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Init(popMsg);
  
    }
    public override void Init(string msg)
    {
        Debug.Log("override init " + msg);
        base.Init(popMsg);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
