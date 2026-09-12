using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Unity;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelect : AttributesSync
{
    public Text debug;
    public Spawner spawner = null;
    
    private UniqueAvatarChild avatarChild;

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


        //get the avatar child so we can change it
        avatarChild = GetComponent<UniqueAvatarChild>();

        //get the spawner from the Multiplayer instance, EZ by tag
        spawner = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Spawner>();

        //say hello
        int c = Multiplayer.GetUsers().Count;
        debug.text += ("I am Player: " + c) + "\n";

        
        //spawn a new NavMesh target
        Transform targ = spawner.Spawn(0).gameObject.transform;
        
        //assign it to this new player 
        GetComponent<AvatarNavMeshDriver>().MainTarget = targ; 
        
        //and to the camera controller.
        //WARNING: better be at zero!
        transform.GetChild(0).GetComponent<ComplexOrbitCamera>().moveTarget = targ;
    }

    // Update is called once per frame
    private void Update()
    {

        //TEST CHARACTER SWAP
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //get the avatar and swap the player character for who is at formation point 0
            if(transform.childCount >= 2)
            {
                //drill down...
                Transform character = transform.GetChild(1); //BAD - but that's where it is
                debug.text += character.name + "\n";
                Transform formation = character.GetChild(0); //BAD - but that's where it is
                debug.text += formation.name + "\n";
                Transform hireling = formation.GetChild(0); //NOT BAD - it's an array
                debug.text += hireling.name + "\n";
                Transform occupier = hireling.GetComponent<FormationPoint>().occupier;
                debug.text += occupier.name + "\n";
                NPCState npc = occupier.GetComponent<NPCState>();
                debug.text += "Avatar Child Index " + npc.AvatarChildIndex + "\n";

                //penultimate swap avatar child
                ChangeMe(npc.AvatarChildIndex);

                //ultimate swap formation occupier to previous avatar child, as NPC

            }
        }
    }
    public void ChangeMe(int which) //which is the pos in the array of UniqueAvatarChild
    {
        ChangeCharacter(which);

        BroadcastRemoteMethod("ChangeCharacter", which);
    }
    [SynchronizableMethod]  
    void ChangeCharacter(int which)
    {

        debug.text += "Hello ChangeCharacter \n";
        {
            avatarChild.OverwritePrefab(avatarChild.Prefabs[which]);
        }


    }
}
