using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioClip AudioOw;

    private AudioSource audioSource;

    void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void PlayOw()
    {
        this.audioSource.PlayOneShot(AudioOw);
    }
}