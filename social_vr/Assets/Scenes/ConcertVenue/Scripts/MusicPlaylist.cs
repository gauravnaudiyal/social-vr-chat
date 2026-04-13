using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{
    public AudioClip[] playlist;
    private AudioSource audioSource;
    private int currentTrack = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            currentTrack = (currentTrack + 1) % playlist.Length;
            audioSource.clip = playlist[currentTrack];
            audioSource.Play();
        }
    }
}