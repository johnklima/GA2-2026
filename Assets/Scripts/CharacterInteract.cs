using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Unity;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteract : AttributesSync
{

    public Transform Character;

    //will happen after Awake but before Start
    //called when player enters room
    public override void Possessed(bool isMe, User user)
    {
        // disables this script for remote players
        enabled = isMe;
    }
    private void Start()
    {
        Character = transform.parent;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            Transform otherTransform = other.transform;

            InteractCharacter(otherTransform, false);           
        }
    }

    public void InteractCharacter(Transform newOwner, bool warp)
    {
        //for character theft, need to remove from other owners formation list
        NPCState npcState = Character.GetComponent<NPCState>();

        if (npcState.CurrentFormationPoint != null)
        {
            npcState.CurrentFormationPoint.occupier = null;
            npcState.CurrentFormationPoint.uniquePlayerIndex = -1; //blow an error
            npcState.CurrentFormationPoint = null;
        }

        //pick a free formation point to follow
        Transform formation = newOwner.GetChild(0);  //BAD - find another way

        //default to alteruna avatar if formation out of bounds, failsafe for theft
        Transform usethis = newOwner.parent;

        //find an empty slot in the player's formation list
        foreach (Transform F in formation)
        {
            if (F.GetComponent<FormationPoint>().occupier == null)
            {
                F.GetComponent<FormationPoint>().occupier = Character;
                //TDOD: make this better
                //let the slot know the occupier's avatar child index
                F.GetComponent<FormationPoint>().uniquePlayerIndex = npcState.AvatarChildIndex;
                //let the NPC know it's current slot
                npcState.CurrentFormationPoint = F.GetComponent<FormationPoint>();

                usethis = F;
                break;
            }

        }
        if (usethis)
        {
            Character.GetComponent<NavMeshDriver>().MainTarget = usethis;
            Character.GetComponent<NavMeshDriver>().State = 2;
            if (warp)
            {
                Character.position= usethis.position;
            }
        }

    }
}
