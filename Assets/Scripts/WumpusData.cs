using UnityEngine;

public class WumpusData : MonoBehaviour
{

    public int row;
    public int col;
    public int cellContents;

    public bool exposed = false;

    [SerializeField] Material[] mats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Expose()
    {

        //flip flag should I care
        exposed = true;

        //set its contents
        SetVisualContents();

    }

    private void SetVisualContents()
    {

        //Based on the index of the contents in data, we choose a material
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = mats[cellContents];
        Debug.Log("cell contents " + cellContents);
    }



}
