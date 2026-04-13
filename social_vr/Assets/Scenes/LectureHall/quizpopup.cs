
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
public class quizpopup : MonoBehaviourPun //MonoBehaviour
{
    [Header("UI Refs")]
    public TMP_Text questionText;
    public Button[] answerButtons;       // assign 3 buttons in Inspector
    public TMP_Text feedbackText;
    public GameObject panel; 

    [Header("Questions")]
    string[] questions = {
    "What does XR stand for?",
    "What does VR block out completely?",
    "What does AR overlay onto?",
    "What does FOV stand for?",
    "What is a common XR input method?",
    "What does CV stand for in tech?",
    "Is it ethical to track users without consent?",
    "What does HTTPS protect against?",
    "Who is responsible for AI bias?",
    "What does CPU stand for?"
};
string[][] options = {
    new[]{ "Extra Reality", "Extended Reality", "External Rendering" },
    new[]{ "Sound", "The real world", "Touch" },
    new[]{ "The real world", "A screen", "A headset" },
    new[]{ "Field Of View", "Frame Of Video", "Focus Of Vision" },
    new[]{ "Keyboard", "Hand tracking", "Mouse" },
    new[]{ "Computer Vision", "Core Video", "Color Values" },
    new[]{ "Yes, for safety", "No, it violates privacy", "Only if anonymous" },
    new[]{ "Slow connections", "Eavesdropping", "Viruses" },
    new[]{ "The data", "The developers", "Everyone involved" },
    new[]{ "Central Process Unit", "Core Processing Unit", "Central Processing Unit" }
};
int[] correctIndex = { 1, 1, 0, 0, 1, 0, 1, 1, 2, 2 };

    int current = 0;
    void Awake()
    {
        panel.SetActive(false);
    }
    void OnEnable()
    {
        feedbackText.text = "";
        LoadQuestion(0);
    }

    void LoadQuestion(int i)
    {
        current = i;
        questionText.text = $"Q{i+1}: {questions[i]}";
        feedbackText.text = "";

        for (int b = 0; b < answerButtons.Length; b++)
        {
            int captured = b;   // capture for lambda
            answerButtons[b].GetComponentInChildren<TMP_Text>().text = options[i][b];
            answerButtons[b].onClick.RemoveAllListeners();
            answerButtons[b].onClick.AddListener(() => OnAnswer(captured));
        }
    }

    void OnAnswer(int chosen)
    {
        if (chosen == correctIndex[current])
        {
            feedbackText.text = "Correct!";
            Invoke(nameof(NextQuestion), 1.2f);
        }
        else
        {
            feedbackText.text = "Try again";
        }
    }

    void NextQuestion()
    {
        int next = current + 1;
        if (next < questions.Length)
            LoadQuestion(next);
        else
        {
            questionText.text = "Quiz Complete!";
            feedbackText.text = "";
            foreach (var b in answerButtons) b.gameObject.SetActive(false);
            Invoke(nameof(HideQuiz), 3f); // disappears after 3 sec
        }
    }

    // public void ToggleQuiz() => gameObject.SetActive(!gameObject.activeSelf);
    public void ToggleQuiz()
    {
        photonView.RPC("RPC_ToggleQuiz", RpcTarget.All);
    }

    [PunRPC]
    void RPC_ToggleQuiz()
    {
        panel.SetActive(!panel.activeSelf);
    }

    void HideQuiz()
    // hides locally for each user that finished
{
    panel.SetActive(false); // local only, no RPC
}

}