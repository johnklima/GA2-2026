using UnityEngine;

public class DialogBranch : MonoBehaviour
{

    public CharacterInteract interact;

    private void OnEnable()
    {
        interact.Character.gameObject.SetActive(true);
    }
}
