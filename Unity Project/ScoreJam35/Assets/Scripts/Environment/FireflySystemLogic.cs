using UnityEngine;

public class FireflySystemLogic : MonoBehaviour
{
    ParticleSystem particles = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particles = GetComponent<ParticleSystem>();

        TurnManager.Instance.OnStartDay += OnDayStarted;
        TurnManager.Instance.OnStartNight += OnNightStarted;
    }

    void OnNightStarted()
    {
        particles.Play();
    }

    void OnDayStarted()
    {
        particles.Stop();
    }
}
