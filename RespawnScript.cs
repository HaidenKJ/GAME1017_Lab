using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    private AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        // gameObject.transform.position = SaveManager.Instance.gameData.spawnPosition;
    }

    private void OnTriggerEnter(Collider other) // 3D not 2D.
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            Debug.Log("Checkpoint reached!");
            if (!aud.isPlaying) // Prevents checkpoint sound from overriding explosion.
            {
                aud.clip = audioClips[0];
                aud.Play();
            }   
            // SaveManager.Instance.UpdateSpawnPoint(other.transform.position);
        }
        else if (other.gameObject.tag == "KillY")
        {
            aud.clip = audioClips[1];
            aud.Play();
            // this.transform.position = SaveManager.Instance.gameData.spawnPosition;
        }
    }
}
