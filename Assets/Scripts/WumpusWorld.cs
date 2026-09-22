using UnityEngine;

public class WumpusWorld : MonoBehaviour
{

    //I should do a typedef enum but rather I shall do it in the simplest of terms
    public const int BAD_SUSHI = -1;
    public const int EMPTY = 0;
    public const int BREEZE = 1;
    public const int STENCH = 2;
    public const int TREASURE = 3;
    public const int PIT = 4;
    public const int WUMPUS = 5;
    public const int AGENT = 6;

    public GameObject baseobject; //thing we instance

    //allocation bounds
    public int ROWS = 4;
    public int COLS = 4;

    private GameObject[,] objs;

    private void Awake()
    {
        objs = new GameObject[ROWS, COLS];
        Build();
        StartGame();
    }

    //hardcode the initial "classic" wumpus world
    private void Build()
    {
        for (int row = 0; row < ROWS; row++)
            for (int col = 0; col < COLS; col++)
            {
                objs[row, col] = Instantiate(baseobject, transform);
                var space = baseobject.transform.localScale.x;
                var pos = new Vector3(row * space, 0.5f, col * space);

                objs[row, col].transform.localPosition = pos;

                // get the data object (script component) of the game object 
                var data = objs[row, col].GetComponent<WumpusData>();
                data.row = row;
                data.col = col;

                //set up this cell
                ApplyInitialState(objs[row, col]);
            }
    }

    void ApplyInitialState(GameObject thisobj)
    {
        thisobj.SetActive(true);

        // get the data object (script component) of the game object 
        var data = thisobj.GetComponent<WumpusData>();


        //apply hard rules as to where things are, based on the classic demo

        if (data.row == 0 && data.col == 0)
        {
            //player starts at 0,0
            data.cellContents = EMPTY;
            return;
        }

        if (data.row == 0 && data.col == 1)
        {
            data.cellContents = BREEZE;
            return;
        }

        if (data.row == 0 && data.col == 2)
        {
            data.cellContents = PIT;
            return;
        }

        if (data.row == 0 && data.col == 3)
        {
            data.cellContents = BREEZE;
            return;
        }

        if (data.row == 1 && data.col == 0)
        {
            data.cellContents = STENCH;
            return;
        }

        if (data.row == 1 && data.col == 1)
        {
            data.cellContents = EMPTY;
            return;
        }

        if (data.row == 1 && data.col == 2)
        {
            data.cellContents = BREEZE;
            return;
        }

        if (data.row == 0 && data.col == 3)
        {
            data.cellContents = EMPTY;
            return;
        }

        if (data.row == 2 && data.col == 0)
        {
            data.cellContents = WUMPUS;
            return;
        }

        if (data.row == 2 && data.col == 1)
        {
            data.cellContents = TREASURE;
            return;
        }

        if (data.row == 2 && data.col == 2)
        {
            data.cellContents = PIT;
            return;
        }

        if (data.row == 2 && data.col == 3)
        {
            data.cellContents = BREEZE;
            return;
        }

        if (data.row == 3 && data.col == 0)
        {
            data.cellContents = STENCH;
            return;
        }

        if (data.row == 3 && data.col == 1)
        {
            data.cellContents = EMPTY;
            return;
        }

        if (data.row == 3 && data.col == 2)
        {
            data.cellContents = BREEZE;
            return;
        }

        if (data.row == 3 && data.col == 3) data.cellContents = PIT;



    }

    private void StartGame()
    {
        //Activate player cell
        objs[0, 0].GetComponent<WumpusData>().Expose();


        //if we want to debug mats
        if (true)
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    objs[row, col].GetComponent<WumpusData>().Expose();

                }
            }

        }


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
