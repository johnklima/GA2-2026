using UnityEngine.UI;
using Alteruna.Multiplayer.Unity;
using Alteruna.Multiplayer.Core;
public class CharacterSelect : CommunicationBridge
{
    public Text debug;
    
    //will happen after Awake but before Start
    //called when player enters room
    public override void Possessed(bool isMe, User user)
    {
        // disables this script for remote players
        enabled = isMe;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        int c = Multiplayer.GetUsers().Count;
        debug.text += ("players: " + c) + "\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
