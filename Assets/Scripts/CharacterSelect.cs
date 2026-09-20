using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Unity;
using UnityEngine;
using UnityEngine.UI;




public class CharacterSelect : AttributesSync
{
    public Text debug;
    public Spawner spawner = null;
    
    public UniqueAvatarChild avatarChild;
    public AvatarSynch synch;

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
        synch = GetComponent<AvatarSynch>();

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
        ComplexOrbitCamera  cam = transform.GetChild(0).GetComponent<ComplexOrbitCamera>();
        cam.moveTarget = targ;

        //set its initial pos
        Vector3 pos = cam.pointCam.transform.localPosition;
        pos.z = 10.0f;
        cam.pointCam.transform.localPosition = pos;

        //hide existing fogs spawned by others players
        var fogs = FindObjectsByType<FogSimulation>(FindObjectsSortMode.None);

        for (int i = 0; i < fogs.Length; i++)
        {
            fogs[i].transform.gameObject.SetActive(false);
        }


        //spawn fog        
        Transform fogroot = spawner.Spawn(1).gameObject.transform;
        Transform foggy = fogroot.GetChild(0);
        foggy.GetComponent<FogSimulation>().player = transform;

        


    }

    bool doHides = true;
    // Update is called once per frame
    private void Update()
    {

        if (doHides) 
        { 
            doHides = false;
            //hide existing fogs spawned by others players
            var fogs = FindObjectsByType<FogSimulation>(FindObjectsSortMode.None);

            for (int i = 0; i < fogs.Length; i++)
            {
                if (fogs[i].player != transform)
                    fogs[i].transform.parent.gameObject.SetActive(false);
            }
        }

        //alpha1 is ascii 49, I have 7 characters
        for (int i = 0;i < 7;i++)
        {
            if (Input.GetKeyDown((KeyCode)(49 + i)))
            {
                testSwap = true;
            }

            if (testSwap)
            {
                testSwap = false;
                ChangeMe(i);

                break;

            }

        }

        //CHARACTER SWAP by Team
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

                                //smash the new NPC into the player, works in older version?
                                newNpc.position = transform.position;

                                //the non-collide approach:
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
        //ChangeCharacter(which);
        
        BroadcastRemoteMethod("ChangeCharacter", which);
        return avatarChild.GetAvatarChild();
    }
    
    [SynchronizableMethod]  
    void ChangeCharacter(int which)
    {

        debug.text += "Hello ChangeCharacter \n";
        {
            //avatarChild.OverwritePrefab(avatarChild.Prefabs[which]);
            synch.SetAvatarPrefab(which);
            
        }

        
    }

   
}
