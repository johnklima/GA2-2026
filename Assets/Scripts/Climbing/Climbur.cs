using UnityEngine;

public class Climbur : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float turnSpeed = 100f;

    public bool elevate = false;
    public float elevation = 0f;

    public float TU;
    public Vector3 startPosU;
    public Vector3 endPosU;
    public float TF;
    public Vector3 startPosF;
    public Vector3 endPosF;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TF = 1;
        TU = 1;
    }

    // Update is called once per frame
    void Update()
    {


        
        
        if (TU < 1)
        {
            TU += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosU, endPosU, TU);
            return;

        }
        if (TU > 1) 
        {
            TU = 1;    //lock down
        }

        if(TU >= 1 && TF < 1)
        {
            TF += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosF, endPosF, TF);
            return;
        }
        if (TF > 1)
        {
            TF = 1;    //lock down
        }


        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        if (elevate)
        {
            elevation += move * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Turn using Euler angles
            transform.eulerAngles += new Vector3(0, turn * turnSpeed * Time.deltaTime, 0);

            // Move forward
            transform.position += transform.forward * move * moveSpeed * Time.deltaTime;


        }
           

        //get the situation report            
        RaycastHit hit;

        Vector3 dir = -transform.up;
        Vector3 pos = transform.position;

        float distanceDown = 100;
        float distanceForward = 100;

        int layerMask = 1 << 8; //ground

        //do down 
        if (Physics.Raycast(pos, dir, out hit, 100, layerMask))
        {
            distanceDown = hit.distance;
            pos.y = hit.point.y + hit.normal.y + elevation;

            if (distanceDown > 1.0f)
            {
                transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * moveSpeed);
            }
            else
            {
                transform.position = pos;
            }
           
        }
        //do forward
        dir = transform.forward;
        if (Physics.Raycast(pos, dir, out hit, 100, layerMask))
        {
            distanceForward = hit.distance;
            //who is closer
            if(distanceForward < 0.75f )
            {
                //pos.y = hit.point.y + hit.normal.y;
                //transform.position = pos;

                Debug.Log("at a wall");
                elevate = true;
                

            }
            else if (elevate)  //if I was elevating and too large
            { 
                elevate = false; 
                elevation = 0;
                TU = 0;
                TF = 0;
                startPosU = transform.position;
                endPosU = transform.position + transform.up ;
                startPosF = endPosU;
                endPosF = endPosU + transform.forward;
              
            }
        }
        else if (elevate) //if I was elevating, and nothing in fron
        {
            elevate = false;
            elevation = 0;
            TU = 0;
            TF = 0; 
            startPosU = transform.position;            
            endPosU = transform.position + transform.up;
            startPosF = endPosU ;
            endPosF = endPosU + transform.forward;

        }
    }
}
