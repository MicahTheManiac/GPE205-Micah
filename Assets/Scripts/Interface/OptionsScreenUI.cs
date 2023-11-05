using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsScreenUI : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        OnMusicVolumeChange();
        OnSfxVolumeChange();
    }

    public void OnMusicVolumeChange()
    {
        // Start w/ Slider Value (Assuming it goes 0 - 1)
        float newVolume = musicSlider.value;

        if (newVolume <= 0)
        {
            // If we are at 0, Set Volume to Lowest Value
            newVolume = -80;
        }
        else
        {
            // Find Log10 Value
            newVolume = Mathf.Log10(newVolume);

            // Make it in the Range of 0-20db
            newVolume *= 20;
        }

        // Set the Volume to newVolume Value
        audioMixer.SetFloat("VolumeMusic", newVolume);
    }

    public void OnSfxVolumeChange()
    {
        // Start w/ Slider Value (Assuming it goes 0 - 1)
        float newVolume = sfxSlider.value;

        if (newVolume <= 0)
        {
            // If we are at 0, Set Volume to Lowest Value
            newVolume = -80;
        }
        else
        {
            // Find Log10 Value
            newVolume = Mathf.Log10(newVolume);

            // Make it in the Range of 0-20db
            newVolume *= 20;
        }

        // Set the Volume to newVolume Value
        audioMixer.SetFloat("VolumeSFX", newVolume);
    }
}
