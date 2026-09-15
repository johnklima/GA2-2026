using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Unity;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
public class CharacterSelect : AttributesSync
{
    public Text debug;
    public Spawner spawner = null;
    
    private UniqueAvatarChild avatarChild;

    public bool testSwap;

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

        if(Input.GetKeyDown(KeyCode.Alpha1)) 
            testSwap = true;


        if (testSwap)
        {
            testSwap = false;
            ChangeMe(5);

        }

        //CHARACTER SWAP
        if (Input.GetKey(KeyCode.LeftShift))
        {           

            if (Input.GetMouseButtonDown(0))
            {
                int layerMask = 1 << 10; //NPCs

                RaycastHit hit;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000, layerMask))
                {
                    Debug.Log("try swap character " + hit.transform.name);
                    
                    //drill down...
                    Transform character = transform.GetChild(1); //BAD - but that's where the character is
                    debug.text += character.name + "\n";
                    Transform formation = character.GetChild(0); //BAD - but that's where the formation is
                    debug.text += formation.name + "\n";
                    
                    //find it in my list of formation points
                    foreach (Transform point in formation)
                    {
                        if(point.GetComponent<FormationPoint>().occupier != null)
                        {
                            Transform occupier = point.GetComponent<FormationPoint>().occupier;
                            if (occupier == hit.transform)
                            {
                                debug.text += "swaping to " + hit.transform.name + "\n";                                
                                
                                //who's there now
                                NPCState npc = occupier.GetComponent<NPCState>();
                                debug.text += "Avatar Child Index " + npc.AvatarChildIndex + "\n";

                                //Get the list
                                Transform characters = GameObject.FindGameObjectWithTag("Characters").transform;
                                //find him based on who I was
                                int myIndex = character.GetComponent<CharacterData>().UniqueAvatarIndex;
                                Transform newNpc = characters.GetChild(myIndex);
                                newNpc.gameObject.SetActive(true);

                                debug.text += "New NPC " + newNpc.name + "\n";
                                
                                //penultimate swap avatar child
                                int childIndex = hit.transform.GetComponent<NPCState>().AvatarChildIndex;
                                
                                //disble the NPC ghost
                                hit.transform.gameObject.SetActive(false);

                                GameObject newPlayer = ChangeMe(childIndex);
                                debug.text += "New Player " + newPlayer.name + "\n";

                                //smash the new NPC into the player, works in older version.
                                newNpc.position = transform.position;


                                //fire off the NavMeshDriver through it's interactor
                                //CharacterInteract CI = newNpc.GetChild(0).GetComponent<CharacterInteract>();
                                //debug.text += "CI " + CI.Character.name + "\n";
                                //CI.InteractCharacter(newPlayer.transform, true);

                            }
                        }
                        
                    }
                    
                }

            }
        }
    

    
    }


    public GameObject ChangeMe(int which) //which is the pos in the array of UniqueAvatarChild
    {
        ChangeCharacter(which);
        
        BroadcastRemoteMethod("ChangeCharacter", which);
        return avatarChild.GetAvatarChild();
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
