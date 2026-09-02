using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Climber : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public float speed = 5f;
    public float turnSpeed = 100f;

    void Update()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        // Turn using Euler angles
        transform.eulerAngles += new Vector3(0, turn * turnSpeed * Time.deltaTime, 0);

        // Move forward
        transform.position += transform.forward * move * speed * Time.deltaTime;


        //get the situation report            
        RaycastHit hit;

        Vector3 dir = -transform.up;
        Vector3 pos = transform.position;

        float distanceDown = 0;
        float distanceForward = 0;
        //do down
        if (Physics.Raycast(pos, dir, out hit, 100))
        {
            distanceDown = hit.distance;
            pos.y = hit.point.y + hit.normal.y;
            transform.position = pos; 
            //transform.position = hit.point + hit.normal;
            // Rotate object so its 'up' aligns with the hit normal
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        //do forward
        dir = transform.forward;
        if (Physics.Raycast(pos, dir, out hit, 1.0f))
        {
            distanceForward = hit.distance;

            //who is closer
            if (distanceForward < 0.5f)
            {
                //pos.y = hit.point.y ;
                //transform.position = pos + transform.up;
                // Rotate object so its 'up' aligns with the hit normal
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            }

        }
 
    }

}
