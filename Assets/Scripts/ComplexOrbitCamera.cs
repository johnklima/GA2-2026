using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexOrbitCamera : MonoBehaviour
{

    public Camera pointCam;
    public Transform moveTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            // Bit shift the index of the layer to get a bit mask
            int layerMask = 1 << 8; //ground

            RaycastHit hit;

            Ray ray = pointCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit , 1000, layerMask))
            {
                moveTarget.position = hit.point;
            }

        }

        float y = Input.mouseScrollDelta.y;
        Vector3 pos = pointCam.transform.localPosition;
        pos.z += y;

        //clamp it
        if (pos.z > -3.0)
            pos.z = -3;
        if (pos.z < -20.0)
            pos.z = -20;

        pointCam.transform.localPosition = pos;

        float deltay = Input.GetAxis("Horizontal");

        Vector3 rot = transform.rotation.eulerAngles;
        rot.y -= deltay * Time.deltaTime * 100.0f ;
        transform.rotation = Quaternion.Euler(rot);

        float deltax = Input.GetAxis("Vertical");

        rot = transform.rotation.eulerAngles;
        rot.x += deltax * Time.deltaTime * 100.0f;
        transform.rotation = Quaternion.Euler(rot);


    }
}
