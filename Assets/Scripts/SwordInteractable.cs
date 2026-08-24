using UnityEngine;
using UnityEngine.UI;
public class SwordInteractable : Interactable
{
    public GameObject lore;
    Animator animator;
    bool playPull = false;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //can only find if active, there is another way I can handle this
        //but it is a big anoyance
        lore = GameObject.FindGameObjectWithTag("Lore");
        lore.SetActive(false);

        popup = GameObject.FindGameObjectWithTag("Popup");
        popup.SetActive(false);

        animator = GetComponent<Animator>();    

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("set anim param");
            isInteracting = true;
            playPull = true;


        }
        if (isInteracting)
        {
            Debug.Log("I am interacting");
            if (isInteracting && playPull)
            {
                playPull = false;  
                animator.SetTrigger("PullSword");
                Debug.Log("play the fuckng animation");

            }
        }

        
        
    }

    public override void Hit()
    {
        base.Hit();
        Debug.Log("Sword is getting Hit");
        lore.SetActive(true);
        Text textobj = lore.transform.GetChild(0).GetComponent<Text>();
        textobj.text = text;
    }
    public override void UnHit()
    {
        base.UnHit();
        Debug.Log("Sword is getting UnHit");
        lore.SetActive(false);
    }
}
