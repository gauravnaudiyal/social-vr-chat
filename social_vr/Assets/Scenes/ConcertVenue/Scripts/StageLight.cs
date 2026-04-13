using UnityEngine;

public class StageLight : MonoBehaviour
{
    private Light stageLight;

    [Header("Colours")]
    public Color[] colors = { Color.red, Color.blue, Color.green, Color.magenta };
    public float colorChangeInterval = 1.5f;
    public float colorChangeSpeed = 2f;

    [Header("Movement")]
    public float swingAngle = 30f;
    public float swingSpeed = 1f;
    public bool swingHorizontal = true;

    private int currentIndex = 0;
    private float colorTimer = 0f;
    private float startRotationX;
    private float startRotationY;

    void Start()
    {
        stageLight = GetComponent<Light>();
        stageLight.color = colors[0];
        startRotationX = transform.eulerAngles.x;
        startRotationY = transform.eulerAngles.y;
    }

    void Update()
    {
        // Colour cycling
        colorTimer += Time.deltaTime;
        stageLight.color = Color.Lerp(
            stageLight.color,
            colors[currentIndex],
            Time.deltaTime * colorChangeSpeed
        );

        if (colorTimer >= colorChangeInterval)
        {
            colorTimer = 0f;
            currentIndex = (currentIndex + 1) % colors.Length;
        }

        // Swinging movement
        float swing = Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        if (swingHorizontal)
        {
            transform.eulerAngles = new Vector3(
                startRotationX,
                startRotationY + swing,
                transform.eulerAngles.z
            );
        }
        else
        {
            transform.eulerAngles = new Vector3(
                startRotationX + swing,
                startRotationY,
                transform.eulerAngles.z
            );
        }
    }
}