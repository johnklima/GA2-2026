using UnityEngine;

//ABstract for all interactables, you can NOT plop this on an object, you need a concrete
public abstract class Interactable : MonoBehaviour
{
    public string text;
    public GameObject popup;

    public virtual void Hit()
    {
        Debug.Log("base Hit");
        popup.SetActive(true);
    }

    public virtual void UnHit()
    {

        Debug.Log("base UnHit");
        popup.SetActive(false);

    }

}
