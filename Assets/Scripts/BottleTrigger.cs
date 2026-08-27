using UnityEngine;

public class BottleTrigger : MonoBehaviour
{
    public Transform theBottle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            Debug.Log("In range of bottle");
            MessageInteractable interactable = theBottle.GetComponent<MessageInteractable>();
            interactable.isInRange = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            Debug.Log("Out of range of bottle");
            MessageInteractable interactable = theBottle.GetComponent<MessageInteractable>();
            interactable.isInRange = false;

            interactable.popup.gameObject.SetActive(false);

        }
    }
}
