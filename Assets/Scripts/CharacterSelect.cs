using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Unity;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelect : CommunicationBridge
{
    public Text debug;
    public Spawner spawner = null;
    
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
        //get the spawner from the Multiplayer instance, EZ by tag
        spawner = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Spawner>();

        //say hello
        int c = Multiplayer.GetUsers().Count;
        debug.text += ("players: " + c) + "\n";

        //spawn a new NavMesh target
        Transform targ = spawner.Spawn(0).gameObject.transform;
        
        //assign it to this new player 
        GetComponent<AvatarNavMeshDriver>().MainTarget = targ; 
        
        //and to the camera controller.
        //WARNING: better be at zero!
        transform.GetChild(0).GetComponent<ComplexOrbitCamera>().moveTarget = targ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
