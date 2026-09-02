using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    }
}
