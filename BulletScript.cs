using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 10f;  
    public float bulletLifetime = 2f;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Fill in for Lab 3
        
    }
}
