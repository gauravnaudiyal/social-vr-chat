
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
        "What year did Einstein publish Special Relativity?",
        "What is Newton's 2nd Law?",
        "Speed of light (approx)?"
    };
    string[][] options = {
        new[]{ "1895", "1905", "1915" },
        new[]{ "F = ma", "E = mc²", "F = mv" },
        new[]{ "3×10⁸ m/s", "3×10⁶ m/s", "3×10¹⁰ m/s" }
    };
    int[] correctIndex = { 1, 0, 0 };   // index of correct answer per question

    int current = 0;

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
            feedbackText.text = "✅ Correct!";
            Invoke(nameof(NextQuestion), 1.2f);
        }
        else
        {
            feedbackText.text = "❌ Try again";
        }
    }

    void NextQuestion()
    {
        int next = current + 1;
        if (next < questions.Length)
            LoadQuestion(next);
        else
        {
            questionText.text = "🎉 Quiz Complete!";
            feedbackText.text = "";
            foreach (var b in answerButtons) b.gameObject.SetActive(false);
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

}