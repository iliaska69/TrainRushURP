using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private float deltaTime;

    void Start()
    {
        // Make the game run as fast as possible
        //Application.targetFrameRate = -1;
        // Limit the framerate to 60
        Application.targetFrameRate = 60;
        //QualitySettings.vSyncCount = 1;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }


}
