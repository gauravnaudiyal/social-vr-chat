using UnityEngine;
public class PrevSlideButton : MonoBehaviour
{
    public SlidePresenter presenter;
        void Start()
    {
    GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>().selectEntered.AddListener(_ => presenter.PrevSlide());
    }
}