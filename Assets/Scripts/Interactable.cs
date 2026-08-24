using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    
    public string text;    
    public bool isInteracting = false;
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
