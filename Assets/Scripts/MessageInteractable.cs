using UnityEngine;

public class MessageInteractable : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Init(); //call my init...
  
    }
    public override void Init()
    {
   
        base.Init();    //...which calls the base init

        //and can also do anything specific to this type of interactable

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
