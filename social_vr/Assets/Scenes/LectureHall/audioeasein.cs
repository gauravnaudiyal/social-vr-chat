using UnityEngine;

public class audioeasein : MonoBehaviour
{
    [SerializeField] float fadeDuration = 120f;
    AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();
        src.volume = 0;
        src.Play();
    }

    void Update()
    {
        if (src.volume < 1f)
            src.volume += Time.deltaTime / fadeDuration;
    }
}