using UnityEngine;
using UnityEngine.UI;

public class CharacterInteract : MonoBehaviour
{

    public string dialog;
    public Text display;
    public GameObject DialogPanel;
    public CurrentDialog DialogTree;
    public Transform Character;
    public Transform Branch;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(dialog);
            display.text = dialog;

            DialogTree.CurrentBranch = Branch;

            //pick a free formation point to follow
            Transform formation = other.transform.GetChild(0);
            Transform usethis = other.transform;

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
