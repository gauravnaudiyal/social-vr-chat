// SlidePresenter.cs
using UnityEngine;

public class SlidePresenter : MonoBehaviour
{
    public Texture2D[] slides;   // drag your PNGs here in Inspector
    int current = 0;

    Renderer rend;

    void Start() => rend = GetComponent<Renderer>();

    public void NextSlide()
    {
        current = (current + 1) % slides.Length;
        rend.material.mainTexture = slides[current];
    }

    public void PrevSlide()
    {
        current = (current - 1 + slides.Length) % slides.Length;
        rend.material.mainTexture = slides[current];
    }
}