using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    SOUND_SFX,
    SOUND_MUSIC
}

public static class SoundManager
{
    // Create Dictionary for sfx.
    private static Dictionary<string, AudioClip> sfxDictionary;
    // Create Dictionary for music.
    private static Dictionary<string, AudioClip> musicDictionary;
    // Create AudioSource for sfx.
    private static AudioSource sfxSource;
    // Create AudioSource for music.
    private static AudioSource musicSource;
    static SoundManager() // Static constructor. Gets called the first time the class is accessed.
    {
        Initialize();
    }

    // Initialize the SoundManager. I just put this functionality here instead of in the static constructor.
    private static void Initialize()
    {
        // Initialize our Dictionaries.
        sfxDictionary = new Dictionary<string, AudioClip>();
        musicDictionary = new Dictionary<string, AudioClip>();
        // Create a new GameObject to hold the AudioSource
        GameObject soundManagerObject = new GameObject("SoundManager");
        sfxSource = soundManagerObject.AddComponent<AudioSource>();
        musicSource = soundManagerObject.AddComponent<AudioSource>();
        sfxSource.volume = 0.75f;
        musicSource.volume = 0.25f;
        musicSource.loop = true;
    }

    // Add a sound to the dictionary.
    public static void AddSound(string soundKey, AudioClip audioClip, SoundType soundType)
    {
        if (audioClip == null)
        {
            Debug.LogError($"Error loading AudioClip: {audioClip}.");
            return;
        }
        switch (soundType)
        {
            case SoundType.SOUND_SFX:
                if (!sfxDictionary.ContainsKey(soundKey))
                { // If there isn't a key matching the name held in soundKey, then add it.
                    sfxDictionary.Add(soundKey, audioClip);
                }
                else
                {
                    Debug.LogWarning($"Key: {soundKey} already found in Dictionary.");
                }
                break;
            case SoundType.SOUND_MUSIC:
                if (!musicDictionary.ContainsKey(soundKey))
                { // If there isn't a key matching the name held in soundKey, then add it.
                    musicDictionary.Add(soundKey, audioClip);
                }
                else
                {
                    Debug.LogWarning($"Key: {soundKey} already found in Dictionary.");
                }
                break;
            default:
                Debug.LogError($"Unsupported soundType: {soundType}.");
                break;
        }
    }

    // Play a sound by key interface.
    public static void PlaySound(string soundKey)
    {
        if (sfxDictionary.ContainsKey(soundKey))
        {
            sfxSource.PlayOneShot(sfxDictionary[soundKey]);
        } // PlayOneShot allows sound clips to be layered (played over each other).
        else
        {
            Debug.LogError($"Key: {soundKey} not found in Dictionary.");
        }
    }

    // Play music by key interface.
    public static void PlayMusic(string soundKey)
    {
        if (musicDictionary.ContainsKey(soundKey))
        {
            musicSource.Stop();
            musicSource.clip = musicDictionary[soundKey];
            musicSource.Play();
        }
        else
        {
            Debug.LogError($"Key: {soundKey} not found in Dictionary.");
        }
    }

    // Play utility.
    private static void Play(string soundKey, SoundType soundType)
    {
        // Fill in next lab.
    }

    //private static void SetTargetsByType(SoundType soundType, out Dictionary<string, AudioClip> targetDictionary, out AudioSource targetSource)
    //{
    //    // Fill in for lab.
    //}
    //private static Dictionary<string, AudioClip> GetDictionaryByType(SoundType soundType)
    //{
    //    // Fill in for lab.
    //}

        // New methods for slider integration

    public static void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume); // Ensure volume stays between 0 and 1
        if (sfxSource.isPlaying)
        {
            sfxSource.PlayOneShot(sfxSource.clip);  // Restart SFX if it's playing
        }
    }

    public static void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume); // Ensure volume stays between 0 and 1
        if (!musicSource.isPlaying)
        {
            musicSource.Play();  // Restart Music if it's not playing
        }
    }
    public static void SetMasterVolume(float volume)
    {
        AudioListener.volume = Mathf.Clamp01(volume); // Adjusts the global audio volume
    }
    public static void SetStereoPanning(float panning)
    {
        sfxSource.panStereo = panning;
        musicSource.panStereo = panning;
    }

    public static float GetStereoPanning()
    {
        return sfxSource.panStereo; // Same for both music and SFX
    }
    public static float GetSFXVolume()
    {
        return sfxSource.volume;
    }

    public static float GetMusicVolume()
    {
        return musicSource.volume;
    }
}
