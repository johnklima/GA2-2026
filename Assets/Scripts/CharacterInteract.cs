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
                    
        }
    }
}
