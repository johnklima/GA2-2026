using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Animator animator;

    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") && Key.hasKey && !isOpen)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
        animator.SetTrigger("Open");
    }
}