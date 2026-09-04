using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Unity;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteract : CommunicationBridge
{

    public string dialog;
    public Text display;
    public GameObject DialogPanel;
    public CurrentDialog DialogTree;
    public Transform Character;
    public Transform Branch;

    //will happen after Awake but before Start
    //called when player enters room
    public override void Possessed(bool isMe, User user)
    {
        // disables this script for remote players
        enabled = isMe;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(dialog);
            display.text = dialog;

            DialogTree.CurrentBranch = Branch;

            //pick a free formation point to follow
            Transform formation = other.transform.GetChild(0);
            Transform usethis = other.transform.parent;  //alteruna avatar

            foreach (Transform F in formation)
            {
                if (F.GetComponent<FormationPoint>().occupier == null)
                {
                    F.GetComponent<FormationPoint>().occupier = Character;
                    usethis = F;
                    break;
                }

            }

            Character.GetComponent<NavMeshDriver>().MainTarget = usethis;
            Character.GetComponent<NavMeshDriver>().State = 2;


        }
    }
}
