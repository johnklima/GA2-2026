using UnityEngine;

public class Key : MonoBehaviour
{
    public static bool hasKey = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            hasKey = true;
            Destroy(gameObject);
        }
    }
}