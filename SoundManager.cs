using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public enum SoundType
{
    SOUND_SFX,
    SOUND_MUSIC
}

// This SoundManager is a GameObject in Memory, it is not an actual simulated object (I mean its not physical)
public static class SoundManager // This is a static class. Every member should be static.
{

    // Create Dictionaries
    private static Dictionary<string, AudioClip> sfxDictionary;
    private static Dictionary<string, AudioClip> musicDictionary;
    // Create AudioSources. A GameObject in the scene is needed for theese.
    private static AudioSource sfxSource;
    private static AudioSource musicSource;

    static SoundManager() // Static Constructor. Called the first time the class is accessed.
    {
        initialize();
    }

    private static void initialize()
    {
        // Initialize the Dictionaries
        sfxDictionary = new Dictionary<string, AudioClip>();
        musicDictionary = new Dictionary<string, AudioClip>();
        // Create a new GameObject to hold the AudioSources.
        GameObject SoundManagerObject = new GameObject("SoundManager");
        
        sfxSource = SoundManagerObject.AddComponent<AudioSource>();
        musicSource = SoundManagerObject.AddComponent<AudioSource>();
        sfxSource.volume = 0.75f; // 0.0f - 1.0f for volume, think of it as out of 100%
        musicSource.volume = 0.25f;
        musicSource.loop = true; // Loops our music track

    }


    //public static void SetTargetsBytType()
    //{

    //}
    //public static Dictionary<string, AudioClip> GetDictionaryByType()
    //{
    //    return null;
    //}




    // Define our public interface for the manager.
    public static void AddSound(string soundkey, AudioClip audioClip, SoundType soundType)
    {
        if(audioClip == null)
        {
            Debug.LogError("Error Loading AudioClip");
            return;
        }
        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                if (!sfxDictionary.ContainsKey(soundkey))
                {
                    sfxDictionary.Add(soundkey, audioClip);
                }
                else
                {
                    Debug.LogError($"Key {soundkey} already in dictionary");
                }
                    break;
            case SoundType.SOUND_MUSIC:
                if (!musicDictionary.ContainsKey(soundkey))
                {
                    musicDictionary.Add(soundkey, audioClip);
                }
                else
                {
                    Debug.LogError($"Key {soundkey} already in dictionary");
                }
                    break;
            
            default:
                Debug.LogError("Unsupported SoundType!");
                break;
        }

    }
    public static void PlaySound(string soundKey)
    {
        if (sfxDictionary.ContainsKey(soundKey))
        {
            sfxSource.PlayOneShot(sfxDictionary[soundKey]);
            // PlayOneShot Allows SFX to be layered over each other using the same source. 
        }
        else
        {
            Debug.LogError($"Error {soundKey} not found!");
        }
    }
    public static void PlayMusic(string soundKey)
    {
        if (musicDictionary.ContainsKey(soundKey))
        {
            musicSource.Stop();
            musicSource.clip = musicDictionary[soundKey]; // Loading music clip into the source
            musicSource.Play(); 
            // This means you can only play one instance of music at time, so no layering like PlayOneShot.
        }
        else
        {
            Debug.LogError($"Error {soundKey} not found!");
        }
    }
    public static void Play()
    {
       // DO THIS NEXT WEEK
    }


}
