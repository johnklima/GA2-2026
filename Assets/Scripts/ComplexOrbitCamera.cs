using Alteruna.Multiplayer.Core;
using Alteruna.Multiplayer.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ComplexOrbitCamera : CommunicationBridge
{

    public Camera pointCam;
    public Transform moveTarget;
    public float mouseSensitivity = 30.0f;

    //will happen after Awake but before Start
    //called when player enters room
    public override void Possessed(bool isMe, User user)
    {
        // disables this script for remote players MAGIC!
        enabled = isMe;
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //left shift is for character select
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) )
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

        //now adjust the stick to show more forward
        float z = Mathf.Abs(pos.z)/5;
        pos = transform.localPosition ;
        pos.z = z;
        transform.localPosition = pos ;

        float deltay = Input.GetAxis("Horizontal");

        Vector3 rot = transform.rotation.eulerAngles;
        rot.y -= deltay * Time.deltaTime * 100.0f ;
        transform.rotation = Quaternion.Euler(rot);

        float deltax = Input.GetAxis("Vertical");

        rot = transform.rotation.eulerAngles;
        rot.x += deltax * Time.deltaTime * 100.0f;
        transform.rotation = Quaternion.Euler(rot);

        //UpdateRotation();


    }
    private void UpdateRotation()
    {
        float sensitivity = mouseSensitivity * Time.deltaTime;

        // Vertical mouse look
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * sensitivity, 0);

        // vertical mouse look

        // Get current camera rotation
        Vector3 cameraRot = pointCam.transform.localEulerAngles;
        // Add mouse movement
        cameraRot.x -= Input.GetAxis("Mouse Y") * sensitivity;
        // Fix overrotation issues
        if (cameraRot.x > 180f) cameraRot.x -= 360f;
        // Clamp input so you can't look behind you
        cameraRot.x = Mathf.Clamp(cameraRot.x, -89f, 89f);
        // apply rotation
        pointCam.transform.localEulerAngles = cameraRot;
    }
}
