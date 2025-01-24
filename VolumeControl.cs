
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider stereoPanningSlider; // New slider for stereo panning
    

    private void Start()
    {
        if (stereoPanningSlider != null)
        {
            stereoPanningSlider.value = SoundManager.GetStereoPanning();
            stereoPanningSlider.onValueChanged.AddListener(SetStereoPanning);
        }

        // Initialize sliders with current volume levels
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = SoundManager.GetSFXVolume();
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = SoundManager.GetMusicVolume();
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = AudioListener.volume;
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        }
    }

    public void OnSFXVolumeChanged(float value)
    {
        SoundManager.SetSFXVolume(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        SoundManager.SetMusicVolume(value);
    }

    public void OnMasterVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }

    public void SetStereoPanning(float value)
    {
        SoundManager.SetStereoPanning(value);
    }

}
