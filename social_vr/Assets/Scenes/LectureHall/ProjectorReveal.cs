using UnityEngine;

public class ProjectorReveal : MonoBehaviour
{
    [SerializeField] float duration = 1.2f;
    Vector3 fullScale;
    float timer = 0;
    bool revealing = false;
    bool revealed = false;

    void Start()
    {
        fullScale = transform.localScale;
        transform.localScale = new Vector3(fullScale.x, 0, fullScale.z);
    }

    public void Toggle()
    {
        if (revealing) return; // ignore if mid-animation
        revealed = !revealed;
        timer = 0;
        revealing = true;

        if (!revealed) // rolling back down
            GetComponent<Renderer>().material.mainTexture = null;
    }

    void Update()
    {
        if (!revealing) return;
        timer += Time.deltaTime;
        float t = Mathf.SmoothStep(0, 1, timer / duration);
        float targetY = revealed ? fullScale.y : 0;
        float startY = revealed ? 0 : fullScale.y;
        transform.localScale = new Vector3(fullScale.x, Mathf.Lerp(startY, targetY, t), fullScale.z);
        if (timer >= duration) revealing = false;
    }
}