using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public Vector2 VolumeRange = Vector2.one;

    AudioSource source = null;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        source.volume = Random.Range(VolumeRange.x, VolumeRange.y);
        source.Play();
    }
}
