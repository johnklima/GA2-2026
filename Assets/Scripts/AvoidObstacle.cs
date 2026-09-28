using UnityEngine;

public class AvoidObstacle : MonoBehaviour
{
    private Transform boid;
    private Boids boids;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boid = transform.parent;
        boids = boid.GetComponent<Boids>();

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform == boid)        
        {
            return;
        }

        //Debug.Log(" trigger " + other.name);

        // Bit shift the index of the layer to get a bit mask
        int layerMask = 1 << 12; //obstacle

        bool didHit = false;
        RaycastHit hit;

        // Does the ray intersect any objects in the layer mask
        if (Physics.Raycast(boid.position, boid.forward, out hit, 10, layerMask))
        {
            Debug.DrawRay(boid.position, boid.forward * hit.distance, Color.red);

            boids.accumAvoid(hit.point);

            didHit = true;
        }
        if (Physics.Raycast(boid.position, -boid.up, out hit, 10, layerMask))
        {
            Debug.DrawRay(boid.position, -boid.up * hit.distance, Color.red);

            boids.accumAvoid(hit.point);

            didHit = true;
        }
        if (Physics.Raycast(boid.position, boid.up, out hit, 10, layerMask))
        {
            Debug.DrawRay(boid.position, boid.up * hit.distance, Color.red);

            boids.accumAvoid(hit.point);

            didHit = true;
        }
        if (Physics.Raycast(boid.position, boid.right, out hit, 10, layerMask))
        {
            Debug.DrawRay(boid.position, boid.right * hit.distance, Color.red);

            boids.accumAvoid(hit.point);

            didHit = true;
        }
        if (Physics.Raycast(boid.position, -boid.right, out hit, 10, layerMask))
        {
            Debug.DrawRay(boid.position, -boid.right * hit.distance, Color.red);

            boids.accumAvoid(hit.point);

            didHit = true;
        }

        if (!didHit)
            boids.resetAvoid();

    }
    private void OnTriggerExit(Collider other)
    {
        boids.resetAvoid();
    }

}
