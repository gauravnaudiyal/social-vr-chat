// SlidePresenter.cs
using UnityEngine;

public class SlidePresenter : MonoBehaviour
{
    public Texture2D[] slides;   // drag your PNGs here in Inspector
    int current = -1;

    Renderer rend;

    void Start() => rend = GetComponent<Renderer>();

  public void NextSlide()
{
    current = Mathf.Clamp(current + 1, 0, slides.Length - 1);
    rend.material.mainTexture = slides[current];
}

public void PrevSlide()
{
    current = Mathf.Clamp(current - 1, 0, slides.Length - 1);
    rend.material.mainTexture = slides[current];
}
}