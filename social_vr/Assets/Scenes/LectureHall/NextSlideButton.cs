using UnityEngine;
public class NextSlideButton : MonoBehaviour
{
    public SlidePresenter presenter;
        void Start()
    {
    GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>().selectEntered.AddListener(_ => presenter.NextSlide());
    }
}