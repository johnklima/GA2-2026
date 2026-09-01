using UnityEngine;


public class CurrentDialog : MonoBehaviour
{

    public Transform CurrentBranch;

    public void ButtonLeft()
    { 
        Debug.Log(CurrentBranch.name);
        CurrentBranch.GetChild(0).gameObject.SetActive(true);


    }
    public void ButtonRight() 
    {
        Debug.Log(CurrentBranch.name);
        CurrentBranch.GetChild(1).gameObject.SetActive(true);

    }
}
