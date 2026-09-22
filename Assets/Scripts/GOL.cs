using UnityEngine;

public class GOL : MonoBehaviour
{

    [SerializeField] GameObject baseobject;
    [SerializeField] float interval = 0.5f;  //time interval
    [SerializeField] int spark = 99;

    const int MAX_ROWS = 32;        //size of the grid
    const int MAX_COLUMNS = 32;

    //2d array of gameobjects to show
    GameObject[,] objs;
    //2d array of ints for cell values
    int[,] cells;


    float timer = -1; //the usual timer to see the generations

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = Time.realtimeSinceStartup;  //this gives a totally unique value

        Random.InitState((int)(timer * 100.0f)); //casts a float to int as a unique seed
        
        //redimension our array
        cells = new int[MAX_ROWS, MAX_COLUMNS];
        objs = new GameObject[MAX_ROWS, MAX_COLUMNS];

        baseobject.SetActive(false);

        for (int row = 0; row < MAX_ROWS; row++)
        {
            for (int col = 0; col < MAX_COLUMNS; col++)
            {
                //create cells
                objs[row, col] = GameObject.Instantiate(baseobject, transform);
                Vector3 pos = new Vector3(row, 10, col);

                objs[row, col].transform.position = pos;

                objs[row, col].SetActive(true);

                //find the ground
                int layerMask = 1 << 8; //ground
                RaycastHit hit;

                // Does the ray intersect any surface in the layer mask
                if (Physics.Raycast(pos, Vector3.down, out hit, 100, layerMask))
                {
                    float x = objs[row, col].transform.position.x;
                    float y = hit.point.y;
                    float z = objs[row, col].transform.position.z;

                    //apply this position to hug a surface
                    objs[row, col].transform.position = new Vector3(x, y, z);

                    //apply this rotation to hug a surface
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    objs[row, col].transform.rotation = rotation;

                    // or this to preserve original forward direction for steering
                    objs[row, col].transform.rotation = Quaternion.LookRotation 
                        (objs[row, col].transform.forward, hit.normal);

                    //or this one, hrm what's the difference?
                    //rotate object so its 'up' aligns with the hit normal
                    transform.rotation = Quaternion.FromToRotation
                        (objs[row, col].transform.up, hit.normal) * objs[row, col].transform.rotation;
                }

                //init cells
                int state = Random.Range(0, 3);   // here I can init more cells live than dead. 
                if (state >= 1)
                {
                    objs[row, col].SetActive(true);
                    cells[row, col] = 1;
                }
                else
                {
                    objs[row, col].SetActive(false);
                    cells[row, col] = 0;
                }



            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - timer > interval)
        {
            generateGOL();
            timer = Time.realtimeSinceStartup;
        }

    }
    void generateGOL() 
    {
        int[,] next = new int[MAX_ROWS, MAX_COLUMNS]; // the next frame

        // Loop through every spot in our 2D array and check spots neighbors
        for (int row = 0; row < MAX_ROWS; row++)
        {
            for (int col = 0; col < MAX_COLUMNS; col++)
            {
                //check my neigbors, so up , down, left, right, and diagonal
                //this is what we need to do, but need to wrap around the array
                int rowminus = row - 1;
                int rowplus = row + 1;
                int colminus = col - 1;
                int colplus = col + 1;

                if (rowminus < 0) rowminus = MAX_ROWS - 1;
                if (rowplus == MAX_ROWS) rowplus = 0;

                if (colminus < 0) colminus = MAX_COLUMNS - 1;
                if (colplus == MAX_COLUMNS) colplus = 0;

                // Add up all the states in a 3x3 surrounding grid, not including where i am now
                int neighbors = 0;

                neighbors += cells[rowminus, col];
                neighbors += cells[rowplus, col];
                neighbors += cells[row, colminus];
                neighbors += cells[row, colplus];
                neighbors += cells[rowplus, colplus];
                neighbors += cells[rowplus, colminus];
                neighbors += cells[rowminus, colplus];
                neighbors += cells[rowminus, colminus];

                // Rules of Life
                if ((cells[row, col] == 1) && (neighbors < 2))				// Loneliness 
                    next[row, col] = 0;
                else if ((cells[row, col] == 1) && (neighbors > 3))         // Overpopulation
                    next[row, col] = 0;
                else if ((cells[row, col] == 0) && (neighbors == 3))        // Reproduction 
                    next[row, col] = 1;
                else                                                        // Stasis         
                    next[row, col] = cells[row, col];

                //and why not?
                int r = Random.Range(0, 100);

                if(r > spark)
                    cells[row, col] = 1;

            }           

        }

        //now swap new values for old
        for (int row = 0; row < MAX_ROWS; row++)
        {
            for (int col = 0; col < MAX_COLUMNS; col++)
            {

                cells[row, col] = next[row, col];

                if (cells[row, col] == 1)
                    objs[row, col].SetActive(true);
                else
                    objs[row, col].SetActive(false);

            }
        }

    }
}
