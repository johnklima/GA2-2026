using UnityEngine;

public class ModultateCloud : MonoBehaviour
{
    public Material material;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //snap to ground
        int layerMask = 1 << 8; //Ground

        RaycastHit hit;       

        if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, out hit, 1000, layerMask))
        {
            Vector3 P = hit.point;

            transform.position = P + transform.localScale / 2.0f;
            //mult the quats to align to surface norm
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        }


    }

    // Update is called once per frame
    void Update()
    {
        float t =( Mathf.Abs(Mathf.Sin(Time.time)) );
        material.SetFloat("_stepSize", t);
        material.SetFloat("_numSteps", t);

    }
}
