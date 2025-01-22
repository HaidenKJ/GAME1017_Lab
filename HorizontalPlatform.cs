using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] float oscillationSpeed;
    void Update()
    {
        // Calculate the cosine wave between -1 and 1 over time.
        float oscillationFactor = Mathf.Cos(Time.time * oscillationSpeed * 2.0f * Mathf.PI);

        // Map the cosine wave from -1,1 to 0,1. Why? Because that's what Lerp wants.
        float weight = (oscillationFactor * 0.5f) + 0.5f;

        // Interpolate between startPos and endPos based on the calculated weight.
        Vector3 newPosition = Vector3.Lerp(endPos.position, startPos.position, weight);

        // Set the platform's position to the interpolated value.
        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    { // collision is object that hits the platform.
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = this.transform; // "this" is the script.
        }
    }

    private void OnCollisionExit(Collision collision)
    { // collision is object that hits the platform.
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
