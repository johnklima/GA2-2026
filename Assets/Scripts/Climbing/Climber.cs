using UnityEngine;

public class Climber : MonoBehaviour
{
    //the usual mults
    public float moveSpeed = 1;
    public float turnSpeed = 100f;

    //for lerping rotations (no worky atm, keep it compiling)
    float T = 1;
    Quaternion startQ = Quaternion.identity;
    Quaternion endQ = Quaternion.identity;  

    void Update()
    {

        //no worky, but keep it compilable
        if(false)
        {
            //precise interpolate to a rotation
            T += Time.deltaTime * turnSpeed;
            if (T >= 1)
            {
                T = 1.1f;
            }
            if (T < 1)
            {
                transform.rotation = Quaternion.Lerp(startQ, endQ, T);

            }

        }
        
       
        //get user input (WASD or arrows, or gamepad. long live the old input system!)
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        //turn using eulers (global Y up, TODO transform.up, tricky...)
        transform.eulerAngles += new Vector3(0, turn * turnSpeed * Time.deltaTime, 0);

        //move forward
        transform.position += transform.forward * move * moveSpeed * Time.deltaTime;

        //we need domain knowledge ;)
        //lemme know the distances from down to a poly, and forward to a poly
        float distanceDown = 100;
        float distanceForward = 100;

        //get the situation report            
        RaycastHit hit;

        Vector3 dir = -transform.up;        //down from me
        Vector3 pos = transform.position;   //where i'm at
        
        //do down first, i think     
        if (Physics.Raycast(pos, dir, out hit, 100))
        {
            distanceDown = hit.distance;

            pos.y = hit.point.y + hit.normal.y;
            
            //slam me to the point, up one from its normal
            transform.position = pos;

            //ignore for now, interpolation no worky
            startQ = transform.rotation;

            //rotate the bean so its 'up' aligns with the hit normal
            Quaternion newRot = Quaternion.FromToRotation(transform.up, hit.normal)
                                                         * transform.rotation;

            //slam it, it's just what works atm
            transform.rotation = newRot;

            //HOLY GRAIL: interpolation
            if (T >= 1 && false)  //keep this compilable but don't use it, no worky
            {
                T = 0;
                endQ = newRot;
            }

        }
       
        //do forward
        dir = transform.forward;

        if (Physics.Raycast(pos, dir, out hit, 100.0f))
        {
            distanceForward = hit.distance;

            //who is really close, y down should not be much less than 1 ever, so...
            if (distanceForward < 0.5f)
            {
                //slam the red bean
                pos.y = hit.point.y + hit.normal.y;
                transform.position = pos;

                //rotate the bean so its 'up' aligns with the hit normal
                Quaternion newRot = Quaternion.FromToRotation( transform.up, hit.normal )                 
                                                             * transform.rotation;
                //slam it
                transform.rotation = newRot;

                //HOLY GRAIL: interpolation
                if (T >= 1 && false)  //keep this compilable but don't use it, no worky
                {
                    T = 0;
                    endQ = newRot;
                }
            }

        }
 
    }

}
