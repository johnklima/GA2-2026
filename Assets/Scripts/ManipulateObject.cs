using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManipulateObject : MonoBehaviour
{

    LayerMask layer;
    public Interactable currentInteract ;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        layer = LayerMask.NameToLayer("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        int layer = 1 << 9; //interactable

        // Create a ray from the center of the viewport(0.5, 0.5)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Perform the raycast
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, layer))
        {
            // Handle hit object
            Debug.Log("hit " + hit.transform.name);

            currentInteract = hit.transform.GetComponent<Interactable>();
            currentInteract.Hit();            
        }
        else if (currentInteract)
        {
            currentInteract.UnHit();
            currentInteract = null;
        }
    }
}
