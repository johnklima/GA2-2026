using UnityEngine;
using UnityEngine.UI;

//this is a concrete interactable
public class SwordInteractable : Interactable
{
    public GameObject lore;
    Animator animator;
    public bool playPull = false;

    public string[] story;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        Init();
    }

    public override void Init()
    {
        base.Init();

        //can only find if active, there is another way I can handle this
        //but it is a big anoyance
        lore = Manipulator.lore;
        lore.SetActive(false);

        animator = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isHovering)
        {
            Debug.Log("set anim param");
            isInteracting = true;
            playPull = true;

            pullSword();
        }

       

    }
    public void pullSword()
    {
        if (isInteracting && isHovering)
        {
            Debug.Log("I am interacting");
            if (playPull)
            {
                playPull = false;
                animator.SetTrigger("PullSword");
                Debug.Log("play the animation");

            }
        }

    }


    public override void Hit()
    {
        
        
        base.Hit();


        Debug.Log("Sword is getting Hit");
        lore.SetActive(true);
        Text textobj = lore.transform.GetChild(0).GetComponent<Text>();
        textobj.text = "";
        for (int i = 0; i < story.Length; i++)
        {
            textobj.text += "\n" + story[i];
        }       
        
    }
    public override void UnHit()
    {
        base.UnHit();
        Debug.Log("Sword is getting UnHit");
        lore.SetActive(false);
    }
}
