using UnityEngine;
using System.Collections;
public class Excalibur : SwordInteractable
{

    public Transform key;
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) )
        {

            if (isHovering) 
            {
                bool hasKey = true; //false;  //skip key for now

                //check for key in camera, easy way to find
                if (false)
                {
                    Inventory inv = Camera.main.GetComponent<Inventory>();
                    for (int i = 0; i < inv.things.Length; i++)
                    {

                        if (inv.things[i] == key)
                        {
                            hasKey = true;
                        }

                    }
                }

                if (hasKey) 
                {
                    Debug.Log("set anim param");
                    isInteracting = true;
                    playPull = true;

                    //inv.AddToInventory(transform);
                    
                  
                }
            }
           


        }

        pullSword();
    }

    public override void UnHit()
    {
        //get over it, not sure why
        base.UnHit();
        if(lore)
            lore.SetActive(false);
        if(popup)
            popup.SetActive(false);

    }
}
