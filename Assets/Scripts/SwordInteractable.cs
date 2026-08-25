using UnityEngine;
using UnityEngine.UI;

//this is a concrete interactable
public class SwordInteractable : Interactable
{

    public GameObject lore;
    public GameObject popup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //can only find if active, there is another way I can handle this
        //but it is a big anoyance
        lore = GameObject.FindGameObjectWithTag("Lore");
        lore.SetActive(false);

        popup = GameObject.FindGameObjectWithTag("Popup");
        popup.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

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
