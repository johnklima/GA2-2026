using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Climber : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public float speed = 1;
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

        float distanceDown = 100;
        float distanceForward = 100;
        //do down
        bool dhit = false;
        dhit = Physics.Raycast(pos, dir, out hit, 100);
        if (dhit)
        {
            distanceDown = hit.distance;
            pos.y = hit.point.y + hit.normal.y;
            transform.position = pos;
            // Rotate object so its 'up' aligns with the hit normal
            Quaternion newRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = newRot;
            //transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 3);

        }
        else if (true) //flip it? forward diagonal?
        {
            dir = transform.up + transform.forward;
            dhit = Physics.Raycast(pos, dir, out hit, 100);
            if (dhit)
            {
                pos.y = hit.point.y + hit.normal.y;
                transform.position = pos;
                // Rotate object so its 'up' aligns with the hit normal
                //interpolated
                Quaternion newRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                transform.rotation = newRot;
            }
            else
            {
                dir = -transform.up - transform.forward;
                dhit = Physics.Raycast(pos, dir, out hit, 100);
                pos.y = hit.point.y + hit.normal.y;
                transform.position = pos;
                // Rotate object so its 'up' aligns with the hit normal
                //interpolated
                Quaternion newRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                transform.rotation = newRot;

            }
        }
        //do forward
        dir = transform.forward;
        if (Physics.Raycast(pos, dir, out hit, 100.0f))
        {
            distanceForward = hit.distance;

            //who is closer
            if (distanceForward < 0.5f)
            {
                pos.y = hit.point.y + hit.normal.y;
                transform.position = pos;
                // Rotate object so its 'up' aligns with the hit normal
                Quaternion newRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                transform.rotation = newRot;
                //transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 3);


            }

        }
 
    }

}
