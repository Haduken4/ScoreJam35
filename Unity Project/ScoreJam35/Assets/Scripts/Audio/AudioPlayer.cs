using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public Vector2 VolumeRange = Vector2.one;
    public bool Music = false;
    public bool SetVolumeOnUpdate = false;

    AudioSource source = null;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        float multiplier = Music ? GlobalGameData.MusicVolumeMultiplier : GlobalGameData.SFXVolumeMultiplier;

        source.volume = Random.Range(VolumeRange.x, VolumeRange.y) * multiplier;
    }

    private void Update()
    {
        if (SetVolumeOnUpdate)
        {
            float multiplier = Music ? GlobalGameData.MusicVolumeMultiplier : GlobalGameData.SFXVolumeMultiplier;

            source.volume = Random.Range(VolumeRange.x, VolumeRange.y) * multiplier;
        }
    }

    public void PlaySound()
    {
        float multiplier = Music ? GlobalGameData.MusicVolumeMultiplier : GlobalGameData.SFXVolumeMultiplier;

        source.volume = Random.Range(VolumeRange.x, VolumeRange.y) * multiplier;
        source.Play();
    }
}
