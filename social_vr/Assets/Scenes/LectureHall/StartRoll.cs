using UnityEngine;

public class StartRoll : MonoBehaviour
{
    public ProjectorReveal projector;
         void Start()
    {
    GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>().selectEntered.AddListener(_ => projector.Reveal());
    }
}
