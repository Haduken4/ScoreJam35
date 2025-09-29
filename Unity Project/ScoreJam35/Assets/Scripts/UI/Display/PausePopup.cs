using UnityEngine;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour
{
    public Slider MusicSlider = null;
    public Slider SFXSlider = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MusicSlider.value = GlobalGameData.MusicVolumeMultiplier;
        SFXSlider.value = GlobalGameData.SFXVolumeMultiplier;

        MusicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        SFXSlider.onValueChanged.AddListener(ChangeSFXVolume);
    }

    void ChangeMusicVolume(float volume)
    {
        GlobalGameData.ChangeMusicVolume(volume);
    }

    void ChangeSFXVolume(float volume)
    {
        GlobalGameData.ChangeSFXVolume(volume);
    }


}
