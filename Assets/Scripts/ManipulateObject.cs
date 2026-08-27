using UnityEngine;

public class ManipulateObject : MonoBehaviour
{

    public Interactable currentInteract;
    public GameObject lore;
    public GameObject popup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        int layer = 1 << 9; //interactable

        // Create a ray from the center of the viewport(0.5, 0.5)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        //TODO: For best performance and clarity, add a "don't raycast unless in
        //      bounding volume" (this might solve other quirks)

        // Perform the raycast
        if (Physics.Raycast(ray, out RaycastHit hit, 100, layer))
        {
            // Handle hit object
            Debug.Log("hit " + hit.transform.name);

            //only one at a time thanks!
            if(currentInteract)
                currentInteract.UnHit();

            currentInteract = hit.transform.GetComponent<Interactable>();
            if (currentInteract != null)
            {               
                currentInteract.Hit();
            }
            else 
            {
                //not sure why it can be null, but just log it
                Debug.LogWarning("interact is null??? " + hit.transform.name);
                return;
            }
            
            
        }
        else if (currentInteract)
        {
            currentInteract.UnHit();
            currentInteract = null;
        }
    }
}
