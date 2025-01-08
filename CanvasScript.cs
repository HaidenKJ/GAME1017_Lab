using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    void Start()
    { 

    // SFX
        
       // Note: The first string (key name) can be anything
       //       ,but the second string (file name) has to match the file name in the Resources folder.
       SoundManager.AddSound("boom", Resources.Load<AudioClip>("boom"),SoundType.SOUND_SFX);
       SoundManager.AddSound("jump", Resources.Load<AudioClip>("jump"),SoundType.SOUND_SFX);

    // Music

       SoundManager.AddSound("MASK", Resources.Load<AudioClip>("MASK"),SoundType.SOUND_MUSIC);
       SoundManager.AddSound("turtles", Resources.Load<AudioClip>("Turtles"),SoundType.SOUND_MUSIC);
    }

    public void PlaySFX(string soundKey)
    {
        SoundManager.PlaySound(soundKey);
    }
    public void PlayMusic(string soundKey)
    {
        SoundManager.PlayMusic(soundKey);
    }
}
