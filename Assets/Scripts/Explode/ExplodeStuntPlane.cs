using UnityEngine;
using UnityEngine.VFX;

public class ExplodeStuntPlane : MonoBehaviour
{
    public VisualEffect ExplosionVFX;
    public ParticleSystem particles;
    public bool explode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ExplosionVFX.pause = true;
        particles.Stop();
    }

    Vector3 impulse;
    // Update is called once per frame
    void Update()
    {
        if( explode )
        {

            explode = false;

            foreach (Transform part in transform)
            {
                Rigidbody rb = part.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                impulse = new Vector3(Random.Range(-10, 10), Random.Range(3, 10), Random.Range(-10, 10));
                rb.AddForce(impulse, ForceMode.Impulse);
            }
            
            transform.GetComponent<Rigidbody>().isKinematic = false;
            impulse = new Vector3(Random.Range(-10, 10), Random.Range(5, 10), Random.Range(-10, 10));
            transform.GetComponent<Rigidbody>().AddForce(impulse, ForceMode.Impulse);

            //play explosion VFX
            ExplosionVFX.Reinit();
            ExplosionVFX.pause = false;

            particles.Play();
        }
        
    }
}
