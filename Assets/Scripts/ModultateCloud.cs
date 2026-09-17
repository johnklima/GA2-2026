using UnityEngine;

public class ModultateCloud : MonoBehaviour
{
    public Material material;

    public float timeOffset = 0;
    public float speedMult = 0;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        timeOffset = Random.Range(1f, 256f);
        speedMult = Random.Range(0.01f, 0.1f);

        //start at random rotation Y
        float yRot;
        yRot = Random.Range(-180, 180);

        Quaternion rotation = Quaternion.Euler(0, yRot, 0);
        transform.rotation = rotation;

        //snap to ground
        int layerMask = 1 << 8; //Ground

        RaycastHit hit;       

        if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, out hit, 1000, layerMask))
        {
            Vector3 P = hit.point;

            transform.position = P + transform.localScale / 2.0f ;  //and by what additional scalar?
            //mult the quats to align to surface norm
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            //slerp it or just set it?
            transform.rotation = targetRotation;    

        }


    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.Abs(Mathf.Sin((Time.time + timeOffset) * speedMult) ) ;
        material.SetFloat("_stepSize", t);
        material.SetFloat("_numSteps", t);
        material.SetFloat("_densityScale", t);

        Vector3 eulers = transform.rotation.eulerAngles;

        eulers.y += Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(eulers);
        transform.rotation = rotation;


    }
}
