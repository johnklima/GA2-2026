using UnityEngine;

public class Climber : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        {
            
            //do move first
            Vector3 dir = transform.forward;
            Vector3 pos = transform.position;

            float deltay = Input.GetAxis("Horizontal");

            Vector3 rot = transform.rotation.eulerAngles;
            rot.y -= deltay * Time.deltaTime * 100.0f;
            transform.rotation = Quaternion.Euler(rot);

            float deltax = Input.GetAxis("Vertical");
            pos += dir * deltax * Time.deltaTime * 2.0f;
            transform.position = pos;

            //get the situation report            
            RaycastHit hit;

            dir = -transform.up;
            pos = transform.position;

            float distanceDown = 0;
            float distanceForward = 0;
            //do down
            if (Physics.Raycast(pos,dir, out hit, 100 ))
            {
                distanceDown = hit.distance;    
                transform.position = hit.point + hit.normal;
                // Rotate object so its 'up' aligns with the hit normal
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }

            //do forward
            dir = transform.forward;
            if (Physics.Raycast(pos, dir, out hit, 100))
            {
                distanceForward = hit.distance;
                
            }
            //who is closer
            if (distanceForward < distanceDown && false) 
            {
                //align to what's in front
                transform.position = hit.point + hit.normal;
                // Rotate object so its 'up' aligns with the hit normal
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            }


        }


    }
}
