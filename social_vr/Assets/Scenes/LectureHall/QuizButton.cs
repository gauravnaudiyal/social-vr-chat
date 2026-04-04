// QuizButton.cs — attach to the cube button
using UnityEngine;


public class QuizButton : MonoBehaviour
{
    public quizpopup quizManager;   // drag the Canvas here

    void Start()
    {
        GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>().selectEntered.AddListener(_ => quizManager.ToggleQuiz());
    }
}