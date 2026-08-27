using UnityEngine;
using UnityEngine.Rendering;

public class MessageInteractable : Interactable
{
    
    
    public bool isInRange = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Init(); //call my init...
  
    }
    public override void Init()
    {
   
        base.Init();    //...which calls the base init

        //and can also do anything specific to this type of interactable

    }
    public override void Hit()
    {
        if (isInRange) 
        {
            base.Hit();
        }
        
    }


    public bool isOwned = false;
    public bool isViewing = false;
    Vector3 startPos = Vector3.zero;
    Vector3 startScale = Vector3.one;
    float T = 0;
    // Update is called once per frame
    void Update()
    {
        if (isInRange && !isViewing)
        {
            Debug.Log("is in range of bottle");
            if (Input.GetKeyDown(KeyCode.F) && isHovering)
            {
                Debug.Log("set anim param");
                isInteracting = true;

                FreeCamera camctrl = Camera.main.GetComponent<FreeCamera>();
                camctrl.lookCamera = true;
                

            }

            //final position and scale of the bottle
            Vector3 campos = Camera.main.transform.position;
            Vector3 fwd = Camera.main.transform.forward;
            Vector3 newpos = campos + fwd;
            Vector3 newscale = Vector3.one * 0.5f;

            

            if (isInteracting && !isOwned)
            {
                //i used this to snap
                isOwned = true; 
                startPos = transform.position;
                startScale = transform.localScale;           
            }

            if (isOwned)
            {
                //now lets interpolate method 1
                //transform.position = Vector3.Lerp(transform.position, newpos, Time.deltaTime);
                //transform.localScale = Vector3.Lerp(transform.localScale, newscale, Time.deltaTime);

                //method 2
                T += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, newpos, T);
                transform.localScale = Vector3.Lerp(startScale, newscale, T);

                if (T >= 1.0f)
                {
                    isOwned = false;
                    isViewing = true;
                }
            }

            if (isViewing) 
            {
                Debug.Log("Bottle has arrived");
                if (Input.GetKeyDown(KeyCode.G) )
                {
                    Debug.Log("HELLO F");
                    isInteracting = false;
                    isHovering = false;

                    FreeCamera camctrl = Camera.main.GetComponent<FreeCamera>();
                    Inventory inv = Camera.main.GetComponent<Inventory>();
                    inv.AddToInventory(transform);
                    transform.position = Vector3.down * 666;
                    transform.gameObject.SetActive(false);
                    popup.SetActive(false);

                    camctrl.lookCamera = false;  //release the camera

                    isViewing = false;
                }


            }
        }
    }
}
