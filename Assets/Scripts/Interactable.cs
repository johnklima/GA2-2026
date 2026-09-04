using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Unity;
using UnityEngine;
using UnityEngine.UI;
//ABstract for all interactables, you can NOT plop this on an object, you need a concrete
public abstract class Interactable : CommunicationBridge
{
    public string popMsg;
    public GameObject popup;
    public bool isInteracting = false;
    public ManipulateObject Manipulator;
    public bool isHovering = false;

    private void Start()
    {
        Init();
    }

    //will happen after Awake but before Start
    //called when player enters room
    public override void Possessed(bool isMe, User user)
    {
        // disables this script for remote players
        enabled = isMe;
    }

    //overideable replacement for start
    public virtual void Init()  
    {
 
        popup = Manipulator.popup;
        popup.SetActive(false);     
    }


    public virtual void Hit()
    {
        Debug.Log("base Hit");
        popup.transform.GetChild(0).GetComponent<Text>().text = popMsg;
        popup.SetActive(true);
        isHovering = true;
    }

    public virtual void UnHit()
    {

        Debug.Log("base UnHit");
        popup.SetActive(false);
        isHovering = false;

    }

}
