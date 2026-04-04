using UnityEngine;

public class audioeasein : MonoBehaviour
{
    [SerializeField] float fadeDuration = 120f;
    [SerializeField] AudioSource src;

    void Start()
    {
        src.volume = 0;
        src.Play();
    }

    void Update()
    {
        if (src.volume < 0.5f)
            src.volume += Time.deltaTime / fadeDuration;
    }
}