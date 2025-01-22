using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] float scrollSpeed;
    private const float ENDPOS = -13.312f;
    private const float STARTPOS = 0f;
    
    void Update()
    {
        // Scroll the backgrounds.
        transform.Translate(scrollSpeed * Time.deltaTime, 0f, 0f);
        // If at end of journey, bounce back to start position.
        if (transform.position.x <= ENDPOS)
        {
            transform.position = new Vector3(STARTPOS, 0f, 0f);
        }
    }
}
