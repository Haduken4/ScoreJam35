using System.Collections.Generic;
using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{
    [System.Serializable]
    public class SourceVolumePair
    {
        public AudioSource Source = null;
        public float Volume = 0;
    }

    public List<SourceVolumePair> AllSources = new List<SourceVolumePair>();

    public List<SourceVolumePair> DayAmbienceSources = new List<SourceVolumePair>();
    public List<SourceVolumePair> NightAmbienceSources = new List<SourceVolumePair>();

    public float TransitionTime = 3;
    float timer = 0;
    bool transitioning = false;

    List<SourceVolumePair> transitioningIn = null;
    List<SourceVolumePair> transitioningOut = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TurnManager.Instance.OnEndDay += OnEndDay;
        TurnManager.Instance.OnEndNight += OnEndNight;
        GlobalGameData.OnMusicVolumeChanged += OnMusicVolumeChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if(transitioning)
        {
            timer -= Time.deltaTime;
            float t = Mathf.Max(0, 1.0f - timer / TransitionTime);

            foreach(SourceVolumePair source in transitioningIn)
            {
                source.Source.volume = Mathf.Lerp(0, source.Volume * GlobalGameData.MusicVolumeMultiplier, t);
            }

            foreach (SourceVolumePair source in transitioningOut)
            {
                source.Source.volume = Mathf.Lerp(source.Volume * GlobalGameData.MusicVolumeMultiplier, 0, t);
            }

            if(timer <= 0.0f)
            {
                transitioning = false;
            }
        }
    }

    void OnEndDay()
    {
        transitioning = true;
        transitioningIn = NightAmbienceSources;
        transitioningOut = DayAmbienceSources;
        timer = TransitionTime;
    }

    void OnEndNight()
    {
        transitioning = true;
        transitioningIn = DayAmbienceSources;
        transitioningOut = NightAmbienceSources;
        timer = TransitionTime;
    }

    void OnMusicVolumeChanged()
    {
        if (transitioning)
        {
            return;
        }

        foreach(SourceVolumePair source in AllSources)
        {
            if (source.Source.volume != 0)
            {
                source.Source.volume = source.Volume * GlobalGameData.MusicVolumeMultiplier;
            }
        }
    }
}
