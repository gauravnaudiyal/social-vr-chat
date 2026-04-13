using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MicrophoneGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    [Header("Music Settings")]
    public AudioSource musicSource;
    public float normalVolume = 1f;
    public float grabbedVolume = 0.2f;
    public float fadeSpeed = 2f;

    private float targetVolume;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        targetVolume = normalVolume;

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void Update()
    {
        if (musicSource != null)
            musicSource.volume = Mathf.Lerp(musicSource.volume, targetVolume, Time.deltaTime * fadeSpeed);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        targetVolume = grabbedVolume;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        targetVolume = normalVolume;
    }
}