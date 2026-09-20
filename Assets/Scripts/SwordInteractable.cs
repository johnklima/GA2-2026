using UnityEngine;
using UnityEngine.UI;
using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Unity;

//this is a concrete interactable
[RequireComponent(typeof(AnimationSynchronizable))]
public class SwordInteractable : Interactable
{
    public GameObject lore;
    Animator animator;
    public bool playPull = false;

    public string[] story;

    private AnimationSynchronizable _aniSync;
    private int pullID = Animator.StringToHash("PullSword");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        Init();

        _aniSync = GetComponent<AnimationSynchronizable>();
    }

    public override void Init()
    {
        base.Init();

        //can only find if active, there is another way I can handle this
        //but it is a big anoyance
        if(Manipulator.lore)
        {
            lore = Manipulator.lore;
            lore.SetActive(false);
        }


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
    //all swords support a pull animation, and expose "PullSword" property
    //but all can have a different controller/animation
    public void pullSword()
    {
        if (isInteracting && isHovering)
        {
            Debug.Log("I am interacting");
            if (playPull)
            {
                playPull = false;
                //animator.SetTrigger("PullSword");  //as long as the controller has this property
                Debug.Log("play the animation");   //the animation can be anything

               _aniSync.Play(pullID);        //magically works
            }
        }

    }


    public override void Hit()
    {
        
        
        base.Hit();


        Debug.Log("Sword is getting Hit " + transform.name);

        if (lore)
        {
            lore.SetActive(true);
            Text textobj = lore.transform.GetChild(0).GetComponent<Text>();
            textobj.text = "";
            for (int i = 0; i < story.Length; i++)
            {
                textobj.text += "\n" + story[i];
            }
        }

    }
    public override void UnHit()
    {
        base.UnHit();
        Debug.Log("Sword is getting UnHit");
        
        if(lore)
            lore.SetActive(false);
    }
}
