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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
          recurseChildren(transform);
        }
    }

    void recurseChildren(Transform T)
    {
        foreach (Transform child in T)
        {
            if (child.gameObject.activeSelf)
            {
                Debug.Log(child.name);
            }            
            recurseChildren(child);
        }

    }
}
