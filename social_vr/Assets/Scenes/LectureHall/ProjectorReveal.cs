using UnityEngine;

public class ProjectorReveal : MonoBehaviour
{
    [SerializeField] float duration = 1.2f;
    Vector3 fullScale;
    float timer = 0;
    bool revealing = false;

    // void Start() => fullScale = transform.localScale;
     void Start()
    {
        fullScale = transform.localScale; // saves whatever size you set in Inspector
        transform.localScale = new Vector3(fullScale.x, 0, fullScale.z); // starts at 0
    }
    public void Reveal()
    {
        transform.localScale = new Vector3(fullScale.x, 0, fullScale.z);
        revealing = true;
        timer = 0;
    }

    void Update()
    {
        if (!revealing) return;
        timer += Time.deltaTime;
        float t = Mathf.SmoothStep(0, 1, timer / duration); // smooth ease
        transform.localScale = new Vector3(fullScale.x, fullScale.y * t, fullScale.z);
        if (timer >= duration) revealing = false;
    }
}