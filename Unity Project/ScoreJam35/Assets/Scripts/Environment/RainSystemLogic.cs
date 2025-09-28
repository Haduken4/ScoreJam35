using UnityEngine;

public class RainSystemLogic : MonoBehaviour
{
    ParticleSystem particles = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        TurnManager.Instance.OnStartedRaining += OnStartRaining;
        TurnManager.Instance.OnFinishedRaining += OnStopRaining;
    }

    void OnStartRaining()
    {
        particles.Play();
    }

    void OnStopRaining()
    {
        particles.Stop();
    }
}
